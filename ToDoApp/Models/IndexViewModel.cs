using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;

namespace ToDoApp.Models
{
    public class IndexViewModel
    {
        public IEnumerable<ToDo> todo { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
