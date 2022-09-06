﻿// <auto-generated />
using System;
using KgNet88.Woden.Account.Infrastructure.Profile.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KgNet88.Woden.Account.Infrastructure.Profile.Persistence.Database.Migrations;

[DbContext(typeof(ProfileDbContext))]
partial class ProfileDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasDefaultSchema("account")
            .HasAnnotation("ProductVersion", "6.0.8")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("KgNet88.Woden.Account.Infrastructure.Profile.Persistence.Database.Model.DbUserProfile", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<string>("DisplayName")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("MatrixId")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("ProfileEmail")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid>("UserId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.ToTable("Profiles", "account");
            });
#pragma warning restore 612, 618
    }
}
