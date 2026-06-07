using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;

namespace SmartShoppingAssistant.DataAccess.Repositories
{
    public class CartItemRepository(SmartShoppingAssistantDbContext context) : BaseRepository<CartItem>(context), ICartItemRepository
    {
        public async Task<CartItem> GetByIdWithProductAsync(int id, int userId)
        {
            var item = await context.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.Id == id && ci.UserId == userId);

            if (item == null)
                throw new KeyNotFoundException($"CartItem with ID {id} was not found.");

            return item;
        }

        public async Task<List<CartItem>> GetAllWithProductsAsync(int userId)
        {
            return await context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Product)
                    .ThenInclude(p => p.Categories)
                .ToListAsync();
        }

        public async Task DeleteAllAsync(int userId)
        {
            var items = await context.CartItems
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
            context.CartItems.RemoveRange(items);
            await context.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetAllWithProductWithCategoriesAsync(int userId)
        {
            return await context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Product)
                    .ThenInclude(p => p.Categories)
                .ToListAsync();
        }
    }
}
