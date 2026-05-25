using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebQuanLyDuAn;
using WebQuanLyDuAn.Data;
using WebQuanLyDuAn.Models;

namespace WebQuanLyDuAn.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDbContext _db;

        public TaskController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /Task  hoặc /Task?projectId=1
        public async Task<IActionResult> Index(int? projectId)
        {
            var query = _db.Tasks.AsQueryable();

            if (projectId.HasValue)
                query = query.Where(t => t.ProjectId == projectId.Value);

            var tasks = await query.OrderBy(t => t.DueDate).ToListAsync();

            ViewBag.ProjectId = projectId;
            ViewBag.Projects = await _db.Projects.ToListAsync();
            return View(tasks);
        }

        // GET: /Task/Display/5
        public async Task<IActionResult> Display(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            ViewBag.Project = await _db.Projects.FindAsync(task.ProjectId);
            return View(task);
        }

        // GET: /Task/Add?projectId=1
        public async Task<IActionResult> Add(int? projectId)
        {
            await LoadDropdownsAsync(projectId);
            var model = new ProjectTask
            {
                ProjectId = projectId ?? 0,
                DueDate = DateTime.Today.AddDays(7)
            };
            return View(model);
        }

        // POST: /Task/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProjectTask task)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(task.ProjectId);
                return View(task);
            }
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Đã thêm công việc \"{task.Title}\" thành công!";
            return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
        }

        // GET: /Task/Update/5
        public async Task<IActionResult> Update(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            await LoadDropdownsAsync(task.ProjectId);
            return View(task);
        }

        // POST: /Task/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProjectTask task)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(task.ProjectId);
                return View(task);
            }
            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Đã cập nhật công việc \"{task.Title}\" thành công!";
            return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
        }

        // GET: /Task/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            ViewBag.Project = await _db.Projects.FindAsync(task.ProjectId);
            return View(task);
        }

        // POST: /Task/DeleteConfirmed
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task != null)
            {
                var projectId = task.ProjectId;
                _db.Tasks.Remove(task);
                await _db.SaveChangesAsync();
                TempData["Success"] = $"Đã xóa công việc \"{task.Title}\"!";
                return RedirectToAction(nameof(Index), new { projectId });
            }
            return RedirectToAction(nameof(Index));
        }

        // ── helpers ─────────────────────────────────────────────────────────────

        private async Task LoadDropdownsAsync(int? selectedProjectId = null)
        {
            var projects = await _db.Projects.ToListAsync();

            ViewBag.ProjectList = projects.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name,
                Selected = p.Id == selectedProjectId
            });

            ViewBag.StatusList = Enum.GetValues<WorkStatus>()
                .Select(s => new SelectListItem
                {
                    Value = ((int)s).ToString(),
                    Text = s.GetDisplayName()
                });

            ViewBag.PriorityList = Enum.GetValues<PriorityLevel>()
                .Select(p => new SelectListItem
                {
                    Value = ((int)p).ToString(),
                    Text = p.GetDisplayName()
                });
        }
    }
}