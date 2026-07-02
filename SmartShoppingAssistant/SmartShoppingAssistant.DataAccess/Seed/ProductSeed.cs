using Microsoft.EntityFrameworkCore;
using SmartShoppingAssistant.DataAccess.Entities;

namespace SmartShoppingAssistant.DataAccess.Seed
{
    public static class ProductSeed
    {
        // Tricouri & merch: poze reale de pe FIFA Store (CDN Shopify, hotlink-abil).
        private static string Fifa(string file) => $"https://store.fifa.com/cdn/shop/files/{file}?width=533";
        // Tricouri fără poză FIFA dedicată: steagul național (flagcdn, foarte fiabil).
        private static string Flag(string code) => $"https://flagcdn.com/w320/{code}.png";
        // Colecționabile Panini (CDN Magento, hotlink-abil).
        private static string Panini(string path) =>
            $"https://www.panini.ro/media/catalog/product/{path}?quality=80&bg-color=255,255,255&fit=bounds&height=400&width=400&canvas=400:400";

        private const string JerseyDesc = "Tricou oficial replica, ediția Cupa Mondială 2026";

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                // ===== Tricouri Naționale (categoria 1) — poze reale FIFA =====
                new Product { Id = 1, Name = "Tricou Național Argentina", Description = JerseyDesc, Price = 259.99m, ImageUrl = Fifa("image_ab8f6ea2-c16a-4882-822e-aa52e9506054.png") },
                new Product { Id = 2, Name = "Tricou Național Brazilia", Description = JerseyDesc, Price = 259.99m, ImageUrl = Fifa("image_02047e33-3b4e-41f2-869b-ac361dd4b283.png") },
                new Product { Id = 3, Name = "Tricou Național Franța", Description = JerseyDesc, Price = 249.99m, ImageUrl = Fifa("image_f7f029ac-d57c-4aab-ab4e-45d5d056ac29.jpg") },
                new Product { Id = 4, Name = "Tricou Național Spania", Description = JerseyDesc, Price = 249.99m, ImageUrl = Fifa("image_b3db0408-344c-4ba2-b34b-5bedc55c84f1.png") },
                new Product { Id = 5, Name = "Tricou Național Anglia", Description = JerseyDesc, Price = 249.99m, ImageUrl = Fifa("image_54f59fd1-10b1-4430-b581-a46bbd87255c.jpg") },
                new Product { Id = 6, Name = "Tricou Național Germania", Description = JerseyDesc, Price = 249.99m, ImageUrl = Fifa("KD8363_1_APPAREL_Photography_FrontCenterView_white.jpg") },
                new Product { Id = 7, Name = "Tricou Național Portugalia", Description = JerseyDesc, Price = 249.99m, ImageUrl = Fifa("FIFAMZ0368-1.png") },
                new Product { Id = 8, Name = "Tricou Național Statele Unite", Description = JerseyDesc, Price = 239.99m, ImageUrl = Fifa("image_c78fa204-51ce-440a-a410-c8b8a4258deb.jpg") },
                new Product { Id = 9, Name = "Tricou Național Mexic", Description = JerseyDesc, Price = 239.99m, ImageUrl = Fifa("image_09d4d8ad-c993-4e39-af1c-caed11623a11.png") },
                new Product { Id = 10, Name = "Tricou Național Italia", Description = JerseyDesc, Price = 249.99m, ImageUrl = Fifa("image_3bf28b9c-e9f3-49e3-ac48-fd7473b92b15.png") },
                new Product { Id = 11, Name = "Tricou Național Belgia", Description = JerseyDesc, Price = 239.99m, ImageUrl = Fifa("image_0cf9c430-1214-48a1-a3ee-cfd8b32a882c.png") },
                new Product { Id = 12, Name = "Tricou Național Columbia", Description = JerseyDesc, Price = 229.99m, ImageUrl = Fifa("image_695e8c00-a951-4ed8-af81-899e39ed5503.png") },
                new Product { Id = 13, Name = "Tricou Național Nigeria", Description = JerseyDesc, Price = 229.99m, ImageUrl = Fifa("image_1a0d5dda-c1e6-4621-9fa7-c0079d201af8.jpg") },
                new Product { Id = 14, Name = "Tricou Național Paraguay", Description = JerseyDesc, Price = 219.99m, ImageUrl = Fifa("FIFAMZ0420-1.png") },
                new Product { Id = 15, Name = "Tricou Național Africa de Sud", Description = JerseyDesc, Price = 219.99m, ImageUrl = Fifa("KY2217_1_APPAREL_Photography_FrontCenterView_white.jpg") },

