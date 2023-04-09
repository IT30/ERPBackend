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
            ProductTypeEntity? productType = GetProductTypeByID(ProductTypeID);
            if (productType != null)
                context.Remove(productType);
        }

        public ProductTypeEntity? GetProductTypeByID(Guid ProductTypeID)
        {
            return context.ProductType.FirstOrDefault(e => e.IDProductType == ProductTypeID);
        }

        public List<ProductTypeEntity> GetProductTypes()
        {
            return context.ProductType.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
