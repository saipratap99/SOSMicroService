﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SOSReqQueueAPIService.Models;

#nullable disable

namespace SOSReqQueueAPIService.Migrations
{
    [DbContext(typeof(SOSDbContext))]
    partial class SOSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SOSReqQueueAPIService.Models.FIR", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Pincode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SOSRequestId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("incedentDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("SOSRequestId");

                    b.HasIndex("StatusId");

                    b.ToTable("FIRs", null, t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("SOSReqQueueAPIService.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PriorityType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Priorities", null, t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("SOSReqQueueAPIService.Models.SOSReqQueue", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PoliceId")
                        .HasColumnType("int");

                    b.Property<int>("SOSRequestId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PoliceId")
                        .IsUnique();

                    b.HasIndex("SOSRequestId");

                    b.HasIndex("UserId");

                    b.ToTable("SOSReqQueue");
                });

            modelBuilder.Entity("SOSReqQueueAPIService.Models.SOSRequest", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

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

                    b.Property<int>("PoliceId")
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

                    b.ToTable("SOSRequests", null, t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("SOSReqQueueAPIService.Models.Status", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("StatusType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Statuses", null, t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("SOSReqQueueAPIService.Models.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

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

            modelBuilder.Entity("SOSReqQueueAPIService.Models.FIR", b =>
                {
                    b.HasOne("SOSReqQueueAPIService.Models.SOSRequest", "SOSRequest")
                        .WithMany()
                        .HasForeignKey("SOSRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOSReqQueueAPIService.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SOSRequest");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("SOSReqQueueAPIService.Models.SOSReqQueue", b =>
                {
                    b.HasOne("SOSReqQueueAPIService.Models.User", "Police")
                        .WithOne("policeSOSReqQueue")
                        .HasForeignKey("SOSReqQueueAPIService.Models.SOSReqQueue", "PoliceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOSReqQueueAPIService.Models.SOSRequest", "SOSRequest")
                        .WithMany()
                        .HasForeignKey("SOSRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOSReqQueueAPIService.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Police");

                    b.Navigation("SOSRequest");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SOSReqQueueAPIService.Models.SOSRequest", b =>
                {
                    b.HasOne("SOSReqQueueAPIService.Models.User", "Police")
                        .WithMany()
                        .HasForeignKey("PoliceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOSReqQueueAPIService.Models.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOSReqQueueAPIService.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOSReqQueueAPIService.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Police");

                    b.Navigation("Priority");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SOSReqQueueAPIService.Models.User", b =>
                {
                    b.Navigation("policeSOSReqQueue");
                });
#pragma warning restore 612, 618
        }
    }
}
