using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NeuroTradeAPI.Migrations
{
    public partial class Onemorereinitialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    InstrumentId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Alias = table.Column<string>(type: "text", nullable: true),
                    DownloadAlias = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.InstrumentId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    PersonalData = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BeginTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    InstrumentId = table.Column<int>(type: "int4", nullable: false),
                    Interval = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.BatchId);
                    table.ForeignKey(
                        name: "FK_Batches_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "InstrumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Algorithms",
                columns: table => new
                {
                    AlgorithmId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
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
                name: "Candles",
                columns: table => new
                {
                    CandleId = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BatchId = table.Column<int>(type: "int4", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Close = table.Column<float>(type: "float4", nullable: false),
                    High = table.Column<float>(type: "float4", nullable: false),
                    Low = table.Column<float>(type: "float4", nullable: false),
                    Open = table.Column<float>(type: "float4", nullable: false),
                    Volume = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candles", x => x.CandleId);
                    table.ForeignKey(
                        name: "FK_Candles_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainedModels",
                columns: table => new
                {
                    TrainedModelId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AlgorithmId = table.Column<int>(type: "int4", nullable: false),
                    Data = table.Column<string>(type: "jsonb", nullable: true),
                    InstrumentId = table.Column<int>(type: "int4", nullable: false),
                    Parameters = table.Column<string>(type: "jsonb", nullable: true),
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
                name: "IX_Batches_InstrumentId",
                table: "Batches",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Candles_BatchId",
                table: "Candles",
                column: "BatchId");

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
                name: "Candles");

            migrationBuilder.DropTable(
                name: "TrainedModels");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Algorithms");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
