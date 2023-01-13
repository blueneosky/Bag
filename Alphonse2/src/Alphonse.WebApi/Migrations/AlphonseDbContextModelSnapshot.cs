﻿// <auto-generated />
using System;
using Alphonse.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Alphonse.WebApi.Migrations
{
    [DbContext(typeof(AlphonseDbContext))]
    partial class AlphonseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("Alphonse.WebApi.Dbo.CallHistoryDbo", b =>
                {
                    b.Property<int>("CallHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Action")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<long>("Timestamp")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UCallNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("CallHistoryId");

                    b.ToTable("CallHistories");
                });

            modelBuilder.Entity("Alphonse.WebApi.Dbo.PhoneNumberDbo", b =>
                {
                    b.Property<int>("PhoneNumberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Allowed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("UPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("PhoneNumberId");

                    b.HasIndex(new[] { "UPhoneNumber" }, "Unicity_UPhoneNumber")
                        .IsUnique();

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("Alphonse.WebApi.Dbo.UserDbo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccessRole")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<string>("HPass")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
