using Microsoft.AspNetCore.Mvc;

namespace BEExam10.Areas.Admin.ViewComponents
{
    public class NavAdminViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
