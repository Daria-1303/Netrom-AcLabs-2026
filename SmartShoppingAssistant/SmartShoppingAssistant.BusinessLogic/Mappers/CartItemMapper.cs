using SmartShoppingAssistant.BusinessLogic.DTOs.CartItem;
using SmartShoppingAssistant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.Mappers
{
    public static class CartItemMapper
    {
        public static CartItemGetDTO ToCartItemGetDTO(CartItem cartItem)
        {
            return new CartItemGetDTO
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = cartItem.Product.Name,
                UnitPrice = cartItem.Product.Price,
                Quantity = cartItem.Quantity,
                ItemTypeTotal = cartItem.Product.Price * cartItem.Quantity
            };
        }

        public static CartItem ToEntity(CartItemCreateDTO dto, int userId)
        {
            return new CartItem
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UserId = userId
            };
        }
    }
}
