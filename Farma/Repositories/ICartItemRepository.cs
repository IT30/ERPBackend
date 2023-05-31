using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public interface ICartItemRepository
    {
        List<CartItemEntity> GetCartItems();

        CartItemEntity? GetCartItemByID(Guid CartItemID);

        List<CartItemEntity> GetCartItemsByUser(Guid IDUser);

        CartItemDTO CreateCartItem(CartItemCreateDTO cartItemCreateDTO);

        void DeleteCartItem(Guid CartItemID);

        bool SaveChanges();
    }
}
