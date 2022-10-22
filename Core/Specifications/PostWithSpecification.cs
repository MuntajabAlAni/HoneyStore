using Core.Entities;

namespace Core.Specifications;

public class PostWithSpecification : BaseSpecification<Post>
{
    public PostWithSpecification(PostSpecificationParameters postParameters)
        : base(post =>
            (string.IsNullOrEmpty(postParameters.Search) || post.Title.ToLower().Contains(postParameters.Search)) &&
            post.IsDeleted == false && post.Type == postParameters.Type)
           
    {
        AddOrderBy(p => p.Title);
        ApplyPaging(postParameters.PageSize * (postParameters.PageIndex - 1), postParameters.PageSize);
    }

    public PostWithSpecification(int id) : base(p => p.Id == id && p.IsDeleted == false)
    {
    }
}