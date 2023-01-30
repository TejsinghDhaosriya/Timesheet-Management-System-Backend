﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TimesheetService.DBContext;

#nullable disable

namespace TimesheetService.Migrations
{
    [DbContext(typeof(TimeSheetDbContext))]
    partial class TimeSheetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TimesheetService.Models.Approval", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<DateTime>("approval_date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("manager_id")
                        .HasColumnType("bigint");

                    b.Property<long>("organization_id")
                        .HasColumnType("bigint");

                    b.Property<string>("reason_for_rejection")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("timesheet_idid")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("timesheet_idid");

                    b.ToTable("Approvals");
                });

            modelBuilder.Entity("TimesheetService.Models.Project", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("end_date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("is_active")
                        .HasColumnType("boolean");

                    b.Property<long>("manager_id")
                        .HasColumnType("bigint");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("organization_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("start_date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("total_time_spent")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TimesheetService.Models.TimeSheet", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<long>("created_by")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeOnly>("end_time")
                        .HasColumnType("time without time zone");

                    b.Property<long>("organization_id")
                        .HasColumnType("bigint");

                    b.Property<int>("overtime_hours")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("start_time")
                        .HasColumnType("time without time zone");

                    b.Property<int>("total_hours")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("TimeSheets");
                });

            modelBuilder.Entity("TimesheetService.Models.Approval", b =>
                {
                    b.HasOne("TimesheetService.Models.TimeSheet", "timesheet_id")
                        .WithMany("approvals")
                        .HasForeignKey("timesheet_idid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("timesheet_id");
                });

            modelBuilder.Entity("TimesheetService.Models.TimeSheet", b =>
                {
                    b.Navigation("approvals");
                });
#pragma warning restore 612, 618
        }
    }
}
