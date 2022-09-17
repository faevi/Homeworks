using FirstWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApplication.Controllers
{
    public class HomeController : Controller
    {
        DataContext db;
        public HomeController(DataContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.DataSet.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DataModel importData)
        {
            db.DataSet.Add(importData);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return View(await db.DataSet.ToListAsync());
        }

        [HttpGet]
        public IActionResult Get()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Get(int? id)
        {
            if (id != null)
            {
                DataModel? outputData = await db.DataSet.FirstOrDefaultAsync(p => p.Id == id);
                if (outputData != null)
                {
                    ViewBag.Data = outputData;
                    return View("SingleDataView");
                }                
            }
            return NotFound();
        }

        public async Task<IActionResult> BackToIndex()
        {
            return RedirectToAction("Index");
        }
    }
}