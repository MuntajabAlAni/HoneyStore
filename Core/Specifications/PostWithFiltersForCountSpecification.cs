using Core.Entities;

namespace Core.Specifications;

public class PostWithFiltersForCountSpecification : BaseSpecification<Post>
{
    public PostWithFiltersForCountSpecification(PostSpecificationParameters postParameters)
    :base(post=>
        (string.IsNullOrEmpty(postParameters.Search) || post.Title.ToLower().Contains(postParameters.Search))
        && post.Type == postParameters.Type)
    {
        
    }
}