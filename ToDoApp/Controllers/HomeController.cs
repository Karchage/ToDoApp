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
        private readonly EFDBContext db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, EFDBContext context)
        {
            _logger = logger;
            this.db = context;
        }

        public async Task<IActionResult> Index(string datecreate, string context, SortStateToDo sortOrder = SortStateToDo.ContextAsc, int page = 1)
        {
            int pageSize = 3;

            IQueryable<ToDo> todo = db.ToDo;
            //filtr
            if (datecreate != null && Convert.ToDateTime(datecreate) != new DateTime(0).Date)
            {
                todo = todo.Where(p => p.DateCreate.Date == Convert.ToDateTime(datecreate).Date);
            }
            if (!String.IsNullOrEmpty(context))
            {
                todo = todo.Where(p => p.Context.Contains(context));
            }

            //sort
            todo = sortOrder switch
            {
                SortStateToDo.ContextDesc => todo.OrderByDescending(s => s.Context),
                SortStateToDo.DateCreateAsc => todo.OrderBy(s => s.DateCreate),
                SortStateToDo.DateCreateDesc => todo.OrderByDescending(s => s.DateCreate),
                _ => todo.OrderBy(s => s.Context),

            };

            //page
            var count = await todo.CountAsync();
            IQueryable<ToDo> items = todo.Skip((page - 1) * pageSize).Take(pageSize);
            var dateForFilter = db.ToDo.Select(p => p.DateCreate.Date.ToString("dd/MM/yyyy")).Distinct().ToList();
            
            //create view
            IndexViewModel indexViewModel = new IndexViewModel
            {
                todo = items,
                PageViewModel = new PageViewModel(count, page,pageSize),
                FilterViewModel = new FilterViewModel(dateForFilter, datecreate, context),
                SortViewModel = new SortViewModel(sortOrder)

            };
            return View(indexViewModel);
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToDo todo)
        {
            todo.DateDue = todo.GetDateDue(todo.DateDue, todo.DateFor);
            db.ToDo.Add(todo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id != null)
            {
                ToDo Todo = await db.ToDo.FirstOrDefaultAsync(p => p.Id == id);
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
                ToDo todo = await db.ToDo.FirstOrDefaultAsync(t => t.Id == id);
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
            db.ToDo.Update(todo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if(id != null)
            {
                ToDo todo = new ToDo { Id = id.Value };
                db.Entry(todo).State = EntityState.Deleted;
                await db.SaveChangesAsync();
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
