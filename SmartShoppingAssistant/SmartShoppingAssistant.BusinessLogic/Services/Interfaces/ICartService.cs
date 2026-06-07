using SmartShoppingAssistant.BusinessLogic.DTOs.CartItem;
using SmartShoppingAssistant.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.BusinessLogic.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartGetDTO> GetCartAsync(int userId);
        Task<CartItemGetDTO> AddItemAsync(CartItemCreateDTO dto, int userId);
        Task<CartItemGetDTO> UpdateItemQuantityAsync(int itemId, CartItemUpdateDTO dto, int userId);
        Task RemoveItemAsync(int itemId, int userId);
        Task ClearCartAsync(int userId);
        Task<AnalysisResponse> AnalyzeCartAsync(int userId);
    }
}
