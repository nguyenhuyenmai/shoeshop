using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeShop.Migrations
{
    /// <inheritdoc />
    public partial class updatedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog_Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Brand_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand_Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Brand_Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Color_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Color_Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Role_Id);
                });

            migrationBuilder.CreateTable(
                name: "Settting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Setting_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Setting_Type = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Setting_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Desciption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateUser = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Size_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size_Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Size_Id);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    Slider_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Backlink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Slider_Id);
                });

            migrationBuilder.CreateTable(
                name: "Shoe_Line",
                columns: table => new
                {
                    ShoeLine_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand_Id = table.Column<int>(type: "int", nullable: false),
                    ShoeLine_Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoe_Line", x => x.ShoeLine_Id);
                    table.ForeignKey(
                        name: "FK_Shoe_Line_Brand",
                        column: x => x.Brand_Id,
                        principalTable: "Brand",
                        principalColumn: "Brand_Id");
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Permisson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permission_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Role_Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Id = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Create_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Update_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_Id);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "Role_Id");
                });

            migrationBuilder.CreateTable(
                name: "Shoe",
                columns: table => new
                {
                    Shoe_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoeLine_Id = table.Column<int>(type: "int", nullable: false),
                    Shoe_Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Thumbnail_Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    average_rating = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    IsFeature = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoe", x => x.Shoe_Id);
                    table.ForeignKey(
                        name: "FK_Shoe_Shoe_Line",
                        column: x => x.ShoeLine_Id,
                        principalTable: "Shoe_Line",
                        principalColumn: "ShoeLine_Id");
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Blog_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Brief = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Desciption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsFearture = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Blog_Id);
                    table.ForeignKey(
                        name: "FK_Blog_Blog_Category",
                        column: x => x.Category_Id,
                        principalTable: "Blog_Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Blog_User1",
                        column: x => x.Author,
                        principalTable: "User",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Cart_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Create_Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Cart_Id);
                    table.ForeignKey(
                        name: "FK_Cart_User",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Order_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Update_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Update_User = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Order_User",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Shoe_Id = table.Column<int>(type: "int", nullable: false),
                    Rated_Star = table.Column<int>(type: "int", nullable: false),
                    Feedback_Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Update_Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Shoe",
                        column: x => x.Shoe_Id,
                        principalTable: "Shoe",
                        principalColumn: "Shoe_Id");
                    table.ForeignKey(
                        name: "FK_Feedback_User",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "Shoe_Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shoe_Id = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoe_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shoe_Image_Shoe",
                        column: x => x.Shoe_Id,
                        principalTable: "Shoe",
                        principalColumn: "Shoe_Id");
                });

            migrationBuilder.CreateTable(
                name: "Shoe_Variant",
                columns: table => new
                {
                    ShoeVariant_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shoe_Id = table.Column<int>(type: "int", nullable: false),
                    Color_Id = table.Column<int>(type: "int", nullable: false),
                    Size_Id = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoe_Variant", x => x.ShoeVariant_Id);
                    table.ForeignKey(
                        name: "FK_Shoe_Variant_Color",
                        column: x => x.Color_Id,
                        principalTable: "Color",
                        principalColumn: "Color_Id");
                    table.ForeignKey(
                        name: "FK_Shoe_Variant_Shoe",
                        column: x => x.Shoe_Id,
                        principalTable: "Shoe",
                        principalColumn: "Shoe_Id");
                    table.ForeignKey(
                        name: "FK_Shoe_Variant_Size",
                        column: x => x.Size_Id,
                        principalTable: "Size",
                        principalColumn: "Size_Id");
                });

            migrationBuilder.CreateTable(
                name: "Order_Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    Sales = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Sale_Order",
                        column: x => x.Order_Id,
                        principalTable: "Order",
                        principalColumn: "Order_Id");
                    table.ForeignKey(
                        name: "FK_Order_Sale_User",
                        column: x => x.Sales,
                        principalTable: "User",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItem_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cart_Id = table.Column<int>(type: "int", nullable: false),
                    ShoeVariant_Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.CartItem_Id);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart",
                        column: x => x.Cart_Id,
                        principalTable: "Cart",
                        principalColumn: "Cart_Id");
                    table.ForeignKey(
                        name: "FK_CartItem_Shoe_Variant",
                        column: x => x.ShoeVariant_Id,
                        principalTable: "Shoe_Variant",
                        principalColumn: "ShoeVariant_Id");
                });

            migrationBuilder.CreateTable(
                name: "Order_Item",
                columns: table => new
                {
                    OrderItem_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    ShoeVariant_Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Item", x => x.OrderItem_Id);
                    table.ForeignKey(
                        name: "FK_Order_Item_Order",
                        column: x => x.Order_Id,
                        principalTable: "Order",
                        principalColumn: "Order_Id");
                    table.ForeignKey(
                        name: "FK_Order_Item_Shoe_Variant",
                        column: x => x.ShoeVariant_Id,
                        principalTable: "Shoe_Variant",
                        principalColumn: "ShoeVariant_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Author",
                table: "Blog",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Category_Id",
                table: "Blog",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_User_Id",
                table: "Cart",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_Cart_Id",
                table: "CartItem",
                column: "Cart_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ShoeVariant_Id",
                table: "CartItem",
                column: "ShoeVariant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_Shoe_Id",
                table: "Feedback",
                column: "Shoe_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_User_Id",
                table: "Feedback",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_User_Id",
                table: "Order",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Item_Order_Id",
                table: "Order_Item",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Item_ShoeVariant_Id",
                table: "Order_Item",
                column: "ShoeVariant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Sale_Order_Id",
                table: "Order_Sale",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Sale_Sales",
                table: "Order_Sale",
                column: "Sales");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_RoleId",
                table: "Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoe_ShoeLine_Id",
                table: "Shoe",
                column: "ShoeLine_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shoe_Image_Shoe_Id",
                table: "Shoe_Image",
                column: "Shoe_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shoe_Line_Brand_Id",
                table: "Shoe_Line",
                column: "Brand_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shoe_Variant_Color_Id",
                table: "Shoe_Variant",
                column: "Color_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shoe_Variant_Shoe_Id",
                table: "Shoe_Variant",
                column: "Shoe_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shoe_Variant_Size_Id",
                table: "Shoe_Variant",
                column: "Size_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_Id",
                table: "User",
                column: "Role_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Order_Item");

            migrationBuilder.DropTable(
                name: "Order_Sale");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Settting");

            migrationBuilder.DropTable(
                name: "Shoe_Image");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropTable(
                name: "Blog_Category");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Shoe_Variant");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Shoe");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Shoe_Line");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}
