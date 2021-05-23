﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PassTrackingSystem.Models;

namespace PassTrackingSystem.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20210522084845_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PassTrackingSystem.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarBrand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarLicensePlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CarPassId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarPassId")
                        .IsUnique();

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.CarPass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CarPassIssuedId")
                        .HasColumnType("int");

                    b.Property<string>("PurposeOfIssuance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidWith")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValitUntil")
                        .HasColumnType("datetime2");

                    b.Property<int>("VisitorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarPassIssuedId");

                    b.HasIndex("VisitorId");

                    b.ToTable("CarPasses");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("datetime2");

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("IssuingAuthorityId")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Series")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VisitorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("IssuingAuthorityId");

                    b.HasIndex("VisitorId")
                        .IsUnique();

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.IssuingAuthority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IssuingAuthorities");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.ShootingPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CameraType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShootingPermissionIssuedId")
                        .HasColumnType("int");

                    b.Property<string>("ShootingPurpose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidWith")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValitUntil")
                        .HasColumnType("datetime2");

                    b.Property<int>("VisitorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShootingPermissionIssuedId");

                    b.HasIndex("VisitorId");

                    b.ToTable("ShootingPermissions");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.SinglePass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PurposeOfIssuance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SinglePassIssuedId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidWith")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValitUntil")
                        .HasColumnType("datetime2");

                    b.Property<int>("VisitorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SinglePassIssuedId");

                    b.HasIndex("VisitorId");

                    b.ToTable("SinglePasses");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.StationFacility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StationFacilities");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.TemporaryPass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PurposeOfIssuance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TemporaryPassIssuedId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidWith")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValitUntil")
                        .HasColumnType("datetime2");

                    b.Property<int>("VisitorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TemporaryPassIssuedId");

                    b.HasIndex("VisitorId");

                    b.ToTable("TemporaryPasses");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.Visitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfWork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Visitors");
                });

            modelBuilder.Entity("ShootingPermissionStationFacility", b =>
                {
                    b.Property<int>("ShootingPermissionsId")
                        .HasColumnType("int");

                    b.Property<int>("StationFacilitiesId")
                        .HasColumnType("int");

                    b.HasKey("ShootingPermissionsId", "StationFacilitiesId");

                    b.HasIndex("StationFacilitiesId");

                    b.ToTable("ShootingPermissionStationFacility");
                });

            modelBuilder.Entity("SinglePassStationFacility", b =>
                {
                    b.Property<int>("SinglePassesId")
                        .HasColumnType("int");

                    b.Property<int>("StationFacilitiesId")
                        .HasColumnType("int");

                    b.HasKey("SinglePassesId", "StationFacilitiesId");

                    b.HasIndex("StationFacilitiesId");

                    b.ToTable("SinglePassStationFacility");
                });

            modelBuilder.Entity("StationFacilityTemporaryPass", b =>
                {
                    b.Property<int>("StationFacilitiesId")
                        .HasColumnType("int");

                    b.Property<int>("TemporaryPassesId")
                        .HasColumnType("int");

                    b.HasKey("StationFacilitiesId", "TemporaryPassesId");

                    b.HasIndex("TemporaryPassesId");

                    b.ToTable("StationFacilityTemporaryPass");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.Car", b =>
                {
                    b.HasOne("PassTrackingSystem.Models.CarPass", "CarPass")
                        .WithOne("Car")
                        .HasForeignKey("PassTrackingSystem.Models.Car", "CarPassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarPass");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.CarPass", b =>
                {
                    b.HasOne("PassTrackingSystem.Models.Employee", "CarPassIssued")
                        .WithMany()
                        .HasForeignKey("CarPassIssuedId");

                    b.HasOne("PassTrackingSystem.Models.Visitor", "Visitor")
                        .WithMany("CarPasses")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarPassIssued");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.Document", b =>
                {
                    b.HasOne("PassTrackingSystem.Models.DocumentType", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassTrackingSystem.Models.IssuingAuthority", "IssuingAuthority")
                        .WithMany()
                        .HasForeignKey("IssuingAuthorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassTrackingSystem.Models.Visitor", "Visitor")
                        .WithOne("Document")
                        .HasForeignKey("PassTrackingSystem.Models.Document", "VisitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");

                    b.Navigation("IssuingAuthority");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.Employee", b =>
                {
                    b.HasOne("PassTrackingSystem.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.ShootingPermission", b =>
                {
                    b.HasOne("PassTrackingSystem.Models.Employee", "ShootingPermissionIssued")
                        .WithMany()
                        .HasForeignKey("ShootingPermissionIssuedId");

                    b.HasOne("PassTrackingSystem.Models.Visitor", "Visitor")
                        .WithMany("ShootingPermissions")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShootingPermissionIssued");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.SinglePass", b =>
                {
                    b.HasOne("PassTrackingSystem.Models.Employee", "SinglePassIssued")
                        .WithMany()
                        .HasForeignKey("SinglePassIssuedId");

                    b.HasOne("PassTrackingSystem.Models.Visitor", "Visitor")
                        .WithMany("SinglePasses")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SinglePassIssued");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.TemporaryPass", b =>
                {
                    b.HasOne("PassTrackingSystem.Models.Employee", "TemporaryPassIssued")
                        .WithMany()
                        .HasForeignKey("TemporaryPassIssuedId");

                    b.HasOne("PassTrackingSystem.Models.Visitor", "Visitor")
                        .WithMany("TemporaryPasses")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TemporaryPassIssued");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("ShootingPermissionStationFacility", b =>
                {
                    b.HasOne("PassTrackingSystem.Models.ShootingPermission", null)
                        .WithMany()
                        .HasForeignKey("ShootingPermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassTrackingSystem.Models.StationFacility", null)
                        .WithMany()
                        .HasForeignKey("StationFacilitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SinglePassStationFacility", b =>
                {
                    b.HasOne("PassTrackingSystem.Models.SinglePass", null)
                        .WithMany()
                        .HasForeignKey("SinglePassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassTrackingSystem.Models.StationFacility", null)
                        .WithMany()
                        .HasForeignKey("StationFacilitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StationFacilityTemporaryPass", b =>
                {
                    b.HasOne("PassTrackingSystem.Models.StationFacility", null)
                        .WithMany()
                        .HasForeignKey("StationFacilitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassTrackingSystem.Models.TemporaryPass", null)
                        .WithMany()
                        .HasForeignKey("TemporaryPassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PassTrackingSystem.Models.CarPass", b =>
                {
                    b.Navigation("Car");
                });

            modelBuilder.Entity("PassTrackingSystem.Models.Visitor", b =>
                {
                    b.Navigation("CarPasses");

                    b.Navigation("Document");

                    b.Navigation("ShootingPermissions");

                    b.Navigation("SinglePasses");

                    b.Navigation("TemporaryPasses");
                });
#pragma warning restore 612, 618
        }
    }
}