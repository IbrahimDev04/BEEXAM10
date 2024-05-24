using BEExam10.DataAccessLayer;
using BEExam10.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEExam10.Controllers
{
    public class HomeController(LumiaContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {

            var data = await _context.Employees
                .Include(j=> j.Job)
                .Select(s => new GetEmployeeVM
                {
                    FullName = s.FullName,
                    Description = s.Description,
                    InstagramUrl = s.InstagramUrl,
                    FacebookUrl = s.FacebookUrl,
                    LinkedInUrl = s.LinkedInUrl,
                    ImageUrl = s.ImageUrl,
                    TwitterUrl = s.TwitterUrl,
                    Job = s.Job.Name.ToString(),
                }).ToListAsync();

            return View(data);
        }
    }
}
