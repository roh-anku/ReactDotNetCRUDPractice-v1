using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactCRUDSupport_v1.Migrations.ValuesDb
{
    /// <inheritdoc />
    public partial class valuesdbcreationinitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddOne",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddOne", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AddOne",
                columns: new[] { "Id", "Value" },
                values: new object[] { new Guid("eaef8e29-64f4-4536-a3a8-943d8b8eab0d"), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddOne");
        }
    }
}
