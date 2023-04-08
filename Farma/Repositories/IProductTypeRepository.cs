using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public interface IProductTypeRepository
    {
        List<ProductTypeEntity> GetProductTypes();

        ProductTypeEntity? GetProductTypeByID(Guid ProductTypeID);

        ProductTypeDTO CreateProductType(ProductTypeCreateDTO productTypeCreateDTO);

        void DeleteProductType(Guid ProductTypeID);

        bool SaveChanges();
    }
}
