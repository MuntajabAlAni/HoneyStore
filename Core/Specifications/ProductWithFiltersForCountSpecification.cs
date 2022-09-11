using Core.Entities;

namespace Core.Specifications;

public class ProductWithFiltersForCountSpecification: BaseSpecification<Product>
{
    public ProductWithFiltersForCountSpecification(ProductSpecificationParameters productParameters)
        : base(x =>
            (string.IsNullOrEmpty(productParameters.Search) || x.Name!.ToLower().Contains(productParameters.Search)) &&
            (!productParameters.CollectionId.HasValue || x.ProductCollectionId == productParameters.CollectionId) &&
            (!productParameters.TypeId.HasValue || x.ProductTypeId == productParameters.TypeId))
    {
    }
}