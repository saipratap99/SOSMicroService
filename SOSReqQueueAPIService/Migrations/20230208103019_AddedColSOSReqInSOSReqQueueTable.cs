using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSReqQueueAPIService.Migrations
{
    public partial class AddedColSOSReqInSOSReqQueueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SOSReqQueue_Users_PoliceId",
                table: "SOSReqQueue");

            migrationBuilder.AlterColumn<int>(
                name: "PoliceId",
                table: "SOSReqQueue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SOSRequestId",
                table: "SOSReqQueue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SOSReqQueue_SOSRequestId",
                table: "SOSReqQueue",
                column: "SOSRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_SOSReqQueue_SOSRequests_SOSRequestId",
                table: "SOSReqQueue",
                column: "SOSRequestId",
                principalTable: "SOSRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SOSReqQueue_Users_PoliceId",
                table: "SOSReqQueue",
                column: "PoliceId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SOSReqQueue_SOSRequests_SOSRequestId",
                table: "SOSReqQueue");

            migrationBuilder.DropForeignKey(
                name: "FK_SOSReqQueue_Users_PoliceId",
                table: "SOSReqQueue");

            migrationBuilder.DropIndex(
                name: "IX_SOSReqQueue_SOSRequestId",
                table: "SOSReqQueue");

            migrationBuilder.DropColumn(
                name: "SOSRequestId",
                table: "SOSReqQueue");

            migrationBuilder.AlterColumn<int>(
                name: "PoliceId",
                table: "SOSReqQueue",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SOSReqQueue_Users_PoliceId",
                table: "SOSReqQueue",
                column: "PoliceId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
