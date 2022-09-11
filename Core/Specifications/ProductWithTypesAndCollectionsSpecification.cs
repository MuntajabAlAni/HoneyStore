using Core.Entities;

namespace Core.Specifications;

public class ProductWithTypesAndCollectionsSpecification: BaseSpecification<Product>
{
    public ProductWithTypesAndCollectionsSpecification(ProductSpecificationParameters productParameters)
        :base(x => 
            (string.IsNullOrEmpty(productParameters.Search) || x.Name!.ToLower().Contains(productParameters.Search)) &&
            (!productParameters.CollectionId.HasValue || x.ProductCollectionId == productParameters.CollectionId) &&
            (!productParameters.TypeId.HasValue || x.ProductTypeId == productParameters.TypeId))
    {
        AddInclude(x => x.productType!);
        AddInclude(x => x.productCollection!);
        AddOrderBy(p => p.Name!);
        ApplyPaging(productParameters.PageSize * (productParameters.PageIndex - 1), productParameters.PageSize);

        if(!string.IsNullOrEmpty(productParameters.Sort)){
            switch (productParameters.Sort){
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break; 
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break; 
                default:
                    AddOrderBy(p => p.Name!);
                    break; 
            }
        }
    }

    public ProductWithTypesAndCollectionsSpecification(int id) : base(p => p.Id == id)
    {
        AddInclude(x => x.productType!);
        AddInclude(x => x.productCollection!);
    }
}