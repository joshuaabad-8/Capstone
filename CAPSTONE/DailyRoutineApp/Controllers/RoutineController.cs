using Microsoft.AspNetCore.Mvc;
using DailyRoutineApp.Models;
using DailyRoutineApp.Services;

namespace DailyRoutineApp.Controllers
{
    public class RoutineController : Controller
    {
        private readonly RoutineService _service;
        public RoutineController(RoutineService service) => _service = service;

        public IActionResult Index()
        {
            var list = _service.GetAll();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create() => View(new Routine());

        [HttpPost]
        public IActionResult Create(Routine routine)
        {
            // Remove empty activities
            routine.Activities = routine.Activities.Where(a => !string.IsNullOrWhiteSpace(a.Name)).ToList();
            var added = _service.Add(routine);
            return RedirectToAction("Details", new { id = added.Id });
        }

        public IActionResult Details(int id)
        {
            var r = _service.Get(id);
            if (r == null) return NotFound();
            return View(r);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var r = _service.Get(id);
            if (r == null) return NotFound();
            // ensure we have 6 activity slots for the form
            while (r.Activities.Count < 6) r.Activities.Add(new Models.Activity());
            return View(r);
        }

        [HttpPost]
        public IActionResult Edit(Routine routine)
        {
            routine.Activities = routine.Activities.Where(a => !string.IsNullOrWhiteSpace(a.Name)).ToList();
            var ok = _service.Update(routine);
            if (!ok) return NotFound();
            return RedirectToAction("Details", new { id = routine.Id });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}