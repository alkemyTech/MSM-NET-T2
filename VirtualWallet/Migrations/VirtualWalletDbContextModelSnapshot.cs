﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VirtualWallet.DataAccess;

#nullable disable

namespace VirtualWallet.Migrations
{
    [DbContext(typeof(VirtualWalletDbContext))]
    partial class VirtualWalletDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VirtualWallet.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Account", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(2018, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsBlocked = true,
                            Money = 210600m,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsBlocked = false,
                            Money = 100980m,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2019, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsBlocked = false,
                            Money = 250990m,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            CreationDate = new DateTime(2019, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsBlocked = false,
                            Money = 420560m,
                            UserId = 4
                        });
                });

            modelBuilder.Entity("VirtualWallet.Models.Catalogue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Catalogue", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Image = "Image3.jpg",
                            Points = 32000,
                            ProductDescription = "Auriculares inalámbricos"
                        },
                        new
                        {
                            Id = 4,
                            Image = "Image4.jpg",
                            Points = 5000,
                            ProductDescription = "Tarjeta de regalo de $50"
                        },
                        new
                        {
                            Id = 5,
                            Image = "Image5.jpg",
                            Points = 12000,
                            ProductDescription = "Camiseta de edición limitada"
                        },
                        new
                        {
                            Id = 6,
                            Image = "Image6.jpg",
                            Points = 15000,
                            ProductDescription = "Botella de vino premium"
                        });
                });

            modelBuilder.Entity("VirtualWallet.Models.FixedTermDeposit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateTime>("ClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("NominalRate")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("UserId");

                    b.ToTable("FixedTerm", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            Amount = 10000.00m,
                            ClosingDate = new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NominalRate = 5.0m,
                            State = "Active",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 2,
                            Amount = 15000.00m,
                            ClosingDate = new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NominalRate = 4.5m,
                            State = "Active",
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            AccountId = 3,
                            Amount = 20000.00m,
                            ClosingDate = new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NominalRate = 4.0m,
                            State = "Active",
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            AccountId = 4,
                            Amount = 12000.00m,
                            ClosingDate = new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NominalRate = 4.2m,
                            State = "Active",
                            UserId = 4
                        });
                });

            modelBuilder.Entity("VirtualWallet.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Permisos para agregar y eliminar usuarios",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Cliente",
                            Name = "Regular"
                        });
                });

            modelBuilder.Entity("VirtualWallet.Models.Transaction", b =>
                {
                    b.Property<int>("transactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("transactionId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("Concept")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ToAccountId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("transactionId");

                    b.HasIndex("AccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Transaction", (string)null);

                    b.HasData(
                        new
                        {
                            transactionId = 1,
                            AccountId = 2,
                            Amount = 7500m,
                            Concept = "Compra en l�nea 3",
                            Date = new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = "payment",
                            UserId = 2
                        },
                        new
                        {
                            transactionId = 2,
                            AccountId = 1,
                            Amount = 3000m,
                            Concept = "Dep�sito en efectivo 2",
                            Date = new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = "topup",
                            UserId = 1
                        },
                        new
                        {
                            transactionId = 3,
                            AccountId = 3,
                            Amount = 9000m,
                            Concept = "Pago de factura 2",
                            Date = new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ToAccountId = 2,
                            Type = "payment",
                            UserId = 3
                        },
                        new
                        {
                            transactionId = 4,
                            AccountId = 2,
                            Amount = 6000m,
                            Concept = "Transferencia bancaria 2",
                            Date = new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ToAccountId = 3,
                            Type = "payment",
                            UserId = 2
                        },
                        new
                        {
                            transactionId = 5,
                            AccountId = 4,
                            Amount = 4200m,
                            Concept = "Recarga de tarjeta 2",
                            Date = new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = "topup",
                            UserId = 4
                        });
                });

            modelBuilder.Entity("VirtualWallet.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("First_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("Role_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Role_Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "pedro@pedro",
                            First_name = "Pedro",
                            Last_name = "Gonzalez",
                            Password = "123",
                            Points = 50,
                            Role_Id = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "fede@fede",
                            First_name = "Fede",
                            Last_name = "Perez",
                            Password = "1234",
                            Points = 504,
                            Role_Id = 2
                        },
                        new
                        {
                            Id = 3,
                            Email = "maca@maca",
                            First_name = "Maca",
                            Last_name = "Pereira",
                            Password = "12345",
                            Points = 5,
                            Role_Id = 2
                        },
                        new
                        {
                            Id = 4,
                            Email = "mac@maca",
                            First_name = "Sofia",
                            Last_name = "Gomez",
                            Password = "123456",
                            Points = 23,
                            Role_Id = 2
                        });
                });

            modelBuilder.Entity("VirtualWallet.Models.Account", b =>
                {
                    b.HasOne("VirtualWallet.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VirtualWallet.Models.FixedTermDeposit", b =>
                {
                    b.HasOne("VirtualWallet.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VirtualWallet.Models.User", "User")
                        .WithMany("FixedTermDeposits")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VirtualWallet.Models.Transaction", b =>
                {
                    b.HasOne("VirtualWallet.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VirtualWallet.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VirtualWallet.Models.User", b =>
                {
                    b.HasOne("VirtualWallet.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("VirtualWallet.Models.User", b =>
                {
                    b.Navigation("FixedTermDeposits");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
