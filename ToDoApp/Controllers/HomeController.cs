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

        public async Task<IActionResult> Index(string datecreates, string context, SortStateToDo sortOrder = SortStateToDo.ContextAsc, int page = 1)
        {
            int pageSize = 3;

            IQueryable<ToDo> todo = db.ToDo;


            var count = await todo.CountAsync();
            IQueryable<ToDo> items = todo.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            items = sortOrder switch
            {
                SortStateToDo.ContextDesc => items.OrderByDescending(s => s.Context),
                SortStateToDo.DateCreateAsc => items.OrderBy(s => s.DateCreate),
                SortStateToDo.DateCreateDesc => items.OrderByDescending(s => s.DateCreate),
                _ => items.OrderBy(s => s.Context),

            };
            IndexViewModel indexviewModel = new IndexViewModel
            {
                todo = items,
                SortViewModel = new SortViewModel(sortOrder),
                PageViewModel = pageViewModel
            };
            DateTime now = DateTime.Now;
            if (datecreates != null && Convert.ToDateTime(datecreates) != new DateTime(0).Date)
            {
                items = items.Where(p => p.DateCreate.Date == Convert.ToDateTime(datecreates).Date);
            }
            if (!String.IsNullOrEmpty(context))
            {
                items = items.Where(p => p.Context.Contains(context));
            }
            List<string> dateCreate = db.ToDo.Select(p => p.DateCreate.Date.ToString("dd/MM/yyyy")).Distinct().ToList();
            dateCreate.Insert(0, new DateTime(0).Date.ToString("dd/MM/yyyy"));
            ToDoListViewModel viewModel = new ToDoListViewModel
            {
                Todos = items.ToList(),
                DateCreates = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dateCreate),
                Context = context,
                IndexViewModel = indexviewModel,
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
