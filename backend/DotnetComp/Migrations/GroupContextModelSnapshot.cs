﻿// <auto-generated />
using System;
using DotnetComp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotnetComp.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class GroupContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("DotnetComp.Models.Entities.AuthProviderEntity", b =>
                {
                    b.Property<int>("AuthProviderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthProviderUserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AuthProviderId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("AuthProviders");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.GroupEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.PlayerBossKillEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BossId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Kills")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlayerEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerEntityId");

                    b.ToTable("PlayerBossKillEntity");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.PlayerBossRankEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BossId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PlayerEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rank")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerEntityId");

                    b.ToTable("PlayerBossRankEntity");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.PlayerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalExperience")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalLevel")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.PlayerExperienceEntity", b =>
                {
                    b.Property<int>("PlayerExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Experience")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlayerEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlayerExperienceId");

                    b.HasIndex("PlayerEntityId");

                    b.ToTable("PlayerExperienceEntity");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.UserEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GroupEntityPlayerEntity", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GroupsId", "PlayersId");

                    b.HasIndex("PlayersId");

                    b.ToTable("GroupEntityPlayerEntity");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.AuthProviderEntity", b =>
                {
                    b.HasOne("DotnetComp.Models.Entities.UserEntity", "User")
                        .WithOne("AuthProvider")
                        .HasForeignKey("DotnetComp.Models.Entities.AuthProviderEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.GroupEntity", b =>
                {
                    b.HasOne("DotnetComp.Models.Entities.UserEntity", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.PlayerBossKillEntity", b =>
                {
                    b.HasOne("DotnetComp.Models.Entities.PlayerEntity", null)
                        .WithMany("PlayerBossKills")
                        .HasForeignKey("PlayerEntityId");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.PlayerBossRankEntity", b =>
                {
                    b.HasOne("DotnetComp.Models.Entities.PlayerEntity", null)
                        .WithMany("PlayerBossRanks")
                        .HasForeignKey("PlayerEntityId");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.PlayerExperienceEntity", b =>
                {
                    b.HasOne("DotnetComp.Models.Entities.PlayerEntity", null)
                        .WithMany("PlayerExperiences")
                        .HasForeignKey("PlayerEntityId");
                });

            modelBuilder.Entity("GroupEntityPlayerEntity", b =>
                {
                    b.HasOne("DotnetComp.Models.Entities.GroupEntity", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotnetComp.Models.Entities.PlayerEntity", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.PlayerEntity", b =>
                {
                    b.Navigation("PlayerBossKills");

                    b.Navigation("PlayerBossRanks");

                    b.Navigation("PlayerExperiences");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.UserEntity", b =>
                {
                    b.Navigation("AuthProvider")
                        .IsRequired();

                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
