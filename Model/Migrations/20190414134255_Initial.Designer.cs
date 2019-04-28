﻿// <auto-generated />
using System;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20190414134255_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Models.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ForBikers");

                    b.Property<bool>("ForDrivers");

                    b.Property<bool>("ForKids");

                    b.Property<bool>("ForPedestrians");

                    b.Property<int>("SignID");

                    b.Property<int?>("TestID");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("SignID");

                    b.HasIndex("TestID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Models.Models.Sign", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ForBikers");

                    b.Property<bool>("ForDrivers");

                    b.Property<bool>("ForKids");

                    b.Property<bool>("ForPedestrians");

                    b.Property<string>("Hints")
                        .IsRequired();

                    b.Property<byte[]>("Image")
                        .IsRequired();

                    b.Property<string>("Information")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Signs");
                });

            modelBuilder.Entity("Models.Models.Statistic", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("HasAnswered");

                    b.Property<int>("QuestionID");

                    b.Property<int>("TestID");

                    b.HasKey("ID");

                    b.HasIndex("QuestionID");

                    b.HasIndex("TestID");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("Models.Models.Test", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserID")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Models.Models.User", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.Models.Variant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .IsRequired();

                    b.Property<bool>("IsCorrect");

                    b.Property<int>("QuestionID");

                    b.HasKey("ID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Variants");
                });

            modelBuilder.Entity("Models.Models.Question", b =>
                {
                    b.HasOne("Models.Models.Sign", "Sign")
                        .WithMany()
                        .HasForeignKey("SignID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Models.Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestID");
                });

            modelBuilder.Entity("Models.Models.Statistic", b =>
                {
                    b.HasOne("Models.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Models.Test", "Test")
                        .WithMany("Stats")
                        .HasForeignKey("TestID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Models.Test", b =>
                {
                    b.HasOne("Models.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Models.Variant", b =>
                {
                    b.HasOne("Models.Models.Question", "Question")
                        .WithMany("Variants")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}