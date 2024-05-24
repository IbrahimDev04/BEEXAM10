using BEExam10.DataAccessLayer;
using BEExam10.ViewModels.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEExam10.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    public class JobController(LumiaContext _context) : Controller
    {
        public async Task<IActionResult> Index(int page = 0)
        {
            var PageCount = 2;
            double n = await _context.Jobs.CountAsync();
            ViewBag.MaxPage = Math.Ceiling((double)n/PageCount); 



            var data = await _context.Jobs
                .Skip(page*PageCount)
                .Take(PageCount)
                .Select(s => new GetJobAdminVM
                {
                    Name = s.Name,
                    Id = s.Id,
                    CreatedTime = s.CreatedTime.ToString("dd MMM yyyy"),
                    UpdatedTime = s.UpdatedTime.ToString("dd MMM yyyy")
                }).ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobVM vm)
        {
            if(!ModelState.IsValid) return View(vm);

            await _context.Jobs.AddAsync(new Models.Job
            {
                Name = vm.Name,
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 0) return BadRequest();

            var data = _context.Jobs.FirstOrDefault(s => s.Id == id);

            if (data == null) return NotFound();

            UpdateJobVM vM = new UpdateJobVM
            {
                Name = data.Name,
            };

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateJobVM vm)
        {
            if (id == null || id < 0) return BadRequest();

            var data = _context.Jobs.FirstOrDefault(s => s.Id == id);

            if (data == null) return NotFound();

            data.Name = vm.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 0) return BadRequest();

            var data = _context.Jobs.FirstOrDefault(s => s.Id == id);

            if (data == null) return NotFound();

            _context.Jobs.Remove(data);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        }
}
