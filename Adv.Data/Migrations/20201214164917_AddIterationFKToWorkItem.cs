using Microsoft.EntityFrameworkCore.Migrations;

namespace Adv.Data.Migrations
{
    public partial class AddIterationFKToWorkItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_IterationId",
                table: "WorkItems",
                column: "IterationId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Iterations_IterationId",
                table: "WorkItems",
                column: "IterationId",
                principalTable: "Iterations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Iterations_IterationId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_IterationId",
                table: "WorkItems");
        }
    }
}
