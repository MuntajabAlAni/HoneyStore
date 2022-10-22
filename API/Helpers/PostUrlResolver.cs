using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class PostUrlResolver : IValueResolver<Post, Post, string?>
{
    private readonly IConfiguration _config;

    public PostUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public string? Resolve(Post source, Post destination, string? destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return _config["ApiUrl"] + source.PictureUrl;
        }

        return null;
    }
}