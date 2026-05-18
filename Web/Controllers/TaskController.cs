using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebQuanLyDuAn.Data;
using WebQuanLyDuAn.Models;

namespace WebQuanLyDuAn.Controllers
{
    public class TaskController : Controller
    {
        // GET: /Task  (hoặc /Task?projectId=1 để lọc theo dự án)
        public IActionResult Index(int? projectId)
        {
            var tasks = projectId.HasValue
                ? InMemoryDataStore.GetTasksByProject(projectId.Value)
                : InMemoryDataStore.Tasks.OrderBy(t => t.DueDate).ToList();

            ViewBag.ProjectId = projectId;
            ViewBag.Projects = InMemoryDataStore.Projects;
            return View(tasks);
        }

        // GET: /Task/Display/5
        public IActionResult Display(int id)
        {
            var task = InMemoryDataStore.GetTask(id);
            if (task == null) return NotFound();

            ViewBag.Project = InMemoryDataStore.GetProject(task.ProjectId);
            return View(task);
        }

        // GET: /Task/Add?projectId=1
        public IActionResult Add(int? projectId)
        {
            LoadDropdowns(projectId);
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
        public IActionResult Add(ProjectTask task)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns(task.ProjectId);
                return View(task);
            }
            InMemoryDataStore.AddTask(task);
            TempData["Success"] = $"Đã thêm công việc \"{task.Title}\" thành công!";
            return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
        }

        // GET: /Task/Update/5
        public IActionResult Update(int id)
        {
            var task = InMemoryDataStore.GetTask(id);
            if (task == null) return NotFound();
            LoadDropdowns(task.ProjectId);
            return View(task);
        }

        // POST: /Task/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProjectTask task)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns(task.ProjectId);
                return View(task);
            }
            InMemoryDataStore.UpdateTask(task);
            TempData["Success"] = $"Đã cập nhật công việc \"{task.Title}\" thành công!";
            return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
        }

        // GET: /Task/Delete/5
        public IActionResult Delete(int id)
        {
            var task = InMemoryDataStore.GetTask(id);
            if (task == null) return NotFound();
            ViewBag.Project = InMemoryDataStore.GetProject(task.ProjectId);
            return View(task);
        }

        // POST: /Task/DeleteConfirmed
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = InMemoryDataStore.GetTask(id);
            var title = task?.Title ?? "";
            var projectId = task?.ProjectId;
            InMemoryDataStore.DeleteTask(id);
            TempData["Success"] = $"Đã xóa công việc \"{title}\"!";
            return RedirectToAction(nameof(Index), new { projectId });
        }

        // ── helpers ─────────────────────────────────────────────────────────────

        private void LoadDropdowns(int? selectedProjectId = null)
        {
            ViewBag.ProjectList = InMemoryDataStore.Projects
                .Select(p => new SelectListItem
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