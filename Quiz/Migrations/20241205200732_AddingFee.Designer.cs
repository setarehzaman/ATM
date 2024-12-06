﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quiz;

#nullable disable

namespace Quiz.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20241205200732_AddingFee")]
    partial class AddingFee
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Quiz.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("FailedAttempts")
                        .HasColumnType("int");

                    b.Property<string>("HolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Balance = 500f,
                            CardNumber = "1234567812345678",
                            FailedAttempts = 0,
                            HolderName = "Mellat",
                            IsActive = true,
                            Password = "1234",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Balance = 300f,
                            CardNumber = "8765432187654321",
                            FailedAttempts = 0,
                            HolderName = "Meli",
                            IsActive = true,
                            Password = "5678",
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Balance = 300f,
                            CardNumber = "1234567887654321",
                            FailedAttempts = 0,
                            HolderName = "Saderat",
                            IsActive = true,
                            Password = "90-=",
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            Balance = 300f,
                            CardNumber = "8765432112345678",
                            FailedAttempts = 0,
                            HolderName = "Sepah",
                            IsActive = true,
                            Password = "1234",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Quiz.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("DestinationCardId")
                        .HasColumnType("int");

                    b.Property<string>("DestinationCardNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<float>("Fee")
                        .HasColumnType("real");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<int>("SourceCardId")
                        .HasColumnType("int");

                    b.Property<string>("SourceCardNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TransactionId");

                    b.HasIndex("DestinationCardId");

                    b.HasIndex("SourceCardId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Quiz.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "setareh@gmail.com",
                            Name = "Setareh Zaman",
                            Password = "123"
                        },
                        new
                        {
                            Id = 2,
                            Email = "Narges@gmail.com",
                            Name = "Narges Dehghan",
                            Password = "456"
                        },
                        new
                        {
                            Id = 3,
                            Email = "Sarvenaz@gmail.com",
                            Name = "Sarvenaz Fazli",
                            Password = "789"
                        });
                });

            modelBuilder.Entity("Quiz.Entities.Card", b =>
                {
                    b.HasOne("Quiz.Entities.User", "User")
                        .WithMany("UserCards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quiz.Entities.Transaction", b =>
                {
                    b.HasOne("Quiz.Entities.Card", "DestinationCard")
                        .WithMany("DestinationCardTransactions")
                        .HasForeignKey("DestinationCardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Quiz.Entities.Card", "SourceCard")
                        .WithMany("SourceCardTransactions")
                        .HasForeignKey("SourceCardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationCard");

                    b.Navigation("SourceCard");
                });

            modelBuilder.Entity("Quiz.Entities.Card", b =>
                {
                    b.Navigation("DestinationCardTransactions");

                    b.Navigation("SourceCardTransactions");
                });

            modelBuilder.Entity("Quiz.Entities.User", b =>
                {
                    b.Navigation("UserCards");
                });
#pragma warning restore 612, 618
        }
    }
}
