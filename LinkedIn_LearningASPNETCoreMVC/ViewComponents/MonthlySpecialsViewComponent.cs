using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExploreCalifornia.ViewComponents
{

    public class MonthlySpecialsViewComponent : ViewComponent
    {
        private readonly BlogDataContext _context;

        public MonthlySpecialsViewComponent(BlogDataContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var specials = _context.MonthlySpecials.ToArray();
            return View(specials);
        }
    }
}
