// <auto-generated />
using System;
using ECommerceCoreDapper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECommerceCoreDapper.Migrations
{
    [DbContext(typeof(ECommerceDBContext))]
    [Migration("20220809104032_delColumnProducts")]
    partial class delColumnProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ECommerceCoreDapper.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressID"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Address2")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AddressName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(25)");

                    b.Property<bool>("IsBill")
                        .HasColumnType("bit");

                    b.Property<bool>("IsShip")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("AddressID");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Brands", b =>
                {
                    b.Property<int>("BrandID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandID"), 1L, 1);

                    b.Property<string>("BrandDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("BrandID");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Categories", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ParentID")
                        .HasColumnType("int");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Colors", b =>
                {
                    b.Property<int>("ColorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorID"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ColorID");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Comments", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentID"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductPhotoID")
                        .HasColumnType("int");

                    b.HasKey("CommentID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.CreditCards", b =>
                {
                    b.Property<int>("CreditCardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CreditCardID"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CExpMonth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CExpYear")
                        .HasColumnType("datetime2");

                    b.Property<int>("CVV")
                        .HasColumnType("int");

                    b.Property<string>("CardFName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CardLName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CardName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrLoc")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ErrMsg")
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactStatus")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CreditCardID");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Customers", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("AddressID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreditCardID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEntered")
                        .HasColumnType("datetime2");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSuperAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TCKN")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailID"), 1L, 1);

                    b.Property<DateTime>("BillDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ColorID")
                        .HasColumnType("int");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Freight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("FulFilled")
                        .HasColumnType("bit");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("SalesTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ShipDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ShipperID")
                        .HasColumnType("int");

                    b.Property<int>("SizeID")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderDetailID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Orders", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("Freight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("FulFilled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Paid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PaymentID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequiredDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SalesTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ShipDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ShipperID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Payments", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentID"), 1L, 1);

                    b.Property<bool>("Allowed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PaymentID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.ProductAllJoin", b =>
                {
                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<string>("BrandDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BrandID")
                        .HasColumnType("int");

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ColorID")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("DiscountAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("Featured")
                        .HasColumnType("int");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentID")
                        .HasColumnType("int");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhotoPathID")
                        .HasColumnType("int");

                    b.Property<bool>("ProductAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityPerUnit")
                        .HasColumnType("int");

                    b.Property<int?>("Ranking")
                        .HasColumnType("int");

                    b.Property<int>("ReorderLevel")
                        .HasColumnType("int");

                    b.Property<decimal>("RetailPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SizeID")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<int>("TopSell")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("UnitWeight")
                        .HasColumnType("int");

                    b.Property<int>("UnitsInStock")
                        .HasColumnType("int");

                    b.Property<int>("UnitsOnOrder")
                        .HasColumnType("int");

                    b.Property<string>("WebSite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("sRank")
                        .HasColumnType("int");

                    b.ToView("vw_ProductAllJoin");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.ProductPhotos", b =>
                {
                    b.Property<int>("PhotoPathID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhotoPathID"), 1L, 1);

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("PhotoPathID");

                    b.ToTable("ProductPhotos");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Products", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"), 1L, 1);

                    b.Property<int>("BrandID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("ColorID")
                        .HasColumnType("int");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("DiscountAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("Featured")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhotoPathID")
                        .HasColumnType("int");

                    b.Property<bool>("ProductAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("QuantityPerUnit")
                        .HasColumnType("int");

                    b.Property<int?>("Ranking")
                        .HasColumnType("int");

                    b.Property<decimal>("RetailPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SizeID")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<int>("TopSell")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("UnitWeight")
                        .HasColumnType("int");

                    b.Property<int>("UnitsInStock")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Shippers", b =>
                {
                    b.Property<int>("ShipperID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShipperID"), 1L, 1);

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("ShipperID");

                    b.ToTable("Shippers");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Sizes", b =>
                {
                    b.Property<int>("SizeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeID"), 1L, 1);

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SizeID");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("ECommerceCoreDapper.Models.Suppliers", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierID"), 1L, 1);

                    b.Property<int?>("AddressID")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContactFName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ContactLName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContactTitle")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("DiscountAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("DiscountRate")
                        .HasColumnType("int");

                    b.Property<string>("DiscountType")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Logo")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Ranking")
                        .HasColumnType("int");

                    b.Property<string>("WebSite")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SupplierID");

                    b.ToTable("Suppliers");
                });
#pragma warning restore 612, 618
        }
    }
}
