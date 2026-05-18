using Microsoft.AspNetCore.Mvc;
using WebQuanLyDuAn.Data;
using WebQuanLyDuAn.Models;

namespace WebQuanLyDuAn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var projects = InMemoryDataStore.Projects;
            var tasks = InMemoryDataStore.Tasks;

            ViewBag.TotalProjects = projects.Count;
            ViewBag.TotalTasks = tasks.Count;
            ViewBag.InProgress = projects.Count(p => p.Status == ProjectStatus.DangThucHien);
            ViewBag.Completed = projects.Count(p => p.Status == ProjectStatus.HoanThanh);
            ViewBag.TasksDone = tasks.Count(t => t.Status == WorkStatus.HoanThanh);
            ViewBag.TasksBlocked = tasks.Count(t => t.Status == WorkStatus.BiChan);
            ViewBag.RecentProjects = projects.OrderByDescending(p => p.StartDate).Take(5).ToList();
            ViewBag.UpcomingTasks = tasks
                .Where(t => t.DueDate.HasValue && t.Status != WorkStatus.HoanThanh)
                .OrderBy(t => t.DueDate)
                .Take(5)
                .ToList();

            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = System.Diagnostics.Activity.Current?.Id
                            ?? HttpContext.TraceIdentifier
            });
        }
    }
}