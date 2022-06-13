using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskTracker.DataAccess.Migrations
{
    public partial class AddSortIndexColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .AddColumn<WorkTask>(
                    name: "SortIndex",
                    table: "Tasks",
                    nullable: false,
                    defaultValue: 0,
                    type: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "SortIndex", table: "Tasks");
        }
    }
}
