using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSReqQueueAPIService.Migrations
{
    public partial class CreateSOSReqQueueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SOSReqQueue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SOSRequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PoliceId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOSReqQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SOSReqQueue_SOSRequests_SOSRequestId",
                        column: x => x.SOSRequestId,
                        principalTable: "SOSRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SOSReqQueue_Users_PoliceId",
                        column: x => x.PoliceId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SOSReqQueue_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SOSReqQueue_PoliceId",
                table: "SOSReqQueue",
                column: "PoliceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SOSReqQueue_SOSRequestId",
                table: "SOSReqQueue",
                column: "SOSRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SOSReqQueue_UserId",
                table: "SOSReqQueue",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SOSReqQueue");
        }
    }
}
