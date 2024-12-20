﻿// <auto-generated />
using System;
using DevOpsApi.core.api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DevOpsApi.core.api.Migrations
{
    [DbContext(typeof(STRDMSContext))]
    [Migration("20241114042352_init tempus db")]
    partial class inittempusdb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DevOpsApi.core.api.Data.Entities.Employee", b =>
                {
                    b.Property<long>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EmployeeID"));

                    b.Property<long?>("HomeCompanyDepartmentID")
                        .HasColumnType("bigint");

                    b.Property<long?>("HomeCompanyID")
                        .HasColumnType("bigint");

                    b.HasKey("EmployeeID");

                    b.ToTable("tblEmployee");
                });

            modelBuilder.Entity("DevOpsApi.core.api.Data.Entities.Location", b =>
                {
                    b.Property<long>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LocationID"));

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsPDC")
                        .HasColumnType("bit");

                    b.Property<string>("LocationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RimproWarehouseCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationID");

                    b.ToTable("tblLocation");
                });

            modelBuilder.Entity("DevOpsApi.core.api.Data.Entities.POSConfiguration", b =>
                {
                    b.Property<int>("POSConfigurationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("POSConfigurationID"));

                    b.Property<long>("CompanyDepartmentID")
                        .HasColumnType("bigint");

                    b.Property<long>("CompanyID")
                        .HasColumnType("bigint");

                    b.Property<string>("RNCert")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RNID")
                        .HasColumnType("int");

                    b.HasKey("POSConfigurationID");

                    b.ToTable("tblPOSConfiguration");
                });

            modelBuilder.Entity("DevOpsApi.core.api.Data.Entities.POSDeviceConfiguration", b =>
                {
                    b.Property<int>("POSDeviceConfigurationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("POSDeviceConfigurationID"));

                    b.Property<int>("Active")
                        .HasColumnType("int");

                    b.Property<long>("CompanyDepartmentID")
                        .HasColumnType("bigint");

                    b.Property<long>("CompanyID")
                        .HasColumnType("bigint");

                    b.Property<string>("DeviceAlias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("POSConfigurationID")
                        .HasColumnType("int");

                    b.Property<string>("Subscriberkey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("POSDeviceConfigurationID");

                    b.HasIndex("POSConfigurationID");

                    b.ToTable("tblPOSDeviceConfiguration");
                });

            modelBuilder.Entity("DevOpsApi.core.api.Data.Entities.POSDeviceConfigurationHostName", b =>
                {
                    b.Property<int>("POSDeviceConfigurationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("POSDeviceConfigurationID"));

                    b.Property<string>("DeviceAlias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsDefault")
                        .HasColumnType("int");

                    b.HasKey("POSDeviceConfigurationID");

                    b.ToTable("POSDeviceConfigurationHostNames");
                });

            modelBuilder.Entity("DevOpsApi.core.api.Data.Entities.POSLoginDetail", b =>
                {
                    b.Property<long>("POSLoginID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("POSLoginID"));

                    b.Property<long?>("CompanyDepartmentID")
                        .HasColumnType("bigint");

                    b.Property<string>("DeviceAlias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EmpID")
                        .HasColumnType("bigint");

                    b.Property<string>("HostIP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LoginDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LoginStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LogoutDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("POSDeviceConfigurationID")
                        .HasColumnType("int");

                    b.HasKey("POSLoginID");

                    b.ToTable("tblPOSLoginDetails");
                });

            modelBuilder.Entity("DevOpsApi.core.api.Data.Entities.PosInvoice", b =>
                {
                    b.Property<long>("SalesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SalesId"));

                    b.Property<decimal>("AdditionalCharges")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("AdditionalChargesQty")
                        .HasColumnType("bigint");

                    b.Property<long>("CompanyDepartmentID")
                        .HasColumnType("bigint");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerTaxRate")
                        .HasColumnType("int");

                    b.Property<decimal>("Freight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ItemTaxAmount")
                        .HasColumnType("int");

                    b.Property<decimal>("Parts")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("PartsQty")
                        .HasColumnType("bigint");

                    b.Property<string>("SalesNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SalesId");

                    b.ToTable("POSInvoices");
                });

            modelBuilder.Entity("DevOpsApi.core.api.Data.Entities.SISPosInvoice", b =>
                {
                    b.Property<long>("SalesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SalesId"));

                    b.Property<decimal>("AdditionalCharges")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Additional Charges");

                    b.Property<long>("CompanyDepartmentID")
                        .HasColumnType("bigint");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Freight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FreightQty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Labor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LaborQty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Paint")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PaintQty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Parts")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PartsQty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SalesNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ServiceAddChargeQty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Sublet")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SubletQty")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SalesId");

                    b.ToTable("SISPOSInvoices");
                });

            modelBuilder.Entity("DevOpsApi.core.api.Data.Entities.POSDeviceConfiguration", b =>
                {
                    b.HasOne("DevOpsApi.core.api.Data.Entities.POSConfiguration", "POSConfiguration")
                        .WithMany()
                        .HasForeignKey("POSConfigurationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("POSConfiguration");
                });
#pragma warning restore 612, 618
        }
    }
}
