using SmartShoppingAssistant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<CartItem> GetByIdWithProductAsync(int id, int userId);
        Task<List<CartItem>> GetAllWithProductsAsync(int userId);
        Task DeleteAllAsync(int userId);
        Task<List<CartItem>> GetAllWithProductWithCategoriesAsync(int userId);
    }
}
