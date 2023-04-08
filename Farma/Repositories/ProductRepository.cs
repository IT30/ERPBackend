using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly FarmaContext context;
        private readonly IMapper mapper;

        public ProductRepository(FarmaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ProductDTO CreateProduct(ProductCreateDTO productCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Guid ProductID)
        {
            throw new NotImplementedException();
        }

        public ProductEntity? GetProductByID(Guid ProductID)
        {
            throw new NotImplementedException();
        }

        public List<ProductEntity> GetProducts()
        {
            return context.Product.ToList();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
