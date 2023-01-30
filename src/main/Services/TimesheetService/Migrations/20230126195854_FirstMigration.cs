﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TimesheetService.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    startdate = table.Column<DateTime>(name: "start_date", type: "timestamp with time zone", nullable: false),
                    enddate = table.Column<DateTime>(name: "end_date", type: "timestamp with time zone", nullable: false),
                    totaltimespent = table.Column<DateTime>(name: "total_time_spent", type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    managerid = table.Column<long>(name: "manager_id", type: "bigint", nullable: false),
                    organizationid = table.Column<long>(name: "organization_id", type: "bigint", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    starttime = table.Column<TimeOnly>(name: "start_time", type: "time without time zone", nullable: false),
                    endtime = table.Column<TimeOnly>(name: "end_time", type: "time without time zone", nullable: false),
                    totalhours = table.Column<int>(name: "total_hours", type: "integer", nullable: false),
                    createdby = table.Column<long>(name: "created_by", type: "bigint", nullable: false),
                    organizationid = table.Column<long>(name: "organization_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Approvals",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    timesheetidid = table.Column<long>(name: "timesheet_idid", type: "bigint", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    reasonforrejection = table.Column<string>(name: "reason_for_rejection", type: "text", nullable: false),
                    approvaldate = table.Column<DateTime>(name: "approval_date", type: "timestamp with time zone", nullable: false),
                    managerid = table.Column<long>(name: "manager_id", type: "bigint", nullable: false),
                    organizationid = table.Column<long>(name: "organization_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals", x => x.id);
                    table.ForeignKey(
                        name: "FK_Approvals_TimeSheets_timesheet_idid",
                        column: x => x.timesheetidid,
                        principalTable: "TimeSheets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_timesheet_idid",
                table: "Approvals",
                column: "timesheet_idid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approvals");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TimeSheets");
        }
    }
}
