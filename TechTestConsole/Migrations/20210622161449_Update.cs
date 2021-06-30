using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTestConsole.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Students_StudentID",
                table: "ExamResults");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Subjects_SubjectID",
                table: "ExamResults");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Students",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SubjectID",
                table: "ExamResults",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "ExamResults",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamResults_SubjectID",
                table: "ExamResults",
                newName: "IX_ExamResults_SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Students_StudentId",
                table: "ExamResults",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Subjects_SubjectId",
                table: "ExamResults",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Students_StudentId",
                table: "ExamResults");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Subjects_SubjectId",
                table: "ExamResults");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "ExamResults",
                newName: "SubjectID");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "ExamResults",
                newName: "StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_ExamResults_SubjectId",
                table: "ExamResults",
                newName: "IX_ExamResults_SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Students_StudentID",
                table: "ExamResults",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Subjects_SubjectID",
                table: "ExamResults",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
