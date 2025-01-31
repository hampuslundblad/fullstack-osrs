﻿// <auto-generated />
using DotnetComp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotnetComp.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250115151642_UpdateEntities2")]
    partial class UpdateEntities2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("DotnetComp.Models.Entities.GroupEntity", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.PlayerEntity", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExperienceGainedLast24H")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExperienceGainedLastWeek")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroupEntityGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalExperience")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlayerId");

                    b.HasIndex("GroupEntityGroupId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.PlayerEntity", b =>
                {
                    b.HasOne("DotnetComp.Models.Entities.GroupEntity", null)
                        .WithMany("Players")
                        .HasForeignKey("GroupEntityGroupId");
                });

            modelBuilder.Entity("DotnetComp.Models.Entities.GroupEntity", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
