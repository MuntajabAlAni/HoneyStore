using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PostsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public PostsController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<PostToReturnDto>>> GetPosts(
        [FromQuery] PostSpecificationParameters postParameters)
    {
        var spec = new PostWithSpecification(postParameters);
        var countSpec = new PostWithFiltersForCountSpecification(postParameters);

        var totalItems = await _unitOfWork.Repository<Post>().CountAsync(countSpec);
        var posts = await _unitOfWork.Repository<Post>().ListAsyncWithSpec(spec);
        var data = _mapper.Map<IReadOnlyList<Post>, IReadOnlyList<PostToReturnDto>>(posts);
        return Ok(new Pagination<PostToReturnDto>(postParameters.PageIndex, postParameters.PageSize, totalItems, data));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostToReturnDto>> GetPost(int id)
    {
        var spec = new PostWithSpecification(id);
        var post = await _unitOfWork.Repository<Post>().GetEntityWithSpec(spec);

        if (post is null)
            return NotFound(new ApiResponse(404));
        return _mapper.Map<Post, PostToReturnDto>(post);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] PostRequestDto requestDto)
    {
        var post = _mapper.Map<Post>(requestDto);
        post.PictureUrl = await CopyFileToServerAsync(requestDto.Image);

        _unitOfWork.Repository<Post>().Add(post);
        await _unitOfWork.Complete();

        if (requestDto.ReceiptField > 0)
        {
            _unitOfWork.Repository<PostReceiptFields>().Add(new PostReceiptFields
            {
                PostId = post.Id,
                ReceiptFieldId = requestDto.ReceiptField
            });
        }

        var result = await _unitOfWork.Complete();
        var postToReturnDto = _mapper.Map<Post, PostToReturnDto>(post);
        postToReturnDto.ReceiptField = requestDto.ReceiptField;

        return Ok(result <= 0 ? null : postToReturnDto);
    }

    [HttpPut]
    public async Task<ActionResult<Post>> Update([FromForm] PostRequestDto requestDto)
    {
        var post = _mapper.Map<Post>(requestDto);
        DeleteFileFromServer(post.PictureUrl);

        post.PictureUrl = await CopyFileToServerAsync(requestDto.Image);

        _unitOfWork.Repository<Post>().Update(post);
        var result = await _unitOfWork.Complete();

        return Ok(result <= 0 ? null : post);
    }

    [HttpDelete]
    public async Task<ActionResult<int>> Delete(int id)
    {
        var spec = new PostWithSpecification(id);
        var post = await _unitOfWork.Repository<Post>().GetEntityWithSpec(spec);

        if (post is null)
            return NotFound(new ApiResponse(404));

        DeleteFileFromServer(post.PictureUrl);

        post.IsDeleted = true;
        _unitOfWork.Repository<Post>().Update(post);
        var result = await _unitOfWork.Complete();

        return Ok(result <= 0 ? result : post.Id);
    }

    private static async Task<string> CopyFileToServerAsync(IFormFile image)
    {
        var imageFolderName = Path.Combine("Resources", "PostImages");
        if (!Directory.Exists(imageFolderName))
            Directory.CreateDirectory(imageFolderName);
        var imageUrl = Guid.NewGuid() + Path.GetExtension(image.FileName);
        var pathToSaveImage = Path.Combine(imageFolderName, imageUrl);

        await using var streamImage = new FileStream((pathToSaveImage), FileMode.Create);
        await image.CopyToAsync(streamImage);
        return imageUrl;
    }

    private static void DeleteFileFromServer(string pictureUrl)
    {
        var imageFolderName = Path.Combine("Resources", "PostImages");
        var pathToDeleteImage = Path.Combine(imageFolderName, pictureUrl);

        System.IO.File.Delete(pathToDeleteImage);
    }
}