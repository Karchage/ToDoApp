using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoApp.Entity;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly EFDBContext context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, EFDBContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.ToDo.ToListAsync());
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToDo todo)
        {
            context.ToDo.Add(todo);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id != null)
            {
                ToDo Todo = await context.ToDo.FirstOrDefaultAsync(p => p.Id == id);
                if(Todo != null)
                {
                    return View(Todo);
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id != null)
            {
                ToDo todo = await context.ToDo.FirstOrDefaultAsync(t => t.Id == id);
                if(todo != null)
                {
                    return View(todo);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit (ToDo todo)
        {
            context.ToDo.Update(todo);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if(id != null)
            {
                ToDo todo = new ToDo { Id = id.Value };
                context.Entry(todo).State = EntityState.Deleted;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
