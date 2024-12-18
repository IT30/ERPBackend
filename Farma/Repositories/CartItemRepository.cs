﻿using AutoMapper;
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
            CartItemEntity cartItem = mapper.Map<CartItemEntity>(cartItemCreateDTO);
            cartItem.IDCartItem = Guid.NewGuid();
            context.Add(cartItem);
            return mapper.Map<CartItemDTO>(cartItem);
        }

        public void DeleteCartItem(Guid CartItemID)
        {
            CartItemEntity? cartItem = GetCartItemByID(CartItemID);
            if (cartItem != null)
                context.Remove(cartItem);
        }

        public CartItemEntity? GetCartItemByID(Guid CartItemID)
        {
            return context.CartItem.FirstOrDefault(e => e.IDCartItem == CartItemID);
        }

        public List<CartItemEntity> GetCartItems()
        {
            return context.CartItem.ToList();
        }

        public List<CartItemEntity> GetCartItemsByUser(Guid UserID)
        {
            return context.CartItem.Where(e => e.IDUser == UserID).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
