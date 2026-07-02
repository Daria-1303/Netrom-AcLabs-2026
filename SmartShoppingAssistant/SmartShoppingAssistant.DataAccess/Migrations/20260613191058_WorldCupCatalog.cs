using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartShoppingAssistant.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class WorldCupCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 11 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 12 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 13 });

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
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Tricouri oficiale ale echipelor naționale, Cupa Mondială 2026", "Tricouri Naționale" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Mingi oficiale Trionda și accesorii de joc", "Mingi & Echipament" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Fulare și șepci pentru suporteri");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Mascote de pluș și stickere Panini");

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 7 },
                    { 1, 8 },
                    { 1, 9 },
                    { 1, 10 },
                    { 1, 11 },
                    { 1, 12 },
                    { 1, 13 },
                    { 1, 14 },
                    { 1, 15 },
                    { 1, 16 },
                    { 1, 17 },
                    { 1, 18 },
                    { 1, 19 },
                    { 1, 20 },
                    { 1, 21 },
                    { 1, 22 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_ab8f6ea2-c16a-4882-822e-aa52e9506054.png?width=533", 259.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_02047e33-3b4e-41f2-869b-ac361dd4b283.png?width=533", 259.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_f7f029ac-d57c-4aab-ab4e-45d5d056ac29.jpg?width=533", 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_b3db0408-344c-4ba2-b34b-5bedc55c84f1.png?width=533", "Tricou Național Spania", 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_54f59fd1-10b1-4430-b581-a46bbd87255c.jpg?width=533", "Tricou Național Anglia", 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/KD8363_1_APPAREL_Photography_FrontCenterView_white.jpg?width=533", "Tricou Național Germania", 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/FIFAMZ0368-1.png?width=533", "Tricou Național Portugalia", 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_c78fa204-51ce-440a-a410-c8b8a4258deb.jpg?width=533", "Tricou Național Statele Unite", 239.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_09d4d8ad-c993-4e39-af1c-caed11623a11.png?width=533", "Tricou Național Mexic", 239.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_3bf28b9c-e9f3-49e3-ac48-fd7473b92b15.png?width=533", "Tricou Național Italia", 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_0cf9c430-1214-48a1-a3ee-cfd8b32a882c.png?width=533", "Tricou Național Belgia", 239.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_695e8c00-a951-4ed8-af81-899e39ed5503.png?width=533", "Tricou Național Columbia", 229.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_1a0d5dda-c1e6-4621-9fa7-c0079d201af8.jpg?width=533", "Tricou Național Nigeria", 229.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/FIFAMZ0420-1.png?width=533", "Tricou Național Paraguay", 219.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/KY2217_1_APPAREL_Photography_FrontCenterView_white.jpg?width=533", "Tricou Național Africa de Sud", 219.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/nl.png", "Tricou Național Țările de Jos", 239.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/hr.png", "Tricou Național Croația", 229.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/uy.png", "Tricou Național Uruguay", 229.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/ca.png", "Tricou Național Canada", 229.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/jp.png", "Tricou Național Japonia", 229.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/kr.png", "Tricou Național Coreea de Sud", 219.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/ma.png", "Tricou Național Maroc", 219.99m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 23, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/sn.png", "Tricou Național Senegal", 219.99m },
                    { 24, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/gh.png", "Tricou Național Ghana", 209.99m },
                    { 25, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/cm.png", "Tricou Național Camerun", 209.99m },
                    { 26, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/ci.png", "Tricou Național Coasta de Fildeș", 209.99m },
                    { 27, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/eg.png", "Tricou Național Egipt", 209.99m },
                    { 28, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/tn.png", "Tricou Național Tunisia", 199.99m },
                    { 29, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/dz.png", "Tricou Național Algeria", 199.99m },
                    { 30, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/au.png", "Tricou Național Australia", 219.99m },
                    { 31, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/sa.png", "Tricou Național Arabia Saudită", 209.99m },
                    { 32, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/ir.png", "Tricou Național Iran", 199.99m },
                    { 33, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/qa.png", "Tricou Național Qatar", 209.99m },
                    { 34, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/ec.png", "Tricou Național Ecuador", 209.99m },
                    { 35, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/pe.png", "Tricou Național Peru", 209.99m },
                    { 36, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/cl.png", "Tricou Național Chile", 219.99m },
                    { 37, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/dk.png", "Tricou Național Danemarca", 229.99m },
                    { 38, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/ch.png", "Tricou Național Elveția", 229.99m },
                    { 39, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/pl.png", "Tricou Național Polonia", 229.99m },
                    { 40, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/rs.png", "Tricou Național Serbia", 219.99m },
                    { 41, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/at.png", "Tricou Național Austria", 219.99m },
                    { 42, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/gb-sct.png", "Tricou Național Scoția", 229.99m },
                    { 43, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/gb-wls.png", "Tricou Național Țara Galilor", 219.99m },
                    { 44, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/no.png", "Tricou Național Norvegia", 229.99m },
                    { 45, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/se.png", "Tricou Național Suedia", 229.99m },
                    { 46, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/tr.png", "Tricou Național Turcia", 219.99m },
                    { 47, "Tricou oficial replica, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/ua.png", "Tricou Național Ucraina", 219.99m },
                    { 48, "Tricou suporter România, ediția Cupa Mondială 2026", "https://flagcdn.com/w320/ro.png", "Tricou Național România", 219.99m },
                    { 49, "Replica mingii oficiale de turneu Trionda", "https://store.fifa.com/cdn/shop/files/image_9c1a59b6-9fe5-480d-845d-b77dfc2c839c.png?width=533", "Minge Oficială Trionda", 179.99m },
                    { 50, "Minge de joc Trionda Pro, performanță înaltă", "https://store.fifa.com/cdn/shop/files/image_acc6fba9-fdb5-46ad-8325-7c31b56ebd97.png?width=533", "Minge Trionda Pro", 149.99m },
                    { 51, "Minge Trionda pentru competiție", "https://store.fifa.com/cdn/shop/files/image_b74b2afa-3620-4e7c-af50-00dcc69d343b.png?width=533", "Minge Trionda Competiție", 129.99m },
                    { 52, "Mini-minge de colecție Trionda", "https://store.fifa.com/cdn/shop/files/image_cb4ed0a3-31c3-4106-be94-dceb214b7337.png?width=533", "Mini-minge Trionda", 59.99m },
                    { 53, "Mini-minge AFA Argentina, ediție turneu", "https://store.fifa.com/cdn/shop/files/FIFAEQ013000-1.jpg?width=533", "Mini-minge Argentina", 64.99m },
                    { 54, "Mini-minge Mexic, ediție turneu", "https://store.fifa.com/cdn/shop/files/image_31911b92-ff2a-45f4-853b-0f7f5d6a229c.png?width=533", "Mini-minge Mexic", 64.99m },
                    { 55, "Breloc decorativ în formă de minge", "https://store.fifa.com/cdn/shop/files/Untitleddesign_2.png?width=533", "Breloc Minge cu Sclipici", 29.99m },
                    { 56, "Fular oficial suporter, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_8e3e9958-8194-41ec-a67e-1ce56d64d40a.png?width=533", "Fular Mexic 'We Are'", 79.99m },
                    { 57, "Fular oficial suporter, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_59bb7189-4e92-4068-87b4-3caa8d17a2f6.png?width=533", "Fular Anglia 'We Are'", 79.99m },
                    { 58, "Fular oficial suporter, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_1c549abc-9b97-4f9f-b28f-aa2bd3c125c2.jpg?width=533", "Fular Franța 'We Are'", 79.99m },
                    { 59, "Fular oficial suporter, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_4d4a4fb6-d072-4612-9b1b-6133c55d2ee2.jpg?width=533", "Fular Spania 'We Are'", 79.99m },
                    { 60, "Fular oficial suporter, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_16a9dbc7-07fb-460c-a622-c74e47fbe5c2.jpg?width=533", "Fular Portugalia 'We Are'", 79.99m },
                    { 61, "Fular oficial suporter, ediția Cupa Mondială 2026", "https://store.fifa.com/cdn/shop/files/image_8fabf9f0-77e6-4532-9ab6-134da3792d3b.jpg?width=533", "Fular Germania 'We Are'", 79.99m },
                    { 62, "Fular cu designul oficial al turneului", "https://store.fifa.com/cdn/shop/files/image_94d81176-cac3-4b5f-9828-d8171386f304.png?width=533", "Fular Turneu Oficial", 69.99m },
                    { 63, "Șapcă oficială echipa națională", "https://store.fifa.com/cdn/shop/files/France_4.jpg?width=533", "Șapcă Franța", 89.99m },
                    { 64, "Șapcă oficială echipa națională", "https://store.fifa.com/cdn/shop/files/Brazil_3.jpg?width=533", "Șapcă Brazilia", 89.99m },
                    { 65, "Șapcă oficială echipa națională", "https://store.fifa.com/cdn/shop/files/Mexico_4.jpg?width=533", "Șapcă Mexic", 89.99m },
                    { 66, "Șapcă oficială echipa națională", "https://store.fifa.com/cdn/shop/files/England_3.jpg?width=533", "Șapcă Anglia", 89.99m },
                    { 67, "Șapcă oficială echipa națională", "https://store.fifa.com/cdn/shop/files/Croatia_3.jpg?width=533", "Șapcă Croația", 89.99m },
                    { 68, "Mascota oficială a turneului, varianta USA", "https://store.fifa.com/cdn/shop/files/image_45665d36-9cd6-403e-9029-a8ad77c8379c.jpg?width=533", "Mascotă de Pluș USA 25cm", 119.99m },
                    { 69, "Mascota oficială a turneului, varianta Mexic", "https://store.fifa.com/cdn/shop/files/image_8c3712e0-ce68-4e13-adb2-9576753635f3.jpg?width=533", "Mascotă de Pluș Mexic 25cm", 119.99m },
                    { 70, "Mascotă de pluș Squishmallows, varianta Canada", "https://store.fifa.com/cdn/shop/files/image_7ec8497a-6355-4871-8694-a6feec5af3c1.jpg?width=533", "Mascotă Squishmallows Canada", 99.99m },
                    { 71, "Album Panini + plicuri de start, Cupa Mondială 2026", "https://www.panini.ro/media/catalog/product/0/0/005460baggexp_0_1.jpg?quality=80&bg-color=255,255,255&fit=bounds&height=400&width=400&canvas=400:400", "Set Start Album Stickere", 24.99m },
                    { 72, "Cutie completă cu 144 de plicuri Panini", "https://www.panini.ro/media/catalog/product/0/0/005460box144oe_0_en.jpg?quality=80&bg-color=255,255,255&fit=bounds&height=400&width=400&canvas=400:400", "Cutie 144 Plicuri Stickere", 199.99m },
                    { 73, "Cutie metalică de colecție cu plicuri Panini", "https://www.panini.ro/media/catalog/product/b/u/bundle005460b50cptin_0.jpg?quality=80&bg-color=255,255,255&fit=bounds&height=400&width=400&canvas=400:400", "Tin Colecție Stickere", 49.99m },
                    { 74, "Bundle cu 50 de plicuri Panini", "https://www.panini.ro/media/catalog/product/b/u/bundle005460b7bexp50_0.jpg?quality=80&bg-color=255,255,255&fit=bounds&height=400&width=400&canvas=400:400", "Pachet 50 Plicuri Stickere", 89.99m }
                });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "3 tricouri naționale — cel mai ieftin e gratis");

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "RewardValue", "Threshold" },
                values: new object[] { "5% reducere la comenzi peste 250 RON", 5, 250.00m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { null, "10% reducere la comenzi peste 400 RON", 1, 10, 400.00m, 1 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { null, "Seara finalei: 15% la comenzi peste 700 RON", 1, 15, 700.00m, 1 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "RewardValue", "Threshold" },
                values: new object[] { "Super fan: 20% la comenzi peste 1000 RON", 20, 1000.00m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Name", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { 3, "2 accesorii fani — al doilea e gratis", 0, 1, 2m, 0 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Name", "ProductId", "Reward", "RewardValue", "Threshold" },
                values: new object[] { 4, "3 colecționabile — cel mai ieftin e gratis", null, 0, 1, 3m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "ProductId", "Reward", "RewardValue", "Threshold" },
                values: new object[] { "Minge Oficială Trionda: 15% reducere", 49, 1, 15, 1m });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 23 },
                    { 1, 24 },
                    { 1, 25 },
                    { 1, 26 },
                    { 1, 27 },
                    { 1, 28 },
                    { 1, 29 },
                    { 1, 30 },
                    { 1, 31 },
                    { 1, 32 },
                    { 1, 33 },
                    { 1, 34 },
                    { 1, 35 },
                    { 1, 36 },
                    { 1, 37 },
                    { 1, 38 },
                    { 1, 39 },
                    { 1, 40 },
                    { 1, 41 },
                    { 1, 42 },
                    { 1, 43 },
                    { 1, 44 },
                    { 1, 45 },
                    { 1, 46 },
                    { 1, 47 },
                    { 1, 48 },
                    { 2, 49 },
                    { 2, 50 },
                    { 2, 51 },
                    { 2, 52 },
                    { 2, 53 },
                    { 2, 54 },
                    { 2, 55 },
                    { 3, 56 },
                    { 3, 57 },
                    { 3, 58 },
                    { 3, 59 },
                    { 3, 60 },
                    { 3, 61 },
                    { 3, 62 },
                    { 3, 63 },
                    { 3, 64 },
                    { 3, 65 },
                    { 3, 66 },
                    { 3, 67 },
                    { 4, 68 },
                    { 4, 69 },
                    { 4, 70 },
                    { 4, 71 },
                    { 4, 72 },
                    { 4, 73 },
                    { 4, 74 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 12 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 13 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 14 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 15 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 16 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 17 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 18 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 19 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 20 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 21 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 22 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 23 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 24 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 25 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 26 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 27 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 28 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 29 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 30 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 31 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 32 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 33 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 34 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 35 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 36 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 37 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 38 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 39 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 40 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 41 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 42 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 43 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 44 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 45 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 46 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 47 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 48 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 49 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 50 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 51 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 52 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 53 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 54 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 55 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 56 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 57 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 58 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 59 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 60 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 61 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 62 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 63 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 64 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 65 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 66 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 67 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 68 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 69 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 70 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 71 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 72 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 73 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 74 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 74);

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
                column: "Description",
                value: "Fulare, steaguri, șepci și accesorii pentru suporteri");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Stickere, trofee, mascote și figurine");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 5, "Gustări și băuturi pentru seara meciului", "Snacks & Băuturi" });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 2, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 },
                    { 3, 11 },
                    { 3, 12 },
                    { 3, 13 },
                    { 3, 14 },
                    { 3, 15 },
                    { 4, 16 },
                    { 4, 17 },
                    { 4, 18 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială", "https://loremflickr.com/400/400/argentina,jersey,football?lock=1", 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială", "https://loremflickr.com/400/400/brazil,jersey,football?lock=2", 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl", "Price" },
                values: new object[] { "Tricou oficial replica, ediția Cupa Mondială", "https://loremflickr.com/400/400/france,jersey,soccer?lock=3", 239.99m });

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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Mascota oficială a turneului, de pluș", "https://loremflickr.com/400/400/mascot,plush,toy?lock=18", "Mascotă de Pluș", 79.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Bax 6 sticle pentru seara meciului", "https://loremflickr.com/400/400/beer,bottles?lock=19", "Set 6 Beri Blonde", 34.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Pungă mare de chipsuri pentru petrecere", "https://loremflickr.com/400/400/chips,snacks?lock=20", "Chipsuri Party Mix", 14.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Set 3 pungi popcorn, gata în 3 minute", "https://loremflickr.com/400/400/popcorn,snack?lock=21", "Popcorn pentru Microunde", 8.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Bax 6 doze băuturi carbogazoase", "https://loremflickr.com/400/400/soda,drinks,cans?lock=22", "Set 6 Băuturi Răcoritoare", 29.99m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Cumpără 3 tricouri, cel mai ieftin e gratis");

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "RewardValue", "Threshold" },
                values: new object[] { "10% reducere la comenzi peste 300 RON", 10, 300.00m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { 3, "3 accesorii fani — cel mai ieftin gratis", 0, 1, 3m, 0 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { 5, "Pachet meci: la 4 snacks, 1 gratis", 0, 1, 4m, 0 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "RewardValue", "Threshold" },
                values: new object[] { "Seara finalei: 15% la comenzi peste 500 RON", 15, 500.00m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Name", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { null, "5% reducere la comenzi peste 150 RON", 1, 5, 150.00m, 1 });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Name", "ProductId", "Reward", "RewardValue", "Threshold" },
                values: new object[] { null, "2+ mingi oficiale: 15% reducere", 7, 1, 15, 2m });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "ProductId", "Reward", "RewardValue", "Threshold" },
                values: new object[] { "Cumpără 5 Chipsuri Party Mix, 1 gratis", 20, 0, 1, 5m });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 5, 19 },
                    { 5, 20 },
                    { 5, 21 },
                    { 5, 22 }
                });
        }
    }
}
