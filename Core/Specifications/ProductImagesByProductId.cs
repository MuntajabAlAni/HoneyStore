using Core.Entities;

namespace Core.Specifications;

public class ProductImagesByProductId : BaseSpecification<ProductImages>
{
    public ProductImagesByProductId(int productId) : base(i => i.ProductId == productId)
    {
    }
}