                // ===== Tricouri Naționale (categoria 1) — steag național =====
                new Product { Id = 16, Name = "Tricou Național Țările de Jos", Description = JerseyDesc, Price = 239.99m, ImageUrl = Flag("nl") },
                new Product { Id = 17, Name = "Tricou Național Croația", Description = JerseyDesc, Price = 229.99m, ImageUrl = Flag("hr") },
                new Product { Id = 18, Name = "Tricou Național Uruguay", Description = JerseyDesc, Price = 229.99m, ImageUrl = Flag("uy") },
                new Product { Id = 19, Name = "Tricou Național Canada", Description = JerseyDesc, Price = 229.99m, ImageUrl = Flag("ca") },
                new Product { Id = 20, Name = "Tricou Național Japonia", Description = JerseyDesc, Price = 229.99m, ImageUrl = Flag("jp") },
                new Product { Id = 21, Name = "Tricou Național Coreea de Sud", Description = JerseyDesc, Price = 219.99m, ImageUrl = Flag("kr") },
                new Product { Id = 22, Name = "Tricou Național Maroc", Description = JerseyDesc, Price = 219.99m, ImageUrl = Flag("ma") },
                new Product { Id = 23, Name = "Tricou Național Senegal", Description = JerseyDesc, Price = 219.99m, ImageUrl = Flag("sn") },
                new Product { Id = 24, Name = "Tricou Național Ghana", Description = JerseyDesc, Price = 209.99m, ImageUrl = Flag("gh") },
                new Product { Id = 25, Name = "Tricou Național Camerun", Description = JerseyDesc, Price = 209.99m, ImageUrl = Flag("cm") },
                new Product { Id = 26, Name = "Tricou Național Coasta de Fildeș", Description = JerseyDesc, Price = 209.99m, ImageUrl = Flag("ci") },
                new Product { Id = 27, Name = "Tricou Național Egipt", Description = JerseyDesc, Price = 209.99m, ImageUrl = Flag("eg") },
                new Product { Id = 28, Name = "Tricou Național Tunisia", Description = JerseyDesc, Price = 199.99m, ImageUrl = Flag("tn") },
                new Product { Id = 29, Name = "Tricou Național Algeria", Description = JerseyDesc, Price = 199.99m, ImageUrl = Flag("dz") },
                new Product { Id = 30, Name = "Tricou Național Australia", Description = JerseyDesc, Price = 219.99m, ImageUrl = Flag("au") },
                new Product { Id = 31, Name = "Tricou Național Arabia Saudită", Description = JerseyDesc, Price = 209.99m, ImageUrl = Flag("sa") },
                new Product { Id = 32, Name = "Tricou Național Iran", Description = JerseyDesc, Price = 199.99m, ImageUrl = Flag("ir") },
                new Product { Id = 33, Name = "Tricou Național Qatar", Description = JerseyDesc, Price = 209.99m, ImageUrl = Flag("qa") },
                new Product { Id = 34, Name = "Tricou Național Ecuador", Description = JerseyDesc, Price = 209.99m, ImageUrl = Flag("ec") },
                new Product { Id = 35, Name = "Tricou Național Peru", Description = JerseyDesc, Price = 209.99m, ImageUrl = Flag("pe") },
                new Product { Id = 36, Name = "Tricou Național Chile", Description = JerseyDesc, Price = 219.99m, ImageUrl = Flag("cl") },
                new Product { Id = 37, Name = "Tricou Național Danemarca", Description = JerseyDesc, Price = 229.99m, ImageUrl = Flag("dk") },
                new Product { Id = 38, Name = "Tricou Național Elveția", Description = JerseyDesc, Price = 229.99m, ImageUrl = Flag("ch") },
                new Product { Id = 39, Name = "Tricou Național Polonia", Description = JerseyDesc, Price = 229.99m, ImageUrl = Flag("pl") },
                new Product { Id = 40, Name = "Tricou Național Serbia", Description = JerseyDesc, Price = 219.99m, ImageUrl = Flag("rs") },
                new Product { Id = 41, Name = "Tricou Național Austria", Description = JerseyDesc, Price = 219.99m, ImageUrl = Flag("at") },
                new Product { Id = 42, Name = "Tricou Național Scoția", Description = JerseyDesc, Price = 229.99m, ImageUrl = Flag("gb-sct") },
                new Product { Id = 43, Name = "Tricou Național Țara Galilor", Description = JerseyDesc, Price = 219.99m, ImageUrl = Flag("gb-wls") },
                new Product { Id = 44, Name = "Tricou Național Norvegia", Description = JerseyDesc, Price = 229.99m, ImageUrl = Flag("no") },
                new Product { Id = 45, Name = "Tricou Național Suedia", Description = JerseyDesc, Price = 229.99m, ImageUrl = Flag("se") },
                new Product { Id = 46, Name = "Tricou Național Turcia", Description = JerseyDesc, Price = 219.99m, ImageUrl = Flag("tr") },
                new Product { Id = 47, Name = "Tricou Național Ucraina", Description = JerseyDesc, Price = 219.99m, ImageUrl = Flag("ua") },
                new Product { Id = 48, Name = "Tricou Național România", Description = "Tricou suporter România, ediția Cupa Mondială 2026", Price = 219.99m, ImageUrl = Flag("ro") },

