using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoApp.Entity;
using ToDoApp.Enums;
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

        public async Task<IActionResult> Index(SortStateToDo sortOrder = SortStateToDo.ContextAsc)
        {
            IQueryable<ToDo> todo = context.ToDo;

            todo = sortOrder switch
            {
                SortStateToDo.ContextDesc => todo.OrderByDescending(s => s.Context),
                SortStateToDo.DateCreateAsc => todo.OrderBy(s => s.DateCreate),
                SortStateToDo.DateCreateDesc => todo.OrderByDescending(s => s.DateCreate),
                _ => todo.OrderBy(s => s.Context),

            };
            IndexViewModel viewModel = new IndexViewModel
            {
                todo = await todo.AsNoTracking().ToListAsync(),
                SortViewModel = new SortViewModel(sortOrder),
            };

            return View(viewModel);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToDo todo)
        {
            todo.DateDue = todo.GetDateDue(todo.DateDue, todo.DateFor);
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
