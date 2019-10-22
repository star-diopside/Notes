using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Notes.Web.Migrations
{
    public partial class CreateUploadFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "upload_files",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<string>(nullable: false),
                    content_type = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    hash_value = table.Column<string>(maxLength: 64, nullable: false),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_upload_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "upload_file_data",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    upload_file_id = table.Column<int>(nullable: false),
                    data = table.Column<byte[]>(nullable: true),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_upload_file_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_upload_file_data_upload_files_upload_file_id",
                        column: x => x.upload_file_id,
                        principalTable: "upload_files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_upload_file_data_upload_file_id",
                table: "upload_file_data",
                column: "upload_file_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "upload_file_data");

            migrationBuilder.DropTable(
                name: "upload_files");
        }
    }
}
