using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project01.Migrations
{
    /// <inheritdoc />
    public partial class DBSC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckoutId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Checkouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOnCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpMonth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkouts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CheckoutId",
                table: "CartItems",
                column: "CheckoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Checkouts_CheckoutId",
                table: "CartItems",
                column: "CheckoutId",
                principalTable: "Checkouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Checkouts_CheckoutId",
                table: "CartItems");

            migrationBuilder.DropTable(
                name: "Checkouts");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CheckoutId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CheckoutId",
                table: "CartItems");
        }
    }
}
