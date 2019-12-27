using Microsoft.AspNetCore.Mvc;
using StudentRecords.Entities;
using StudentRecords.web.Clients;
using System.Threading.Tasks;
using Mirosoft.Utilities;

namespace StudentRecords.web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentsClient _studentsClient;

        public StudentsController(StudentsClient studentsClient)
        {
            _studentsClient = studentsClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string keywork)
        {
            var result = await _studentsClient.GetStudents(keywork);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _studentsClient.AddStudent(model);
            return RedirectToAction(nameof(Search));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _studentsClient.GetStudent(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student model)
        {
            var student = await _studentsClient.GetStudent(model.Id);
            if (student == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _studentsClient.UpdateStudent(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _studentsClient.GetStudent(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student model)
        {
            var student = await _studentsClient.GetStudent(model.Id);
            if (student == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _studentsClient.DeleteStudent(model.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}