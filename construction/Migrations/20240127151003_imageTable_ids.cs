using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace construction.Migrations
{
    /// <inheritdoc />
    public partial class imageTable_ids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobs_JobTypes_job_type",
                table: "jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobTypes",
                table: "JobTypes");

            migrationBuilder.RenameTable(
                name: "JobTypes",
                newName: "job_types");

            migrationBuilder.AddPrimaryKey(
                name: "PK_job_types",
                table: "job_types",
                column: "name");

            migrationBuilder.CreateTable(
                name: "jobs_images",
                columns: table => new
                {
                    image_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    job_id = table.Column<int>(type: "integer", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true),
                    job_id1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs_images", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_jobs_images_jobs_job_id1",
                        column: x => x.job_id1,
                        principalTable: "jobs",
                        principalColumn: "job_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_jobs_images_job_id1",
                table: "jobs_images",
                column: "job_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_jobs_job_types_job_type",
                table: "jobs",
                column: "job_type",
                principalTable: "job_types",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobs_job_types_job_type",
                table: "jobs");

            migrationBuilder.DropTable(
                name: "jobs_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_job_types",
                table: "job_types");

            migrationBuilder.RenameTable(
                name: "job_types",
                newName: "JobTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobTypes",
                table: "JobTypes",
                column: "name");

            migrationBuilder.AddForeignKey(
                name: "FK_jobs_JobTypes_job_type",
                table: "jobs",
                column: "job_type",
                principalTable: "JobTypes",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
