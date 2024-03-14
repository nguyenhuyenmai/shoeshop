using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Models;

namespace ShoeShop.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }


    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogCategory> BlogCategories { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<CustomerStatus> CustomerStatuses { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderSale> OrderSales { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Settting> Setttings { get; set; }

    public virtual DbSet<Shoe> Shoes { get; set; }

    public virtual DbSet<ShoeImage> ShoeImages { get; set; }

    public virtual DbSet<ShoeLine> ShoeLines { get; set; }

    public virtual DbSet<ShoeVariant> ShoeVariants { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Slider> Sliders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
		}
	}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("Blog_Id");
            entity.Property(e => e.Brief).HasMaxLength(500);
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Blog_User1");

            entity.HasOne(d => d.Category).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Blog_Blog_Category");
        });

        modelBuilder.Entity<BlogCategory>(entity =>
        {
            entity.ToTable("Blog_Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(250)
                .HasColumnName("Category_Name");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(250)
                .HasColumnName("Brand_Name");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("Cart_Id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasColumnName("Create_Date");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_User");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("CartItem");

            entity.Property(e => e.CartItemId).HasColumnName("CartItem_Id");
            entity.Property(e => e.CartId).HasColumnName("Cart_Id");
            entity.Property(e => e.ShoeVariantId).HasColumnName("ShoeVariant_Id");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItem_Cart");

            entity.HasOne(d => d.ShoeVariant).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ShoeVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItem_Shoe_Variant");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.ToTable("Color");

            entity.Property(e => e.ColorId).HasColumnName("Color_Id");
            entity.Property(e => e.ColorName)
                .HasMaxLength(50)
                .HasColumnName("Color_Name");
        });

        modelBuilder.Entity<CustomerStatus>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("Customer_Status");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithOne(p => p.CustomerStatus)
                .HasForeignKey<CustomerStatus>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Status_User");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackContent).HasColumnName("Feedback_Content");
            entity.Property(e => e.RatedStar).HasColumnName("Rated_Star");
            entity.Property(e => e.ShoeId).HasColumnName("Shoe_Id");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Update_Date");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Shoe).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ShoeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_Shoe");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_User");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.OrderAddress).HasColumnName("Order_Address");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("Order_Date");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("date")
                .HasColumnName("Update_Date");
            entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("Order_Item");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItem_Id");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.ShoeVariantId).HasColumnName("ShoeVariant_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Item_Order");

            entity.HasOne(d => d.ShoeVariant).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ShoeVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Item_Shoe_Variant");
        });

        modelBuilder.Entity<OrderSale>(entity =>
        {
            entity.ToTable("Order_Sale");

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderSales)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Sale_Order");

            entity.HasOne(d => d.SalesNavigation).WithMany(p => p.OrderSales)
                .HasForeignKey(d => d.Sales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Sale_User");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permission");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permission_Role");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(250)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<Settting>(entity =>
        {
            entity.ToTable("Settting");

            entity.Property(e => e.SettingCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Setting_Code");
            entity.Property(e => e.SettingName)
                .HasMaxLength(255)
                .HasColumnName("Setting_Name");
            entity.Property(e => e.SettingType)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Setting_Type");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Shoe>(entity =>
        {
            entity.ToTable("Shoe");

            entity.Property(e => e.ShoeId).HasColumnName("Shoe_Id");
            entity.Property(e => e.AverageRating).HasColumnName("average_rating");
            entity.Property(e => e.Created_Date)
              .HasColumnType("datetime")
              .HasColumnName("Created_Date");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.ShoeLineId).HasColumnName("ShoeLine_Id");
            entity.Property(e => e.ShoeName)
                .HasMaxLength(250)
                .HasColumnName("Shoe_Name");
            entity.Property(e => e.ThumbnailPath).HasColumnName("Thumbnail_Path");
            entity.Property(e => e.Updated_Date)
              .HasColumnType("datetime")
              .HasColumnName("Updated_Date");

            entity.HasOne(d => d.ShoeLine).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.ShoeLineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoe_Shoe_Line");
        });

        modelBuilder.Entity<ShoeImage>(entity =>
        {
            entity.ToTable("Shoe_Image");

            entity.Property(e => e.ShoeId).HasColumnName("Shoe_Id");

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoeImages)
                .HasForeignKey(d => d.ShoeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoe_Image_Shoe");
        });

        modelBuilder.Entity<ShoeLine>(entity =>
        {
            entity.ToTable("Shoe_Line");

            entity.Property(e => e.ShoeLineId).HasColumnName("ShoeLine_Id");
            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");
            entity.Property(e => e.ShoeLineName)
                .HasMaxLength(250)
                .HasColumnName("ShoeLine_Name");

            entity.HasOne(d => d.Brand).WithMany(p => p.ShoeLines)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoe_Line_Brand");
        });

        modelBuilder.Entity<ShoeVariant>(entity =>
        {
            entity.ToTable("Shoe_Variant");

            entity.Property(e => e.ShoeVariantId).HasColumnName("ShoeVariant_Id");
            entity.Property(e => e.ColorId).HasColumnName("Color_Id");
            entity.Property(e => e.ShoeId).HasColumnName("Shoe_Id");
            entity.Property(e => e.SizeId).HasColumnName("Size_Id");

            entity.HasOne(d => d.Color).WithMany(p => p.ShoeVariants)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoe_Variant_Color");

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoeVariants)
                .HasForeignKey(d => d.ShoeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoe_Variant_Shoe");

            entity.HasOne(d => d.Size).WithMany(p => p.ShoeVariants)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shoe_Variant_Size");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.ToTable("Size");

            entity.Property(e => e.SizeId).HasColumnName("Size_Id");
            entity.Property(e => e.SizeValue).HasColumnName("Size_Value");
        });

        modelBuilder.Entity<Slider>(entity =>
        {
            entity.ToTable("Slider");

            entity.Property(e => e.SliderId).HasColumnName("Slider_Id");
            entity.Property(e => e.Title).HasMaxLength(250);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("Create_Date");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Update_Date");
            entity.Property(e => e.UserName)
                .HasMaxLength(500)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
