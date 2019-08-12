﻿// <auto-generated />
using System;
using WoWning.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WoWning.DataLayer.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190812104730_Recommendations")]
    partial class Recommendations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("WoWning.DataLayer.Entities.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAlchemist");

                    b.Property<bool>("IsArmorsmith");

                    b.Property<bool>("IsAxesmith");

                    b.Property<bool>("IsBlacksmith");

                    b.Property<bool>("IsCook");

                    b.Property<bool>("IsDragonscale");

                    b.Property<bool>("IsElemental");

                    b.Property<bool>("IsEnchanter");

                    b.Property<bool>("IsEngineer");

                    b.Property<bool>("IsFirstAid");

                    b.Property<bool>("IsGnomish");

                    b.Property<bool>("IsGoblin");

                    b.Property<bool>("IsLeather");

                    b.Property<bool>("IsMacesmith");

                    b.Property<bool>("IsSwordsmith");

                    b.Property<bool>("IsTailor");

                    b.Property<bool>("IsTribal");

                    b.Property<bool>("IsWeaponsmith");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Server");

                    b.Property<int>("Side");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("WoWning.DataLayer.Entities.CharacterRecipe", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<int>("RecipeId");

                    b.Property<int>("Copper");

                    b.Property<int>("Gold");

                    b.Property<int>("Silver");

                    b.HasKey("CharacterId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("CharacterRecipes");
                });

            modelBuilder.Entity("WoWning.DataLayer.Entities.CompanyUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("CompanyName");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<int>("NegativeRecommendations");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("PositiveRecommendations");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WoWning.DataLayer.Entities.Material", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("WoWning.DataLayer.Entities.Recipe", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("Amount");

                    b.Property<int>("Category");

                    b.Property<int>("ItemId");

                    b.Property<int>("Level");

                    b.Property<int?>("MaxAmount");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("WoWning.DataLayer.Entities.RecipeMaterial", b =>
                {
                    b.Property<int>("MaterialId");

                    b.Property<int>("RecipeId");

                    b.Property<int>("Amount");

                    b.HasKey("MaterialId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeMaterial");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WoWning.DataLayer.Entities.Character", b =>
                {
                    b.HasOne("WoWning.DataLayer.Entities.CompanyUser", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WoWning.DataLayer.Entities.CharacterRecipe", b =>
                {
                    b.HasOne("WoWning.DataLayer.Entities.Character", "Character")
                        .WithMany("CharacterRecipes")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WoWning.DataLayer.Entities.Recipe", "Recipe")
                        .WithMany("CharacterRecipes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WoWning.DataLayer.Entities.RecipeMaterial", b =>
                {
                    b.HasOne("WoWning.DataLayer.Entities.Material", "Material")
                        .WithMany("RecipeMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WoWning.DataLayer.Entities.Recipe", "Recipe")
                        .WithMany("RecipeMaterials")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WoWning.DataLayer.Entities.CompanyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WoWning.DataLayer.Entities.CompanyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WoWning.DataLayer.Entities.CompanyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WoWning.DataLayer.Entities.CompanyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
