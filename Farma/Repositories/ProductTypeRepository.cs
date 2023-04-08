using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly FarmaContext context;
        private readonly IMapper mapper;

        public ProductTypeRepository(FarmaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ProductTypeDTO CreateProductType(ProductTypeCreateDTO productTypeCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductType(Guid ProductTypeID)
        {
            throw new NotImplementedException();
        }

        public ProductTypeEntity? GetProductTypeByID(Guid ProductTypeID)
        {
            throw new NotImplementedException();
        }

        public List<ProductTypeEntity> GetProductTypes()
        {
            return context.ProductType.ToList();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
