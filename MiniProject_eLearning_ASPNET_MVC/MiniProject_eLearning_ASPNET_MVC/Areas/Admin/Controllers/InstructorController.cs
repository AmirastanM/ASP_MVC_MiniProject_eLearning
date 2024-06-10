using Microsoft.AspNetCore.Mvc;
using MiniProject_eLearning_ASPNET_MVC.Models;
using MiniProject_eLearning_ASPNET_MVC.Services.Interfaces;

namespace MiniProject_eLearning_ASPNET_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {

        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorService.GetAllAsync();
            return View(instructors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                await _instructorService.CreateAsync(instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _instructorService.EditAsync(id, instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _instructorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
