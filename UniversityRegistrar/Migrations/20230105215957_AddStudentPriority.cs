using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityRegistrar.Migrations
{
    public partial class AddStudentPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CourseId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CourseId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Students");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollmentDate",
                table: "Students",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MajorCourses_MajorId",
                table: "MajorCourses",
                column: "MajorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MajorCourses_Majors_MajorId",
                table: "MajorCourses",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "MajorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MajorCourses_Majors_MajorId",
                table: "MajorCourses");

            migrationBuilder.DropIndex(
                name: "IX_MajorCourses_MajorId",
                table: "MajorCourses");

            migrationBuilder.AlterColumn<string>(
                name: "EnrollmentDate",
                table: "Students",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseId",
                table: "Students",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CourseId",
                table: "Students",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");
        }
    }
}
