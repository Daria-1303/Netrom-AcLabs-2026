using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;
using SmartShoppingAssistant.DataAccess.Entities.Enums;

namespace SmartShoppingAssistant.DataAccess.Seed
{
    public static class PromotionSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Promotion>().HasData(
                new Promotion
                {
                    Id = 1,
                    Name = "3 tricouri naționale — cel mai ieftin e gratis",
                    Type = PromotionType.Quantity,
                    Threshold = 3,
                    Reward = PromotionReward.FreeItems,
                    RewardValue = 1,
                    ProductId = null,
                    CategoryId = 1,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 2,
                    Name = "5% reducere la comenzi peste 250 RON",
                    Type = PromotionType.CartTotal,
                    Threshold = 250.00m,
                    Reward = PromotionReward.PercentDiscount,
                    RewardValue = 5,
                    ProductId = null,
                    CategoryId = null,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 3,
                    Name = "10% reducere la comenzi peste 400 RON",
                    Type = PromotionType.CartTotal,
                    Threshold = 400.00m,
                    Reward = PromotionReward.PercentDiscount,
                    RewardValue = 10,
                    ProductId = null,
                    CategoryId = null,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 4,
                    Name = "Seara finalei: 15% la comenzi peste 700 RON",
                    Type = PromotionType.CartTotal,
                    Threshold = 700.00m,
                    Reward = PromotionReward.PercentDiscount,
                    RewardValue = 15,
                    ProductId = null,
                    CategoryId = null,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 5,
                    Name = "Super fan: 20% la comenzi peste 1000 RON",
                    Type = PromotionType.CartTotal,
                    Threshold = 1000.00m,
                    Reward = PromotionReward.PercentDiscount,
                    RewardValue = 20,
                    ProductId = null,
                    CategoryId = null,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 6,
                    Name = "2 accesorii fani — al doilea e gratis",
                    Type = PromotionType.Quantity,
                    Threshold = 2,
                    Reward = PromotionReward.FreeItems,
                    RewardValue = 1,
                    ProductId = null,
                    CategoryId = 3,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 7,
                    Name = "3 colecționabile — cel mai ieftin e gratis",
                    Type = PromotionType.Quantity,
                    Threshold = 3,
                    Reward = PromotionReward.FreeItems,
                    RewardValue = 1,
                    ProductId = null,
                    CategoryId = 4,
                    IsActive = true
                },
                new Promotion
                {
                    Id = 8,
                    Name = "Minge Oficială Trionda: 15% reducere",
                    Type = PromotionType.Quantity,
                    Threshold = 1,
                    Reward = PromotionReward.PercentDiscount,
                    RewardValue = 15,
                    ProductId = 49,
                    CategoryId = null,
                    IsActive = true
                }
            );
        }
    }
}
