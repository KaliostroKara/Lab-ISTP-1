using Lab_ISTP_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab_ISTP_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagramController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiagramController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("product-categories")]
        public IActionResult GetProductCategoriesChartData()
        {
            var categoriesCount = _context.Products
                .Include(p => p.Categories)
                .GroupBy(p => p.Categories.Name)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToList();

            return Ok(categoriesCount);
        }
    }
}
