﻿// <auto-generated />
using System;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220701010733_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("Catalog.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryForeignKey")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("ParentCategoryForeignKey")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryForeignKey")
                        .IsUnique();

                    b.HasIndex("ParentCategoryForeignKey");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Catalog.Core.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryForeignKey")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Catalog.Core.Entities.Category", b =>
                {
                    b.HasOne("Catalog.Core.Entities.Item", null)
                        .WithOne("Category")
                        .HasForeignKey("Catalog.Core.Entities.Category", "CategoryForeignKey");

                    b.HasOne("Catalog.Core.Entities.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Catalog.Core.Entities.Item", b =>
                {
                    b.HasOne("Catalog.Core.Entities.Category", null)
                        .WithMany("Items")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Catalog.Core.Entities.Category", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Catalog.Core.Entities.Item", b =>
                {
                    b.Navigation("Category")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