                // ===== Mingi & Echipament (categoria 2) =====
                new Product { Id = 49, Name = "Minge Oficială Trionda", Description = "Replica mingii oficiale de turneu Trionda", Price = 179.99m, ImageUrl = Fifa("image_9c1a59b6-9fe5-480d-845d-b77dfc2c839c.png") },
                new Product { Id = 50, Name = "Minge Trionda Pro", Description = "Minge de joc Trionda Pro, performanță înaltă", Price = 149.99m, ImageUrl = Fifa("image_acc6fba9-fdb5-46ad-8325-7c31b56ebd97.png") },
                new Product { Id = 51, Name = "Minge Trionda Competiție", Description = "Minge Trionda pentru competiție", Price = 129.99m, ImageUrl = Fifa("image_b74b2afa-3620-4e7c-af50-00dcc69d343b.png") },
                new Product { Id = 52, Name = "Mini-minge Trionda", Description = "Mini-minge de colecție Trionda", Price = 59.99m, ImageUrl = Fifa("image_cb4ed0a3-31c3-4106-be94-dceb214b7337.png") },
                new Product { Id = 53, Name = "Mini-minge Argentina", Description = "Mini-minge AFA Argentina, ediție turneu", Price = 64.99m, ImageUrl = Fifa("FIFAEQ013000-1.jpg") },
                new Product { Id = 54, Name = "Mini-minge Mexic", Description = "Mini-minge Mexic, ediție turneu", Price = 64.99m, ImageUrl = Fifa("image_31911b92-ff2a-45f4-853b-0f7f5d6a229c.png") },
                new Product { Id = 55, Name = "Breloc Minge cu Sclipici", Description = "Breloc decorativ în formă de minge", Price = 29.99m, ImageUrl = Fifa("Untitleddesign_2.png") },

