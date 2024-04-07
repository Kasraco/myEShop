﻿using Microsoft.EntityFrameworkCore;
using myEShop.Web.Models;

namespace myEShop.Web;

public class myEShopContext : DbContext
{
    public myEShopContext(DbContextOptions<myEShopContext> Options) : base(Options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CategoryToProduct> categoryToProducts { get; set; }

    public DbSet<Item> Items { get; set; }


    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<CategoryToProduct>().HasKey(x => new { x.ProductId, x.CategoryId });

        // modelBuilder.Entity<Product>(
        //     p =>
        //     {
        //         p.HasKey(x => x.Id);
        //         p.HasOne<Item>(x => x.Item).WithOne(x => x.Product).HasForeignKey<Item>(x => x.Id);
        //     }
        // );

        modelBuilder.Entity<Item>(
            i =>
            {
                i.Property(x => x.Price).HasColumnType("Money");
                i.HasKey(x => x.Id);
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Man", Description = "Man Deress" },
            new Category { Id = 2, Name = "Woman", Description = "Woman Deress" },
            new Category { Id = 3, Name = "Sport", Description = "Sport Deress" }
        );

        modelBuilder.Entity<Item>().HasData(
            new Item() { Id = 1, Price = 858540M, QuntityInStock = 5 },
            new Item() { Id = 2, Price = 2500, QuntityInStock = 8 },
            new Item() { Id = 3, Price = 45222, QuntityInStock = 3 }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product() { Id = 1, ItemId = 1, Name = "T-Short", Description = "T-Short", ImageName = "01.jpg" },
            new Product() { Id = 2, ItemId = 2, Name = "Dress1", Description = "Dress", ImageName = "02.jpg" },
            new Product() { Id = 3, ItemId = 3, Name = "Jeen", Description = "Jeen", ImageName = "03.jpg" }
        );

        modelBuilder.Entity<CategoryToProduct>().HasData(
            new CategoryToProduct() { CategoryId = 1, ProductId = 1 },
            new CategoryToProduct() { CategoryId = 2, ProductId = 1 },
            new CategoryToProduct() { CategoryId = 3, ProductId = 1 },
            new CategoryToProduct() { CategoryId = 1, ProductId = 2 },
            new CategoryToProduct() { CategoryId = 2, ProductId = 2 },
            new CategoryToProduct() { CategoryId = 3, ProductId = 2 },
            new CategoryToProduct() { CategoryId = 1, ProductId = 3 },
            new CategoryToProduct() { CategoryId = 2, ProductId = 3 },
            new CategoryToProduct() { CategoryId = 3, ProductId = 3 }
        );
        base.OnModelCreating(modelBuilder);
    }

}
