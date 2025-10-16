using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asset.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "AssetHistories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SparePart",
                table: "AssetHistories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PerformedBy",
                table: "AssetHistories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AssetHistories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AssetHistories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedToUser",
                table: "AssetHistories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentPath",
                table: "AssetHistories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "AssetHistories",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromLocation",
                table: "AssetHistories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FromStatusId",
                table: "AssetHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromUser",
                table: "AssetHistories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnReason",
                table: "AssetHistories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SparePartSerialNumber",
                table: "AssetHistories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToLocation",
                table: "AssetHistories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToStatusId",
                table: "AssetHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToUser",
                table: "AssetHistories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetHistories_FromStatusId",
                table: "AssetHistories",
                column: "FromStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetHistories_ToStatusId",
                table: "AssetHistories",
                column: "ToStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_AssetStatuses_FromStatusId",
                table: "AssetHistories",
                column: "FromStatusId",
                principalTable: "AssetStatuses",
                principalColumn: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_AssetStatuses_ToStatusId",
                table: "AssetHistories",
                column: "ToStatusId",
                principalTable: "AssetStatuses",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_AssetStatuses_FromStatusId",
                table: "AssetHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_AssetStatuses_ToStatusId",
                table: "AssetHistories");

            migrationBuilder.DropIndex(
                name: "IX_AssetHistories_FromStatusId",
                table: "AssetHistories");

            migrationBuilder.DropIndex(
                name: "IX_AssetHistories_ToStatusId",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "AttachmentPath",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "FromLocation",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "FromStatusId",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "FromUser",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "ReturnReason",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "SparePartSerialNumber",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "ToLocation",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "ToStatusId",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "ToUser",
                table: "AssetHistories");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "AssetHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SparePart",
                table: "AssetHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PerformedBy",
                table: "AssetHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AssetHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AssetHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedToUser",
                table: "AssetHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
