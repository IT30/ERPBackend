using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly FarmaContext context;
        private readonly IMapper mapper;

        public CartItemRepository(FarmaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public CartItemDTO CreateCartItem(CartItemCreateDTO cartItemCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteCartItem(Guid CartItemID)
        {
            throw new NotImplementedException();
        }

        public CartItemEntity? GetCartItemByID(Guid CartItemID)
        {
            throw new NotImplementedException();
        }

        public List<CartItemEntity> GetCartItems()
        {
            return context.CartItem.ToList();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
