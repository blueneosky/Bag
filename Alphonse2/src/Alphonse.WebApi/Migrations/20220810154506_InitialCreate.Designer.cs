﻿// <auto-generated />
using System;
using Alphonse.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Alphonse.WebApi.Migrations
{
    [DbContext(typeof(AlphonseDbContext))]
    [Migration("20220810154506_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("Alphonse.WebApi.Dbo.CallHistoryDbo", b =>
                {
                    b.Property<int>("CallHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

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
#pragma warning restore 612, 618
        }
    }
}
