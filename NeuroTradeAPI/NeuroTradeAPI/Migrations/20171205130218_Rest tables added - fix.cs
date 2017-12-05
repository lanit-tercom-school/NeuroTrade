using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NeuroTradeAPI.Migrations
{
    public partial class Resttablesaddedfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Algorithms",
                columns: table => new
                {
                    AlgorithmId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Algorithms", x => x.AlgorithmId);
                    table.ForeignKey(
                        name: "FK_Algorithms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainedModels",
                columns: table => new
                {
                    TrainedModelId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AlgorithmId = table.Column<int>(type: "int4", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    InstrumentId = table.Column<int>(type: "int4", nullable: false),
                    Parameters = table.Column<string>(type: "text", nullable: true),
                    Performance = table.Column<float>(type: "float4", nullable: false),
                    TestBegin = table.Column<DateTime>(type: "timestamp", nullable: false),
                    TestEnd = table.Column<DateTime>(type: "timestamp", nullable: false),
                    TrainBegin = table.Column<DateTime>(type: "timestamp", nullable: false),
                    TrainEnd = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainedModels", x => x.TrainedModelId);
                    table.ForeignKey(
                        name: "FK_TrainedModels_Algorithms_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "Algorithms",
                        principalColumn: "AlgorithmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainedModels_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "InstrumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Algorithms_UserId",
                table: "Algorithms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainedModels_AlgorithmId",
                table: "TrainedModels",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainedModels_InstrumentId",
                table: "TrainedModels",
                column: "InstrumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainedModels");

            migrationBuilder.DropTable(
                name: "Algorithms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
