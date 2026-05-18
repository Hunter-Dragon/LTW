using System.ComponentModel.DataAnnotations;

namespace WebQuanLyDuAn.Models
{
    public enum ProjectStatus
    {
        [Display(Name = "Chưa bắt đầu")] ChuaBatDau,
        [Display(Name = "Đang thực hiện")] DangThucHien,
        [Display(Name = "Hoàn thành")] HoanThanh,
        [Display(Name = "Tạm dừng")] TamDung,
        [Display(Name = "Đã huỷ")] DaHuy
    }

    public enum PriorityLevel
    {
        [Display(Name = "Thấp")] Thap,
        [Display(Name = "Trung bình")] TrungBinh,
        [Display(Name = "Cao")] Cao,
        [Display(Name = "Khẩn cấp")] KhanCap
    }

    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên dự án không được để trống")]
        [StringLength(200, ErrorMessage = "Tên dự án tối đa 200 ký tự")]
        [Display(Name = "Tên dự án")]
        public string Name { get; set; } = "";

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Trạng thái")]
        public ProjectStatus Status { get; set; } = ProjectStatus.ChuaBatDau;

        [Display(Name = "Độ ưu tiên")]
        public PriorityLevel Priority { get; set; } = PriorityLevel.TrungBinh;

        [Display(Name = "Người quản lý")]
        public string? ManagerName { get; set; }
    }
}