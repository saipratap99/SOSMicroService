﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SOSRequestsAPIService.Models;

#nullable disable

namespace SOSRequestsAPIService.Migrations
{
    [DbContext(typeof(SOSDbContext))]
    [Migration("20230130100839_ModifySOSRequestTable")]
    partial class ModifySOSRequestTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SOSRequestsAPIService.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PriorityType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("SOSRequestsAPIService.Models.SOSRequest", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("IncedentDetails")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Pincode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<int?>("PoliceId")
                        .HasColumnType("int");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PoliceId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("SOSRequests");
                });

            modelBuilder.Entity("SOSRequestsAPIService.Models.Status", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("StatusType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("UsersAPIService.Models.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Pincode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("Role")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Users", null, t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("SOSRequestsAPIService.Models.SOSRequest", b =>
                {
                    b.HasOne("UsersAPIService.Models.User", "Police")
                        .WithMany()
                        .HasForeignKey("PoliceId");

                    b.HasOne("SOSRequestsAPIService.Models.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOSRequestsAPIService.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UsersAPIService.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Police");

                    b.Navigation("Priority");

                    b.Navigation("Status");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
