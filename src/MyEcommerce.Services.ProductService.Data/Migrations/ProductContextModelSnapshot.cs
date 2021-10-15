﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyEcommerce.Services.ProductService.Data;

namespace MyEcommerce.Services.ProductService.Data.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyEcommerce.Core.Domain.Common.ProductId", b =>
                {
                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Value");

                    b.ToTable("ProductIds", "ProductService");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate.Catalog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Catalogs", "ProductService");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CatalogId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

                    b.ToTable("Categories", "ProductService");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate.CategoryId", b =>
                {
                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Value");

                    b.ToTable("CategoryIds", "ProductService");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("AvailableStock")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<long>("MaxStockThreshold")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnReorder")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("RestockTreshold")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products", "ProductService");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.ProductCategory", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategories", "ProductService");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.ProductTag", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TagId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProductId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductTags", "ProductService");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags", "ProductService");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.TagId", b =>
                {
                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Value");

                    b.ToTable("TagIds", "ProductService");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate.Category", b =>
                {
                    b.HasOne("MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate.Catalog", "Catalog")
                        .WithMany("Categories")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.Product", b =>
                {
                    b.OwnsOne("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.ProductBrand", "ProductBrand", b1 =>
                        {
                            b1.Property<string>("ProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductId");

                            b1.ToTable("ProductBrands");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.ProductType", "ProductType", b1 =>
                        {
                            b1.Property<string>("ProductId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductId");

                            b1.ToTable("ProductTypes");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("ProductBrand")
                        .IsRequired();

                    b.Navigation("ProductType")
                        .IsRequired();
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.ProductCategory", b =>
                {
                    b.HasOne("MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.ProductTag", b =>
                {
                    b.HasOne("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.Product", "Product")
                        .WithMany("ProductTags")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.Tag", "Tag")
                        .WithMany("ProductTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate.Catalog", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.Product", b =>
                {
                    b.Navigation("ProductCategories");

                    b.Navigation("ProductTags");
                });

            modelBuilder.Entity("MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate.Tag", b =>
                {
                    b.Navigation("ProductTags");
                });
#pragma warning restore 612, 618
        }
    }
}
