using Microsoft.EntityFrameworkCore.Migrations;

namespace fahim.Migrations
{
    public partial class Updated_Contract_21092508190948 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "AppContracts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppContracts_ContractId",
                table: "AppContracts",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppContracts_AppContracts_ContractId",
                table: "AppContracts",
                column: "ContractId",
                principalTable: "AppContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppContracts_AppContracts_ContractId",
                table: "AppContracts");

            migrationBuilder.DropIndex(
                name: "IX_AppContracts_ContractId",
                table: "AppContracts");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "AppContracts");
        }
    }
}
