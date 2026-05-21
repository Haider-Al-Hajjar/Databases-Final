using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ord.EF.Models;

namespace Ord.EF.Data;

public partial class OrdOracleContext : DbContext
{
    public OrdOracleContext(DbContextOptions<OrdOracleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Address { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<CategoryType> CategoryType { get; set; }

    public virtual DbSet<Customer> Customer { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }

    public virtual DbSet<OrderLine> OrderLine { get; set; }

    public virtual DbSet<OrderStatus> OrderStatus { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<ProductCategory> ProductCategory { get; set; }

    public virtual DbSet<ProductPrice> ProductPrice { get; set; }

    public virtual DbSet<VwProductMaxPriceIncrease> VwProductMaxPriceIncrease { get; set; }

    public virtual DbSet<VwProductPrice> VwProductPrice { get; set; }

    public virtual DbSet<Zip> Zip { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("UD_HALHAJJA")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("ADDRESS_PK");

            entity.Property(e => e.AddressId).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.AddressUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.AddressZipcodeNavigation).WithMany(p => p.Address).HasConstraintName("ADDRESS_FK1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("CATEGORY_PK");

            entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CategoryCategoryType).WithMany(p => p.Category).HasConstraintName("CATEGORY_FK2");

            entity.HasOne(d => d.CategoryPrntCategory).WithMany(p => p.InverseCategoryPrntCategory).HasConstraintName("CATEGORY_FK1");
        });

        modelBuilder.Entity<CategoryType>(entity =>
        {
            entity.HasKey(e => e.CategoryTypeId).HasName("CATEGORY_TYPE_PK");

            entity.Property(e => e.CategoryTypeId).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryTypeCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryTypeCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryTypeUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryTypeUpdtId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("CUSTOMER_PK");

            entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerUpdtId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.CustomerAddressId).HasName("CUSTOMER_ADDRESS_PK");

            entity.Property(e => e.CustomerAddressId).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerAddressCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerAddressCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerAddressUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerAddressUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CustomerAddressAddress).WithMany(p => p.CustomerAddress).HasConstraintName("CUSTOMER_ADDRESS_FK2");

            entity.HasOne(d => d.CustomerAddressCustomer).WithMany(p => p.CustomerAddress).HasConstraintName("CUSTOMER_ADDRESS_FK1");
        });

        modelBuilder.Entity<OrderLine>(entity =>
        {
            entity.HasKey(e => e.OrderLineId).HasName("ORDER_LINE_PK");

            entity.Property(e => e.OrderLineId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderLineCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderLineCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderLineUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderLineUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.OrderLineOrder).WithMany(p => p.OrderLine).HasConstraintName("ORDER_LINE_FK1");

            entity.HasOne(d => d.OrderLineProduct).WithMany(p => p.OrderLine).HasConstraintName("ORDER_LINE_FK2");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("ORDER_STATUS_PK");

            entity.Property(e => e.OrderStatusId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStatusCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStatusCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStatusUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrderStatusUpdtId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.OrdersId).HasName("ORDERS_PK");

            entity.Property(e => e.OrdersId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.OrdersUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.OrdersCustomerAddress).WithMany(p => p.Orders).HasConstraintName("ORDERS_FK3");

            entity.HasOne(d => d.OrdersCustomer).WithMany(p => p.Orders).HasConstraintName("ORDERS_FK1");

            entity.HasOne(d => d.OrdersOrderStatus).WithMany(p => p.Orders).HasConstraintName("ORDERS_FK2");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRODUCT_PK");

            entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductUpdtId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PRODUCT_CATEGORY_PK");

            entity.Property(e => e.ProductCategoryId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductCategoryCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductCategoryCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductCategoryUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductCategoryUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ProductCategoryProduct).WithMany(p => p.ProductCategory).HasConstraintName("PRODUCT_CATEGORY_FK1");
        });

        modelBuilder.Entity<ProductPrice>(entity =>
        {
            entity.HasKey(e => e.ProductPriceId).HasName("PRODUCT_PRICE_PK");

            entity.Property(e => e.ProductPriceId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductPriceCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductPriceCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductPriceUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductPriceUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ProductPriceProduct).WithMany(p => p.ProductPrice).HasConstraintName("PRODUCT_PRICE_FK1");
        });

        modelBuilder.Entity<VwProductMaxPriceIncrease>(entity =>
        {
            entity.ToView("VW_PRODUCT_MAX_PRICE_INCREASE");
        });

        modelBuilder.Entity<VwProductPrice>(entity =>
        {
            entity.ToView("VW_PRODUCT_PRICE");
        });

        modelBuilder.Entity<Zip>(entity =>
        {
            entity.HasKey(e => e.Zipcode).HasName("ZIP_PK");

            entity.Property(e => e.Zipcode).ValueGeneratedOnAdd();
            entity.Property(e => e.ZipCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ZipCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.ZipUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ZipUpdtId).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
