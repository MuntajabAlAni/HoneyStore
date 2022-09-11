using API.DTOs;
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
    public async Task<ActionResult<Pagination<Post>>> GetPosts([FromQuery] PostSpecificationParameters postParameters)
    {
        var spec = new PostWithSpecification(postParameters);
        var countSpec = new PostWithFiltersForCountSpecification(postParameters);
        
        var totalItems = await _unitOfWork.Repository<Post>().CountAsync(countSpec);
        var posts = await _unitOfWork.Repository<Post>().ListAsyncWithSpec(spec);
        return Ok(new Pagination<Post>(postParameters.PageIndex, postParameters.PageSize, totalItems, posts));
    }

    [HttpPost]
    public async Task<IActionResult> Create(PostRequestDto requestDto)
    {
        var post = _mapper.Map<Post>(requestDto);
        if (requestDto.Image is null)
            post.PictureUrl = "";
        else
            post.PictureUrl = await CopyFileToServerAsync(requestDto.Image);
        _unitOfWork.Repository<Post>().Add(post);
        var result = await _unitOfWork.Complete();
         
        return Ok(result <= 0 ? null : post);
    }

    private async Task<string> CopyFileToServerAsync(IFormFile image)
    {
        var imageFolderName = Path.Combine("Resources", "PostImages");
        var imageUrl = Guid.NewGuid() + Path.GetExtension(image.FileName);
        var pathToSaveImage = Path.Combine(imageFolderName, imageUrl);

        await using var streamImage = new FileStream((pathToSaveImage), FileMode.Create);
        await image.CopyToAsync(streamImage);
        return imageUrl;
    }
}