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
            ProductEntity product = mapper.Map<ProductEntity>(productCreateDTO);
            product.IDProduct = Guid.NewGuid();
            context.Add(product);
            return mapper.Map<ProductDTO>(product);
        }

        public void DeleteProduct(Guid ProductID)
        {
            ProductEntity? product = GetProductByID(ProductID);
            if (product != null)
                context.Remove(product);
        }

        public ProductEntity? GetProductByID(Guid ProductID)
        {
            return context.Product.FirstOrDefault(e => e.IDProduct == ProductID);
        }

        public List<ProductEntity> GetProducts()
        {
            return context.Product.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
