using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectcore.Data.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameFamily",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postalcode",
                table: "AspNetUsers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SellerStatus",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    KeyWords = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConfigEmails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Smtp = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigEmails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConfigSms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Api = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigSms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    NameFamily = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Authority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Suports",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suports", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfAds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfAds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CategoryDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CategoryDetails_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubCategories_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleAds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Visits = table.Column<int>(type: "int", nullable: false),
                    Special = table.Column<bool>(type: "bit", nullable: false),
                    SpecialDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfAdId = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    CategryID = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleAds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SaleAds_Category_CategryID",
                        column: x => x.CategryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleAds_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleAds_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SaleAds_TypeOfAds_TypeOfAdId",
                        column: x => x.TypeOfAdId,
                        principalTable: "TypeOfAds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryDetailsAd",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDAd = table.Column<int>(type: "int", nullable: false),
                    IDCategoryDetails = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDetailsAd", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CategoryDetailsAd_CategoryDetails_IDCategoryDetails",
                        column: x => x.IDCategoryDetails,
                        principalTable: "CategoryDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryDetailsAd_SaleAds_IDAd",
                        column: x => x.IDAd,
                        principalTable: "SaleAds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Favorties",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaleAdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Favorties_SaleAds_SaleAdId",
                        column: x => x.SaleAdId,
                        principalTable: "SaleAds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoGalleries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SaleAdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoGalleries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PhotoGalleries_SaleAds_SaleAdId",
                        column: x => x.SaleAdId,
                        principalTable: "SaleAds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<bool>(type: "bit", maxLength: 250, nullable: false),
                    Date = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AdID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reports_SaleAds_AdID",
                        column: x => x.AdID,
                        principalTable: "SaleAds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDetails_CategoryID",
                table: "CategoryDetails",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDetailsAd_IDAd",
                table: "CategoryDetailsAd",
                column: "IDAd");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDetailsAd_IDCategoryDetails",
                table: "CategoryDetailsAd",
                column: "IDCategoryDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Favorties_SaleAdId",
                table: "Favorties",
                column: "SaleAdId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoGalleries_SaleAdId",
                table: "PhotoGalleries",
                column: "SaleAdId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AdID",
                table: "Reports",
                column: "AdID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleAds_CategryID",
                table: "SaleAds",
                column: "CategryID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleAds_ProductTypeId",
                table: "SaleAds",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleAds_SubCategoryId",
                table: "SaleAds",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleAds_TypeOfAdId",
                table: "SaleAds",
                column: "TypeOfAdId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryID",
                table: "SubCategories",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropTable(
                name: "CategoryDetailsAd");

            migrationBuilder.DropTable(
                name: "ConfigEmails");

            migrationBuilder.DropTable(
                name: "ConfigSms");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "Favorties");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "PhotoGalleries");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Suports");

            migrationBuilder.DropTable(
                name: "CategoryDetails");

            migrationBuilder.DropTable(
                name: "SaleAds");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "TypeOfAds");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NameFamily",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Postalcode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SellerStatus",
                table: "AspNetUsers");
        }
    }
}
