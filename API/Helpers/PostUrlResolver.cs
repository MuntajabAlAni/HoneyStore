using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class PostUrlResolver : IValueResolver<Post, PostToReturnDto, string?>
{
    private readonly IConfiguration _config;

    public PostUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public string? Resolve(Post source, PostToReturnDto destination, string? destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return _config["PostsApiUrl"] + source.PictureUrl;
        }

        return null;
    }
}