using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KgNet88.Woden.Account.Infrastructure.Profile.Persistence.Database.Migrations;

public partial class InitProfile : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.EnsureSchema(
            name: "account");

        _ = migrationBuilder.CreateTable(
            name: "Profiles",
            schema: "account",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                DisplayName = table.Column<string>(type: "text", nullable: false),
                ProfileEmail = table.Column<string>(type: "text", nullable: false),
                MatrixId = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Profiles", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "Profiles",
            schema: "account");
    }
}
