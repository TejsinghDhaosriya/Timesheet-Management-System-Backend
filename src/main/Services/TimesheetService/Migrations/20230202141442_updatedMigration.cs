using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimesheetService.Migrations
{
    /// <inheritdoc />
    public partial class updatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_TimeSheets_TimeSheetId",
                table: "Approvals");

            migrationBuilder.DropColumn(
                name: "overtime_hours",
                table: "TimeSheets");

            migrationBuilder.RenameColumn(
                name: "TimeSheetId",
                table: "Approvals",
                newName: "timesheet_id");

            migrationBuilder.RenameIndex(
                name: "IX_Approvals_TimeSheetId",
                table: "Approvals",
                newName: "IX_Approvals_timesheet_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at",
                table: "TimeSheets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "created_by",
                table: "TimeSheets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "TimeSheets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "approval_date",
                table: "Approvals",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_TimeSheets_timesheet_id",
                table: "Approvals",
                column: "timesheet_id",
                principalTable: "TimeSheets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_TimeSheets_timesheet_id",
                table: "Approvals");

            migrationBuilder.RenameColumn(
                name: "timesheet_id",
                table: "Approvals",
                newName: "TimeSheetId");

            migrationBuilder.RenameIndex(
                name: "IX_Approvals_timesheet_id",
                table: "Approvals",
                newName: "IX_Approvals_TimeSheetId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at",
                table: "TimeSheets",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<long>(
                name: "created_by",
                table: "TimeSheets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "TimeSheets",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "overtime_hours",
                table: "TimeSheets",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_at",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "Projects",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "approval_date",
                table: "Approvals",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_TimeSheets_TimeSheetId",
                table: "Approvals",
                column: "TimeSheetId",
                principalTable: "TimeSheets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
