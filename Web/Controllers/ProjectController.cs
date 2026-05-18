using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebQuanLyDuAn.Data;
using WebQuanLyDuAn.Models;

namespace WebQuanLyDuAn.Controllers
{
    public class ProjectController : Controller
    {
        // GET: /Project
        public IActionResult Index()
        {
            var projects = InMemoryDataStore.Projects
                .OrderByDescending(p => p.StartDate)
                .ToList();
            return View(projects);
        }

        // GET: /Project/Display/5
        public IActionResult Display(int id)
        {
            var project = InMemoryDataStore.GetProject(id);
            if (project == null) return NotFound();

            ViewBag.Tasks = InMemoryDataStore.GetTasksByProject(id);
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
        public IActionResult Add(Project project)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(project);
            }
            InMemoryDataStore.AddProject(project);
            TempData["Success"] = $"Đã thêm dự án \"{project.Name}\" thành công!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Project/Update/5
        public IActionResult Update(int id)
        {
            var project = InMemoryDataStore.GetProject(id);
            if (project == null) return NotFound();
            LoadDropdowns();
            return View(project);
        }

        // POST: /Project/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Project project)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(project);
            }
            InMemoryDataStore.UpdateProject(project);
            TempData["Success"] = $"Đã cập nhật dự án \"{project.Name}\" thành công!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Project/Delete/5
        public IActionResult Delete(int id)
        {
            var project = InMemoryDataStore.GetProject(id);
            if (project == null) return NotFound();
            return View(project);
        }

        // POST: /Project/DeleteConfirmed
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = InMemoryDataStore.GetProject(id);
            var name = project?.Name ?? "";
            InMemoryDataStore.DeleteProject(id);
            TempData["Success"] = $"Đã xóa dự án \"{name}\" và tất cả công việc liên quan!";
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