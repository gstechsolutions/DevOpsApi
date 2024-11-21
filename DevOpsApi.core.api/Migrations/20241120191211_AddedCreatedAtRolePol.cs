using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevOpsApi.core.api.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreatedAtRolePol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RolePolicies",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RolePolicies");
        }
    }
}
