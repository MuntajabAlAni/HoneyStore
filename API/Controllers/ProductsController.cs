using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductsController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
        [FromQuery] ProductSpecificationParameters productParameters)
    {
        var spec = new ProductWithTypesAndCollectionsSpecification(productParameters);

        var countSpec = new ProductWithFiltersForCountSpecification(productParameters);

        var totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);

        var products = await _unitOfWork.Repository<Product>().ListAsyncWithSpec(spec);

        var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

        return Ok(new Pagination<ProductToReturnDto>(productParameters.PageIndex, productParameters.PageSize,
            totalItems, data));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductWithTypesAndCollectionsSpecification(id);
        var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec);

        if (product is null) return NotFound(new ApiResponse(404));
        return _mapper.Map<Product, ProductToReturnDto>(product);
    }

    [HttpGet("collections")]
    public async Task<ActionResult<IReadOnlyList<ProductCollection>>> GetProductCollections()
    {
        var collections = await _unitOfWork.Repository<ProductCollection>().ListAllAsync();

        return Ok(collections);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        var types = await _unitOfWork.Repository<ProductType>().ListAllAsync();

        return Ok(types);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromForm] ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await CopyFileToServerAsync(productDto.Images, product);

        _unitOfWork.Repository<Product>().Add(product);
        var result = await _unitOfWork.Complete();

        return Ok(result <= 0 ? null : product);
    }

    [HttpPut]
    public async Task<ActionResult<Product>> Update([FromForm] ProductDto productDto)
    {
        var productImagesSpec = new ProductImagesByProductId(productDto.Id!.Value);
        var productImages = await _unitOfWork.Repository<ProductImages>().ListAsyncWithSpec(productImagesSpec);

        productImages.ToList().ForEach(pi => { _unitOfWork.Repository<ProductImages>().Delete(pi); });

        var product = _mapper.Map<Product>(productDto);
        await CopyFileToServerAsync(productDto.Images, product);
        
        _unitOfWork.Repository<Product>().Update(product);
        var result = await _unitOfWork.Complete();

        if (result > 0)
            await DeleteFileFromServer(productImages);

        return Ok(result <= 0 ? null : product);
    }

    private static async Task CopyFileToServerAsync(List<ProductImagesDto> productImagesDto, Product product)
    {
        foreach (var productImage in productImagesDto)
        {
            var imageFolderName = Path.Combine("Resources", "ProductImages");
            var imageUrl = Guid.NewGuid() + Path.GetExtension(productImage.Image.FileName);

            if (productImage.IsMain) product.PictureUrl = imageUrl;

            var pathToSaveImage = Path.Combine(imageFolderName, imageUrl);

            await using var streamImage = new FileStream((pathToSaveImage), FileMode.Create);
            await productImage.Image.CopyToAsync(streamImage);

            product.ProductImages.Add(new ProductImages
            {
                pictureUrl = imageUrl
            });
        }
    }

    private static Task DeleteFileFromServer(IEnumerable<ProductImages> productImages)
    {
        foreach (var pathToDeleteImage in from productImage in productImages
                 let imageFolderName = Path.Combine("Resources", "ProductImages")
                 select Path.Combine(imageFolderName, productImage.pictureUrl))
        {
            System.IO.File.Delete(pathToDeleteImage);
        }
    }
}