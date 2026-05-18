using WebQuanLyDuAn.Models;

namespace WebQuanLyDuAn.Data
{
    /// <summary>
    /// Kho dữ liệu in-memory (static).
    /// Sau này có thể thay thế bằng DbContext + Entity Framework.
    /// </summary>
    public static class InMemoryDataStore
    {
        private static int _projectIdCounter = 4;
        private static int _taskIdCounter = 7;

        public static List<Project> Projects { get; } = new()
        {
            new Project
            {
                Id = 1,
                Name = "Website Thương Mại Điện Tử",
                Description = "Xây dựng nền tảng bán hàng online cho công ty.",
                StartDate = new DateTime(2025, 1, 15),
                EndDate   = new DateTime(2025, 6, 30),
                Status    = ProjectStatus.DangThucHien,
                Priority  = PriorityLevel.Cao,
                ManagerName = "Nguyễn Văn An"
            },
            new Project
            {
                Id = 2,
                Name = "App Quản Lý Nhân Sự",
                Description = "Ứng dụng mobile quản lý chấm công và lương.",
                StartDate = new DateTime(2025, 3, 1),
                EndDate   = new DateTime(2025, 9, 1),
                Status    = ProjectStatus.ChuaBatDau,
                Priority  = PriorityLevel.TrungBinh,
                ManagerName = "Trần Thị Bình"
            },
            new Project
            {
                Id = 3,
                Name = "Hệ Thống Báo Cáo BI",
                Description = "Dashboard phân tích dữ liệu kinh doanh theo thời gian thực.",
                StartDate = new DateTime(2024, 10, 1),
                EndDate   = new DateTime(2025, 2, 28),
                Status    = ProjectStatus.HoanThanh,
                Priority  = PriorityLevel.KhanCap,
                ManagerName = "Lê Minh Châu"
            }
        };

        public static List<ProjectTask> Tasks { get; } = new()
        {
            new ProjectTask { Id=1, ProjectId=1, Title="Thiết kế giao diện trang chủ",   AssignedTo="Phạm Văn D",   DueDate=new DateTime(2025,2,15), Status=WorkStatus.HoanThanh, Priority=PriorityLevel.Cao },
            new ProjectTask { Id=2, ProjectId=1, Title="Xây dựng API giỏ hàng",          AssignedTo="Nguyễn Thị E", DueDate=new DateTime(2025,3,10), Status=WorkStatus.DangLam,   Priority=PriorityLevel.Cao },
            new ProjectTask { Id=3, ProjectId=1, Title="Tích hợp cổng thanh toán",       AssignedTo="Trần Văn F",   DueDate=new DateTime(2025,4,20), Status=WorkStatus.CanLam,    Priority=PriorityLevel.KhanCap },
            new ProjectTask { Id=4, ProjectId=2, Title="Phân tích yêu cầu hệ thống",    AssignedTo="Lê Thị G",    DueDate=new DateTime(2025,3,20), Status=WorkStatus.CanLam,    Priority=PriorityLevel.TrungBinh },
            new ProjectTask { Id=5, ProjectId=2, Title="Thiết kế database nhân sự",     AssignedTo="Hoàng Văn H",  DueDate=new DateTime(2025,4,5),  Status=WorkStatus.CanLam,    Priority=PriorityLevel.TrungBinh },
            new ProjectTask { Id=6, ProjectId=3, Title="Kết nối nguồn dữ liệu ERP",     AssignedTo="Vũ Thị I",    DueDate=new DateTime(2024,11,1), Status=WorkStatus.HoanThanh, Priority=PriorityLevel.KhanCap },
        };

        // ── Project CRUD ────────────────────────────────────────────────────────

        public static int NextProjectId() => ++_projectIdCounter;

        public static Project? GetProject(int id) =>
            Projects.FirstOrDefault(p => p.Id == id);

        public static void AddProject(Project project)
        {
            project.Id = NextProjectId();
            Projects.Add(project);
        }

        public static bool UpdateProject(Project updated)
        {
            var existing = GetProject(updated.Id);
            if (existing == null) return false;
            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.StartDate = updated.StartDate;
            existing.EndDate = updated.EndDate;
            existing.Status = updated.Status;
            existing.Priority = updated.Priority;
            existing.ManagerName = updated.ManagerName;
            return true;
        }

        public static bool DeleteProject(int id)
        {
            var project = GetProject(id);
            if (project == null) return false;
            Tasks.RemoveAll(t => t.ProjectId == id); // xóa task liên quan
            Projects.Remove(project);
            return true;
        }

        // ── Task CRUD ────────────────────────────────────────────────────────────

        public static int NextTaskId() => ++_taskIdCounter;

        public static ProjectTask? GetTask(int id) =>
            Tasks.FirstOrDefault(t => t.Id == id);

        public static List<ProjectTask> GetTasksByProject(int projectId) =>
            Tasks.Where(t => t.ProjectId == projectId).ToList();

        public static void AddTask(ProjectTask task)
        {
            task.Id = NextTaskId();
            Tasks.Add(task);
        }

        public static bool UpdateTask(ProjectTask updated)
        {
            var existing = GetTask(updated.Id);
            if (existing == null) return false;
            existing.Title = updated.Title;
            existing.Description = updated.Description;
            existing.ProjectId = updated.ProjectId;
            existing.AssignedTo = updated.AssignedTo;
            existing.DueDate = updated.DueDate;
            existing.Status = updated.Status;
            existing.Priority = updated.Priority;
            return true;
        }

        public static bool DeleteTask(int id)
        {
            var task = GetTask(id);
            if (task == null) return false;
            Tasks.Remove(task);
            return true;
        }
    }
}