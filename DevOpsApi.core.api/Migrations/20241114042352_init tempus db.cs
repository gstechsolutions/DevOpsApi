using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevOpsApi.core.api.Migrations
{
    /// <inheritdoc />
    public partial class inittempusdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POSDeviceConfigurationHostNames",
                columns: table => new
                {
                    POSDeviceConfigurationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceAlias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSDeviceConfigurationHostNames", x => x.POSDeviceConfigurationID);
                });

            migrationBuilder.CreateTable(
                name: "POSInvoices",
                columns: table => new
                {
                    SalesId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyDepartmentID = table.Column<long>(type: "bigint", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartsQty = table.Column<long>(type: "bigint", nullable: false),
                    AdditionalCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdditionalChargesQty = table.Column<long>(type: "bigint", nullable: false),
                    Freight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerTaxRate = table.Column<int>(type: "int", nullable: false),
                    ItemTaxAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSInvoices", x => x.SalesId);
                });

            migrationBuilder.CreateTable(
                name: "SISPOSInvoices",
                columns: table => new
                {
                    SalesId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyDepartmentID = table.Column<long>(type: "bigint", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartsQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Freight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Labor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sublet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdditionalCharges = table.Column<decimal>(name: "Additional Charges", type: "decimal(18,2)", nullable: false),
                    Paint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaintQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LaborQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubletQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreightQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceAddChargeQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SISPOSInvoices", x => x.SalesId);
                });

            migrationBuilder.CreateTable(
                name: "tblEmployee",
                columns: table => new
                {
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeCompanyID = table.Column<long>(type: "bigint", nullable: true),
                    HomeCompanyDepartmentID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEmployee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "tblLocation",
                columns: table => new
                {
                    LocationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    RimproWarehouseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPDC = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLocation", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "tblPOSConfiguration",
                columns: table => new
                {
                    POSConfigurationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RNID = table.Column<int>(type: "int", nullable: false),
                    RNCert = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyID = table.Column<long>(type: "bigint", nullable: false),
                    CompanyDepartmentID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPOSConfiguration", x => x.POSConfigurationID);
                });

            migrationBuilder.CreateTable(
                name: "tblPOSLoginDetails",
                columns: table => new
                {
                    POSLoginID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpID = table.Column<long>(type: "bigint", nullable: false),
                    HostIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoginStatus = table.Column<bool>(type: "bit", nullable: false),
                    CompanyDepartmentID = table.Column<long>(type: "bigint", nullable: true),
                    DeviceAlias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POSDeviceConfigurationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPOSLoginDetails", x => x.POSLoginID);
                });

            migrationBuilder.CreateTable(
                name: "tblPOSDeviceConfiguration",
                columns: table => new
                {
                    POSDeviceConfigurationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subscriberkey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceAlias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<long>(type: "bigint", nullable: false),
                    CompanyDepartmentID = table.Column<long>(type: "bigint", nullable: false),
                    POSConfigurationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPOSDeviceConfiguration", x => x.POSDeviceConfigurationID);
                    table.ForeignKey(
                        name: "FK_tblPOSDeviceConfiguration_tblPOSConfiguration_POSConfigurationID",
                        column: x => x.POSConfigurationID,
                        principalTable: "tblPOSConfiguration",
                        principalColumn: "POSConfigurationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPOSDeviceConfiguration_POSConfigurationID",
                table: "tblPOSDeviceConfiguration",
                column: "POSConfigurationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POSDeviceConfigurationHostNames");

            migrationBuilder.DropTable(
                name: "POSInvoices");

            migrationBuilder.DropTable(
                name: "SISPOSInvoices");

            migrationBuilder.DropTable(
                name: "tblEmployee");

            migrationBuilder.DropTable(
                name: "tblLocation");

            migrationBuilder.DropTable(
                name: "tblPOSDeviceConfiguration");

            migrationBuilder.DropTable(
                name: "tblPOSLoginDetails");

            migrationBuilder.DropTable(
                name: "tblPOSConfiguration");
        }
    }
}
