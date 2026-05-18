using System.ComponentModel.DataAnnotations;

namespace WebQuanLyDuAn.Models
{
    public enum WorkStatus
    {
        [Display(Name = "Cần làm")] CanLam,
        [Display(Name = "Đang làm")] DangLam,
        [Display(Name = "Hoàn thành")] HoanThanh,
        [Display(Name = "Bị chặn")] BiChan
    }

    public class ProjectTask
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(300, ErrorMessage = "Tiêu đề tối đa 300 ký tự")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; } = "";

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Phải chọn dự án")]
        [Display(Name = "Dự án")]
        public int ProjectId { get; set; }

        [Display(Name = "Người được giao")]
        public string? AssignedTo { get; set; }

        [Display(Name = "Hạn hoàn thành")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Trạng thái")]
        public WorkStatus Status { get; set; } = WorkStatus.CanLam;

        [Display(Name = "Độ ưu tiên")]
        public PriorityLevel Priority { get; set; } = PriorityLevel.TrungBinh;
    }
}