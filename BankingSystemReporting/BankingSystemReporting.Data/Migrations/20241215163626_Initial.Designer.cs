﻿// <auto-generated />
using System;
using BankingSystemReporting.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingSystemReporting.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241215163626_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankingSystemReporting.Models.Merchant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("BoardingDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartnerID")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PartnerID");

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("BankingSystemReporting.Models.Partner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("BankingSystemReporting.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BeneficiaryIBAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DebtorIBAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MerchantID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MerchantID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankingSystemReporting.Models.Merchant", b =>
                {
                    b.HasOne("BankingSystemReporting.Models.Partner", "Partner")
                        .WithMany("Merchants")
                        .HasForeignKey("PartnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("BankingSystemReporting.Models.Transaction", b =>
                {
                    b.HasOne("BankingSystemReporting.Models.Merchant", "Merchant")
                        .WithMany("Transactions")
                        .HasForeignKey("MerchantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("BankingSystemReporting.Models.Merchant", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BankingSystemReporting.Models.Partner", b =>
                {
                    b.Navigation("Merchants");
                });
#pragma warning restore 612, 618
        }
    }
}