                // ===== Accesorii Fani (categoria 3) — fulare =====
                new Product { Id = 56, Name = "Fular Mexic 'We Are'", Description = "Fular oficial suporter, ediția Cupa Mondială 2026", Price = 79.99m, ImageUrl = Fifa("image_8e3e9958-8194-41ec-a67e-1ce56d64d40a.png") },
                new Product { Id = 57, Name = "Fular Anglia 'We Are'", Description = "Fular oficial suporter, ediția Cupa Mondială 2026", Price = 79.99m, ImageUrl = Fifa("image_59bb7189-4e92-4068-87b4-3caa8d17a2f6.png") },
                new Product { Id = 58, Name = "Fular Franța 'We Are'", Description = "Fular oficial suporter, ediția Cupa Mondială 2026", Price = 79.99m, ImageUrl = Fifa("image_1c549abc-9b97-4f9f-b28f-aa2bd3c125c2.jpg") },
                new Product { Id = 59, Name = "Fular Spania 'We Are'", Description = "Fular oficial suporter, ediția Cupa Mondială 2026", Price = 79.99m, ImageUrl = Fifa("image_4d4a4fb6-d072-4612-9b1b-6133c55d2ee2.jpg") },
                new Product { Id = 60, Name = "Fular Portugalia 'We Are'", Description = "Fular oficial suporter, ediția Cupa Mondială 2026", Price = 79.99m, ImageUrl = Fifa("image_16a9dbc7-07fb-460c-a622-c74e47fbe5c2.jpg") },
                new Product { Id = 61, Name = "Fular Germania 'We Are'", Description = "Fular oficial suporter, ediția Cupa Mondială 2026", Price = 79.99m, ImageUrl = Fifa("image_8fabf9f0-77e6-4532-9ab6-134da3792d3b.jpg") },
                new Product { Id = 62, Name = "Fular Turneu Oficial", Description = "Fular cu designul oficial al turneului", Price = 69.99m, ImageUrl = Fifa("image_94d81176-cac3-4b5f-9828-d8171386f304.png") },

                // ===== Accesorii Fani (categoria 3) — șepci =====
                new Product { Id = 63, Name = "Șapcă Franța", Description = "Șapcă oficială echipa națională", Price = 89.99m, ImageUrl = Fifa("France_4.jpg") },
                new Product { Id = 64, Name = "Șapcă Brazilia", Description = "Șapcă oficială echipa națională", Price = 89.99m, ImageUrl = Fifa("Brazil_3.jpg") },
                new Product { Id = 65, Name = "Șapcă Mexic", Description = "Șapcă oficială echipa națională", Price = 89.99m, ImageUrl = Fifa("Mexico_4.jpg") },
                new Product { Id = 66, Name = "Șapcă Anglia", Description = "Șapcă oficială echipa națională", Price = 89.99m, ImageUrl = Fifa("England_3.jpg") },
                new Product { Id = 67, Name = "Șapcă Croația", Description = "Șapcă oficială echipa națională", Price = 89.99m, ImageUrl = Fifa("Croatia_3.jpg") },

                // ===== Colecționabile (categoria 4) — mascote + Panini =====
                new Product { Id = 68, Name = "Mascotă de Pluș USA 25cm", Description = "Mascota oficială a turneului, varianta USA", Price = 119.99m, ImageUrl = Fifa("image_45665d36-9cd6-403e-9029-a8ad77c8379c.jpg") },
                new Product { Id = 69, Name = "Mascotă de Pluș Mexic 25cm", Description = "Mascota oficială a turneului, varianta Mexic", Price = 119.99m, ImageUrl = Fifa("image_8c3712e0-ce68-4e13-adb2-9576753635f3.jpg") },
                new Product { Id = 70, Name = "Mascotă Squishmallows Canada", Description = "Mascotă de pluș Squishmallows, varianta Canada", Price = 99.99m, ImageUrl = Fifa("image_7ec8497a-6355-4871-8694-a6feec5af3c1.jpg") },
                new Product { Id = 71, Name = "Set Start Album Stickere", Description = "Album Panini + plicuri de start, Cupa Mondială 2026", Price = 24.99m, ImageUrl = Panini("0/0/005460baggexp_0_1.jpg") },
                new Product { Id = 72, Name = "Cutie 144 Plicuri Stickere", Description = "Cutie completă cu 144 de plicuri Panini", Price = 199.99m, ImageUrl = Panini("0/0/005460box144oe_0_en.jpg") },
                new Product { Id = 73, Name = "Tin Colecție Stickere", Description = "Cutie metalică de colecție cu plicuri Panini", Price = 49.99m, ImageUrl = Panini("b/u/bundle005460b50cptin_0.jpg") },
                new Product { Id = 74, Name = "Pachet 50 Plicuri Stickere", Description = "Bundle cu 50 de plicuri Panini", Price = 89.99m, ImageUrl = Panini("b/u/bundle005460b7bexp50_0.jpg") }
            );
        }
    }
}
