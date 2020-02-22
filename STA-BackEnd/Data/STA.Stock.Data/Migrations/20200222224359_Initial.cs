using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STA.Stock.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "colors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsInBlackList = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Instagram = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "infuluencers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Instagram = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<byte>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Followers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_infuluencers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pictures",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ModelId = table.Column<string>(nullable: true),
                    ImageData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "models",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BrandId = table.Column<Guid>(nullable: false),
                    PictureId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_models_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "adCampains",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<byte>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    CompleteComment = table.Column<string>(nullable: true),
                    InfuluencerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adCampains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_adCampains_infuluencers_InfuluencerId",
                        column: x => x.InfuluencerId,
                        principalTable: "infuluencers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    ModelId = table.Column<Guid>(nullable: false),
                    ColorId = table.Column<Guid>(nullable: false),
                    NumberRange = table.Column<string>(nullable: true),
                    PictureId = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    StockCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_products_models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "productStocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productStocks_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    ProductStockId = table.Column<Guid>(nullable: false),
                    OrderPrice = table.Column<double>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    CompleteDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_productStocks_ProductStockId",
                        column: x => x.ProductStockId,
                        principalTable: "productStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_adCampains_InfuluencerId",
                table: "adCampains",
                column: "InfuluencerId");

            migrationBuilder.CreateIndex(
                name: "IX_models_BrandId",
                table: "models",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_CustomerId",
                table: "orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ProductStockId",
                table: "orders",
                column: "ProductStockId");

            migrationBuilder.CreateIndex(
                name: "IX_products_ColorId",
                table: "products",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_products_ModelId",
                table: "products",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_productStocks_ProductId",
                table: "productStocks",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adCampains");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "pictures");

            migrationBuilder.DropTable(
                name: "infuluencers");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "productStocks");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "models");

            migrationBuilder.DropTable(
                name: "brands");
        }
    }
}
