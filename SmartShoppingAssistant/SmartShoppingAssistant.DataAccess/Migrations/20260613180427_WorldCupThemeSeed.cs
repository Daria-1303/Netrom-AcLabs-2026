using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartShoppingAssistant.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class WorldCupThemeSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 14 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 15 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 16 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 17 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Tricouri naționale și echipament de joc", "Tricouri & Echipament" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Mingi oficiale și accesorii de antrenament", "Mingi & Antrenament" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Fulare, steaguri, șepci și accesorii pentru suporteri", "Accesorii Fani" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Stickere, trofee, mascote și figurine", "Colecționabile" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Gustări și băuturi pentru seara meciului", "Snacks & Băuturi" });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 7 },
                    { 3, 14 },
                    { 3, 15 },
                    { 4, 16 },
                    { 4, 17 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială", "https://loremflickr.com/400/400/argentina,jersey,football?lock=1", "Tricou Național Argentina", 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială", "https://loremflickr.com/400/400/brazil,jersey,football?lock=2", "Tricou Național Brazilia", 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială", "https://loremflickr.com/400/400/france,jersey,soccer?lock=3", "Tricou Național Franța", 239.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou suporter, ediție specială", "https://loremflickr.com/400/400/jersey,football,red?lock=4", "Tricou Național România", 219.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Șort de joc, material respirabil", "https://loremflickr.com/400/400/football,shorts,sport?lock=5", "Șort Fotbal Pro", 89.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Jambiere de joc, mărime universală", "https://loremflickr.com/400/400/football,socks,sport?lock=6", "Jambiere Fotbal", 39.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Replica mingii oficiale de turneu", "https://loremflickr.com/400/400/soccer,ball?lock=7", "Minge Oficială Cupa Mondială", 179.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Minge rezistentă pentru antrenamente", "https://loremflickr.com/400/400/football,ball,training?lock=8", "Minge Antrenament", 69.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Pompă pentru mingi cu indicator de presiune", "https://loremflickr.com/400/400/ball,pump?lock=9", "Pompă cu Manometru", 34.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Set 20 conuri pentru exerciții", "https://loremflickr.com/400/400/training,cones,football?lock=10", "Set Conuri Antrenament", 49.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Fular tricotat pentru tribună", "https://loremflickr.com/400/400/scarf,football,fan?lock=11", "Fular Suporter", 59.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Steag 150x90cm pentru suporteri", "https://loremflickr.com/400/400/flag,stadium,fans?lock=12", "Steag Mare Național", 44.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Șapcă cu cozoroc, ediție turneu", "https://loremflickr.com/400/400/cap,hat,sport?lock=13", "Șapcă Suporter", 49.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Set vopsele pentru susținere la stadion", "https://loremflickr.com/400/400/facepaint,fan,football?lock=14", "Vopsea Față Tricolor", 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Trompetă pentru atmosfera din tribună", "https://loremflickr.com/400/400/vuvuzela,stadium,fans?lock=15", "Trompetă Suporter", 24.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Album oficial pentru colecția de stickere", "https://loremflickr.com/400/400/sticker,album,collection?lock=16", "Album Stickere Mondial", 39.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Replică decorativă a trofeului", "https://loremflickr.com/400/400/trophy,gold,football?lock=17", "Replică Trofeu Aurit", 129.99m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 18, "Mascota oficială a turneului, de pluș", "https://loremflickr.com/400/400/mascot,plush,toy?lock=18", "Mascotă de Pluș", 79.99m },
                    { 19, "Bax 6 sticle pentru seara meciului", "https://loremflickr.com/400/400/beer,bottles?lock=19", "Set 6 Beri Blonde", 34.99m },
                    { 20, "Pungă mare de chipsuri pentru petrecere", "https://loremflickr.com/400/400/chips,snacks?lock=20", "Chipsuri Party Mix", 14.99m },
                    { 21, "Set 3 pungi popcorn, gata în 3 minute", "https://loremflickr.com/400/400/popcorn,snack?lock=21", "Popcorn pentru Microunde", 8.99m },
                    { 22, "Bax 6 doze băuturi carbogazoase", "https://loremflickr.com/400/400/soda,drinks,cans?lock=22", "Set 6 Băuturi Răcoritoare", 29.99m }
                });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Name", "ProductId", "Threshold" },
                values: new object[] { 1, "Cumpără 3 tricouri, cel mai ieftin e gratis", null, 3m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Threshold" },
                values: new object[] { "10% reducere la comenzi peste 300 RON", 300.00m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 3, "3 accesorii fani — cel mai ieftin gratis" });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "ProductId", "Threshold" },
                values: new object[] { 5, "Pachet meci: la 4 snacks, 1 gratis", null, 4m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Name", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { null, "Seara finalei: 15% la comenzi peste 500 RON", 1, 15, 500.00m, 1 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "RewardValue", "Threshold" },
                values: new object[] { "5% reducere la comenzi peste 150 RON", 5, 150.00m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "Reward", "RewardValue", "Threshold" },
                values: new object[] { "2+ mingi oficiale: 15% reducere", 1, 15, 2m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "ProductId", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { "Cumpără 5 Chipsuri Party Mix, 1 gratis", 20, 0, 1, 5m, 0 });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 4, 18 },
                    { 5, 19 },
                    { 5, 20 },
                    { 5, 21 },
                    { 5, 22 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 14 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 15 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 16 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 17 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 18 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 19 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 20 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 21 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 22 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Băuturi răcoritoare și sucuri", "Băuturi" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Gustări și dulciuri", "Snacks" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Produse lactate", "Lactate" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Pâine, cozonaci și produse de patiserie", "Panificație" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Legume și fructe proaspete", "Legume și Fructe" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 6, "Produse din carne și mezeluri", "Carne și Mezeluri" },
                    { 7, "Produse de igienă și cosmetice", "Îngrijire Personală" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 2, 3 },
                    { 3, 4 },
                    { 1, 7 },
                    { 4, 14 },
                    { 4, 15 },
                    { 5, 16 },
                    { 5, 17 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Băutură carbogazoasă", null, "Coca Cola 500ml", 5.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Băutură carbogazoasă", null, "Pepsi 500ml", 5.49m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Chipsuri cu sare", null, "Lay's Sare", 7.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Iaurt 400g", null, "Iaurt natural", 4.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Băutură carbogazoasă cu aromă de portocale", null, "Fanta Portocale 500ml", 5.49m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Băutură carbogazoasă cu lămâie și lime", null, "Sprite 500ml", 5.49m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Apă minerală plată", null, "Apă Plată Bucovina 2L", 3.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Chipsuri crocante în tub", null, "Pringles Original 165g", 12.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Chipsuri de porumb cu aromă de brânză", null, "Doritos Nacho Cheese 150g", 9.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Ciocolată cu lapte alpin", null, "Milka Lapte Alpin 100g", 6.49m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Lapte proaspăt de vacă 3.5% grăsime", null, "Lapte Zuzu 1L", 7.29m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Brânză telemea de vacă", null, "Brânză Telemea 400g", 14.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Smântână pentru gătit și salate", null, "Smântână 20% 400g", 8.49m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Pâine albă moale, feliată", null, "Pâine Albă Feliată 500g", 5.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Croissant cu unt, proaspăt", null, "Croissant Simplu", 3.49m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Mere românești, soiul Ionatan", null, "Mere Ionatan 1kg", 6.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Roșii cherry proaspete", null, "Roșii Cherry 500g", 8.99m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Name", "ProductId", "Threshold" },
                values: new object[] { null, "Cumpără 5 Coca-Cola, primești 1 gratis", 1, 5m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Threshold" },
                values: new object[] { "10% reducere la comenzi peste 100 RON", 100.00m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 2, "Cumpără 3 Snacks-uri, primești 1 gratis" });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "ProductId", "Threshold" },
                values: new object[] { null, "Cumpără 2 Pepsi, primești 1 gratis", 2, 2m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Name", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { 3, "Cumpără 4 lactate, primești 1 gratis", 0, 1, 4m, 0 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "RewardValue", "Threshold" },
                values: new object[] { "20% reducere la comenzi peste 200 RON", 20, 200.00m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "Reward", "RewardValue", "Threshold" },
                values: new object[] { "Cumpără 3 Ape Plată, primești 1 gratis", 0, 1, 3m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "ProductId", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { "5% reducere la comenzi peste 50 RON", null, 1, 5, 50.00m, 1 });
        }
    }
}
