using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venna.Migrations
{
    /// <inheritdoc />
    public partial class s1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    photo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mainimg",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstLink = table.Column<int>(type: "int", nullable: false),
                    SecImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecLink = table.Column<int>(type: "int", nullable: false),
                    ThrdImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThrdLink = table.Column<int>(type: "int", nullable: false),
                    FrthImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrthLink = table.Column<int>(type: "int", nullable: false),
                    FifImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FifLink = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mainimg", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryCover",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstLink = table.Column<int>(type: "int", nullable: false),
                    SecImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecLink = table.Column<int>(type: "int", nullable: false),
                    ThrdImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThrdLink = table.Column<int>(type: "int", nullable: false),
                    Categoryid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCover", x => x.id);
                    table.ForeignKey(
                        name: "FK_CategoryCover_Category_Categoryid",
                        column: x => x.Categoryid,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategorys",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoryid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategorys", x => x.id);
                    table.ForeignKey(
                        name: "FK_SubCategorys_Category_Categoryid",
                        column: x => x.Categoryid,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<int>(type: "int", nullable: false),
                    DateOrderd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    desciption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Productinnerphotos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityinStock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    brandid = table.Column<int>(type: "int", nullable: false),
                    categoryid = table.Column<int>(type: "int", nullable: false),
                    Subcategoryid = table.Column<int>(type: "int", nullable: false),
                    rate = table.Column<int>(type: "int", nullable: false),
                    rateNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Product_Brand_brandid",
                        column: x => x.brandid,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Category_categoryid",
                        column: x => x.categoryid,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_SubCategorys_Subcategoryid",
                        column: x => x.Subcategoryid,
                        principalTable: "SubCategorys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCovers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstLink = table.Column<int>(type: "int", nullable: false),
                    SecImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecLink = table.Column<int>(type: "int", nullable: false),
                    ThrdImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThrdLink = table.Column<int>(type: "int", nullable: false),
                    SubCategoryid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCovers", x => x.id);
                    table.ForeignKey(
                        name: "FK_SubCovers_SubCategorys_SubCategoryid",
                        column: x => x.SubCategoryid,
                        principalTable: "SubCategorys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cartitems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Productid = table.Column<int>(type: "int", nullable: false),
                    Cartid = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cartitems_Cart_Cartid",
                        column: x => x.Cartid,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cartitems_Product_Productid",
                        column: x => x.Productid,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orderitems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Productid = table.Column<int>(type: "int", nullable: false),
                    Orderid = table.Column<int>(type: "int", nullable: false),
                    QuantityOrderd = table.Column<int>(name: "Quantity_Orderd", type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orderitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orderitems_Order_Orderid",
                        column: x => x.Orderid,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orderitems_Product_Productid",
                        column: x => x.Productid,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Userid = table.Column<int>(type: "int", nullable: false),
                    Productid = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<byte>(type: "tinyint", nullable: false),
                    RateDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => new { x.Userid, x.Productid });
                    table.ForeignKey(
                        name: "FK_Review_Product_Productid",
                        column: x => x.Productid,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Userid",
                table: "Cart",
                column: "Userid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cartitems_Cartid",
                table: "Cartitems",
                column: "Cartid");

            migrationBuilder.CreateIndex(
                name: "IX_Cartitems_Productid",
                table: "Cartitems",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCover_Categoryid",
                table: "CategoryCover",
                column: "Categoryid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_Userid",
                table: "Order",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Orderitems_Orderid",
                table: "Orderitems",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_Orderitems_Productid",
                table: "Orderitems",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_brandid",
                table: "Product",
                column: "brandid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_categoryid",
                table: "Product",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Subcategoryid",
                table: "Product",
                column: "Subcategoryid");

            migrationBuilder.CreateIndex(
                name: "IX_Review_Productid",
                table: "Review",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorys_Categoryid",
                table: "SubCategorys",
                column: "Categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_SubCovers_SubCategoryid",
                table: "SubCovers",
                column: "SubCategoryid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartitems");

            migrationBuilder.DropTable(
                name: "CategoryCover");

            migrationBuilder.DropTable(
                name: "Mainimg");

            migrationBuilder.DropTable(
                name: "Orderitems");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "SubCovers");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "SubCategorys");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
