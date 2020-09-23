using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;

namespace ToDoApp.Models
{
    public class ToDoListViewModel
    {
        public IEnumerable<ToDo> Todos { get; set; }
        public SelectList DateCreates { get; set; }
        public string Context { get; set; }
        public IndexViewModel IndexViewModel { get; set; }
    }
}
