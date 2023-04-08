using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public interface IProductRepository
    {
        List<ProductEntity> GetProducts();

        ProductEntity? GetProductByID(Guid ProductID);

        ProductDTO CreateProduct(ProductCreateDTO productCreateDTO);

        void DeleteProduct(Guid ProductID);

        bool SaveChanges();

    }
}
