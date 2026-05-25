using Microsoft.EntityFrameworkCore;
using WebQuanLyDuAn.Models;

namespace WebQuanLyDuAn.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Cấu hình bảng Project ────────────────────────────────────────
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Description).HasMaxLength(1000);
                entity.Property(p => p.ManagerName).HasMaxLength(200);
            });

            // ── Cấu hình bảng Tasks ──────────────────────────────────────────
            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title).IsRequired().HasMaxLength(300);
                entity.Property(t => t.Description).HasMaxLength(1000);
                entity.Property(t => t.AssignedTo).HasMaxLength(200);

                // Khóa ngoại: Task thuộc về Project
                entity.HasOne<Project>()
                      .WithMany()
                      .HasForeignKey(t => t.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade); // xóa project → xóa task theo
            });

            // ── Dữ liệu mẫu (Seed Data) ─────────────────────────────────────
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Name = "Website Thương Mại Điện Tử",
                    Description = "Xây dựng nền tảng bán hàng online cho công ty.",
                    StartDate = new DateTime(2025, 1, 15),
                    EndDate = new DateTime(2025, 6, 30),
                    Status = ProjectStatus.DangThucHien,
                    Priority = PriorityLevel.Cao,
                    ManagerName = "Nguyễn Văn An"
                },
                new Project
                {
                    Id = 2,
                    Name = "App Quản Lý Nhân Sự",
                    Description = "Ứng dụng mobile quản lý chấm công và lương.",
                    StartDate = new DateTime(2025, 3, 1),
                    EndDate = new DateTime(2025, 9, 1),
                    Status = ProjectStatus.ChuaBatDau,
                    Priority = PriorityLevel.TrungBinh,
                    ManagerName = "Trần Thị Bình"
                },
                new Project
                {
                    Id = 3,
                    Name = "Hệ Thống Báo Cáo BI",
                    Description = "Dashboard phân tích dữ liệu kinh doanh.",
                    StartDate = new DateTime(2024, 10, 1),
                    EndDate = new DateTime(2025, 2, 28),
                    Status = ProjectStatus.HoanThanh,
                    Priority = PriorityLevel.KhanCap,
                    ManagerName = "Lê Minh Châu"
                }
            );

            modelBuilder.Entity<ProjectTask>().HasData(
                new ProjectTask { Id = 1, ProjectId = 1, Title = "Thiết kế giao diện trang chủ", AssignedTo = "Phạm Văn D", DueDate = new DateTime(2025, 2, 15), Status = WorkStatus.HoanThanh, Priority = PriorityLevel.Cao },
                new ProjectTask { Id = 2, ProjectId = 1, Title = "Xây dựng API giỏ hàng", AssignedTo = "Nguyễn Thị E", DueDate = new DateTime(2025, 3, 10), Status = WorkStatus.DangLam, Priority = PriorityLevel.Cao },
                new ProjectTask { Id = 3, ProjectId = 1, Title = "Tích hợp cổng thanh toán", AssignedTo = "Trần Văn F", DueDate = new DateTime(2025, 4, 20), Status = WorkStatus.CanLam, Priority = PriorityLevel.KhanCap },
                new ProjectTask { Id = 4, ProjectId = 2, Title = "Phân tích yêu cầu hệ thống", AssignedTo = "Lê Thị G", DueDate = new DateTime(2025, 3, 20), Status = WorkStatus.CanLam, Priority = PriorityLevel.TrungBinh },
                new ProjectTask { Id = 5, ProjectId = 2, Title = "Thiết kế database nhân sự", AssignedTo = "Hoàng Văn H", DueDate = new DateTime(2025, 4, 5), Status = WorkStatus.CanLam, Priority = PriorityLevel.TrungBinh },
                new ProjectTask { Id = 6, ProjectId = 3, Title = "Kết nối nguồn dữ liệu ERP", AssignedTo = "Vũ Thị I", DueDate = new DateTime(2024, 11, 1), Status = WorkStatus.HoanThanh, Priority = PriorityLevel.KhanCap }
            );
        }
    }
}