using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebQuanLyDuAn.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    ManagerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    AssignedTo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "EndDate", "ManagerName", "Name", "Priority", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, "Xây dựng nền tảng bán hàng online cho công ty.", new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyễn Văn An", "Website Thương Mại Điện Tử", 2, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Ứng dụng mobile quản lý chấm công và lương.", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trần Thị Bình", "App Quản Lý Nhân Sự", 1, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, "Dashboard phân tích dữ liệu kinh doanh.", new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lê Minh Châu", "Hệ Thống Báo Cáo BI", 3, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssignedTo", "Description", "DueDate", "Priority", "ProjectId", "Status", "Title" },
                values: new object[,]
                {
                    { 1, "Phạm Văn D", null, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 2, "Thiết kế giao diện trang chủ" },
                    { 2, "Nguyễn Thị E", null, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 1, "Xây dựng API giỏ hàng" },
                    { 3, "Trần Văn F", null, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, 0, "Tích hợp cổng thanh toán" },
                    { 4, "Lê Thị G", null, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 0, "Phân tích yêu cầu hệ thống" },
                    { 5, "Hoàng Văn H", null, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 0, "Thiết kế database nhân sự" },
                    { 6, "Vũ Thị I", null, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 2, "Kết nối nguồn dữ liệu ERP" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
