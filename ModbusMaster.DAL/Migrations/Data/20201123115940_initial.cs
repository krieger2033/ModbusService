using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace ModbusMaster.DAL.Migrations.Data
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    ComPort = table.Column<string>(nullable: true),
                    Baudrate = table.Column<int>(nullable: true),
                    Parity = table.Column<int>(nullable: true),
                    StopBits = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    ChannelId = table.Column<int>(nullable: false),
                    Ip = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: true),
                    Identificator = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dumps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<int>(nullable: false),
                    RegisterType = table.Column<int>(nullable: false),
                    Offset = table.Column<int>(nullable: false),
                    Data = table.Column<int>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dumps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dumps_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Offset = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Formula = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registers_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "Baudrate", "ComPort", "Parity", "StopBits", "Type" },
                values: new object[] { 1, null, null, null, null, 0 });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ChannelId", "Identificator", "Ip", "Port", "Type" },
                values: new object[] { 1, 1, null, "127.0.0.1", 502, 0 });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ChannelId", "Identificator", "Ip", "Port", "Type" },
                values: new object[] { 2, 1, null, "127.0.0.2", 502, 0 });

            migrationBuilder.InsertData(
                table: "Registers",
                columns: new[] { "Id", "Count", "DeviceId", "Formula", "Offset", "Type" },
                values: new object[] { 1, 5, 1, null, 0, 20001 });

            migrationBuilder.InsertData(
                table: "Registers",
                columns: new[] { "Id", "Count", "DeviceId", "Formula", "Offset", "Type" },
                values: new object[] { 2, 3, 2, null, 0, 10001 });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ChannelId",
                table: "Devices",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Dumps_DeviceId",
                table: "Dumps",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_DeviceId",
                table: "Registers",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dumps");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Channels");
        }
    }
}
