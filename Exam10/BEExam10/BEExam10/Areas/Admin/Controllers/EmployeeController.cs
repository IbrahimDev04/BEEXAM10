using BEExam10.DataAccessLayer;
using BEExam10.Models;
using BEExam10.Extensions;
using BEExam10.ViewModels.Employee;
using BEExam10.ViewModels.Job;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Authorization;

namespace BEExam10.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    public class EmployeeController(LumiaContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {

            var data = await _context.Employees
                .Include(j => j.Job)
                .Select(s => new GetEmployeeAdminVM
                {
                    FullName = s.FullName,
                    ImageUrl = s.ImageUrl,
                    InstagramUrl = s.InstagramUrl,
                    FacebookUrl= s.FacebookUrl,
                    LinkedInUrl = s.LinkedInUrl,
                    Description = s.Description,
                    TwitterUrl = s.TwitterUrl,
                    Id = s.Id,
                    Job = s.Job.Name.ToString(),
                    CreatedTime = s.CreatedTime.ToString("dd MMM yyyy"),
                    UpdatedTime = s.UpdatedTime.ToString("dd MMM yyyy")
                }).ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Job = await _context.Jobs.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM vm)
        {
            if (!ModelState.IsValid) return View(vm);


            if(vm.ImageFile != null)
            {
                if (!vm.ImageFile.IsValidType("image"))
                    ModelState.AddModelError("ImageFile", "Type Error");
                if (!vm.ImageFile.IsValidSize(200))
                    ModelState.AddModelError("ImageFile", "Size Error");
            }

            if (!ModelState.IsValid) return View(vm);

            string fileName = await vm.ImageFile.ManageFileSave(Path.Combine(_env.WebRootPath, "imgs", "employees"));

            await _context.Employees.AddAsync(new Employees
            {
                FullName = vm.FullName,
                InstagramUrl = vm.InstagramUrl,
                FacebookUrl = vm.FacebookUrl,
                LinkedInUrl = vm.LinkedInUrl,
                TwitterUrl = vm.TwitterUrl,
                Description = vm.Description,
                ImageUrl = Path.Combine("imgs", "employees", fileName),
                JobId = vm.JobId,
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
           
            if (id == null || id < 0) return BadRequest();

            var data = _context.Employees.FirstOrDefault(s => s.Id == id);

            if (data == null) return NotFound();

            UpdateEmployeeVM vM = new UpdateEmployeeVM
            {
                FacebookUrl = data.FacebookUrl,
                LinkedInUrl = data.LinkedInUrl,
                TwitterUrl = data.TwitterUrl,
                Description = data.Description,
                FullName = data.FullName,
                InstagramUrl = data.InstagramUrl,
                JobId = data.JobId,
            };

            ViewBag.Jobs = await _context.Jobs.ToListAsync();

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateEmployeeVM vm)
        {
            if (id == null || id < 0) return BadRequest();

            var data = _context.Employees.FirstOrDefault(s => s.Id == id);

            if (data == null) return NotFound();

            if (!ModelState.IsValid) return View(vm);

            if (vm.ImageFile != null)
            {
                if (vm.ImageFile.IsValidType("image"))
                    ModelState.AddModelError("ImageFile", "Type Error");
                if (vm.ImageFile.IsValidSize(200))
                    ModelState.AddModelError("ImageFile", "Size Error");
            }

            if (!ModelState.IsValid) return View(vm);

            string fileName = await vm.ImageFile.ManageFileSave(Path.Combine(_env.WebRootPath, "imgs", "employees"));

            data.FullName = vm.FullName;
            data.InstagramUrl = vm.InstagramUrl;
            data.Description = vm.Description;
            data.FacebookUrl = vm.FacebookUrl;
            data.LinkedInUrl = vm.LinkedInUrl;
            data.InstagramUrl = vm.InstagramUrl;
            data.ImageUrl = Path.Combine("imgs", "employees", fileName);
            data.JobId = vm.JobId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 0) return BadRequest();

            var data = _context.Employees.FirstOrDefault(s => s.Id == id);

            if (data == null) return NotFound();

            await data.ImageUrl.Delete(Path.Combine(_env.WebRootPath));

            _context.Employees.Remove(data);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
