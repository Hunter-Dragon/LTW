using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebQuanLyDuAn;
using WebQuanLyDuAn.Data;
using WebQuanLyDuAn.Models;

namespace WebQuanLyDuAn.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext _db;

        public ProjectController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /Project
        public async Task<IActionResult> Index()
        {
            var projects = await _db.Projects
                .OrderByDescending(p => p.StartDate)
                .ToListAsync();
            return View(projects);
        }

        // GET: /Project/Display/5
        public async Task<IActionResult> Display(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();

            ViewBag.Tasks = await _db.Tasks
                .Where(t => t.ProjectId == id)
                .ToListAsync();

            return View(project);
        }

        // GET: /Project/Add
        public IActionResult Add()
        {
            LoadDropdowns();
            return View();
        }

        // POST: /Project/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Project project)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(project);
            }
            _db.Projects.Add(project);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Đã thêm dự án \"{project.Name}\" thành công!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Project/Update/5
        public async Task<IActionResult> Update(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();
            LoadDropdowns();
            return View(project);
        }

        // POST: /Project/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Project project)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(project);
            }
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Đã cập nhật dự án \"{project.Name}\" thành công!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Project/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        // POST: /Project/DeleteConfirmed
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project != null)
            {
                _db.Projects.Remove(project); // Cascade xóa Tasks theo (cấu hình trong DbContext)
                await _db.SaveChangesAsync();
                TempData["Success"] = $"Đã xóa dự án \"{project.Name}\" và tất cả công việc liên quan!";
            }
            return RedirectToAction(nameof(Index));
        }

        // ── helpers ─────────────────────────────────────────────────────────────

        private void LoadDropdowns()
        {
            ViewBag.StatusList = Enum.GetValues<ProjectStatus>()
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