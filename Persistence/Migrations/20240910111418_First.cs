using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MetaBank.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    PinToken = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Holder = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    TriesLeftToBlock = table.Column<int>(type: "int", nullable: false, defaultValue: 4),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CashAvailable = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Withdrawals_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CreatedAt", "Holder", "Number", "PinToken", "TriesLeftToBlock", "UpdatedAt" },
                values: new object[,]
                {
                    { -3, new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(6679), new TimeSpan(0, 0, 0, 0, 0)), "Greta Garbo", "1111222233334444", "$2a$11$rP9n8XBhOGPfeKmi99JO4eU.SzcsgumEmpDwABP57MdTCh7hxWjz2", 4, new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(6680), new TimeSpan(0, 0, 0, 0, 0)) },
                    { -2, new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(6674), new TimeSpan(0, 0, 0, 0, 0)), "Joe Doe", "4321432143214321", "$2a$11$rP9n8XBhOGPfeKmi99JO4eU.SzcsgumEmpDwABP57MdTCh7hxWjz2", 4, new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(6676), new TimeSpan(0, 0, 0, 0, 0)) },
                    { -1, new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(6645), new TimeSpan(0, 0, 0, 0, 0)), "Sam Smith", "1234123412341234", "$2a$11$rP9n8XBhOGPfeKmi99JO4eU.SzcsgumEmpDwABP57MdTCh7hxWjz2", 4, new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(6669), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CardId", "CashAvailable", "CreatedAt", "Number", "UpdatedAt" },
                values: new object[,]
                {
                    { -3, -3, 1111111111L, new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(8058), new TimeSpan(0, 0, 0, 0, 0)), "9876543210", new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(8059), new TimeSpan(0, 0, 0, 0, 0)) },
                    { -2, -2, 555555555L, new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(8053), new TimeSpan(0, 0, 0, 0, 0)), "0987654321", new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(8055), new TimeSpan(0, 0, 0, 0, 0)) },
                    { -1, -1, 10000000L, new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(8043), new TimeSpan(0, 0, 0, 0, 0)), "1234567890", new DateTimeOffset(new DateTime(2024, 9, 10, 11, 14, 17, 965, DateTimeKind.Unspecified).AddTicks(8046), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CardId",
                table: "Accounts",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedAt",
                table: "Accounts",
                column: "CreatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UpdatedAt",
                table: "Accounts",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawals_AccountId",
                table: "Withdrawals",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawals_CreatedAt",
                table: "Withdrawals",
                column: "CreatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawals_UpdatedAt",
                table: "Withdrawals",
                column: "UpdatedAt",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Withdrawals");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
