using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Enums;

namespace ToDoApp.Models
{
    public class SortViewModel
    {
        public SortStateToDo ContextSort { get; set; }
        public SortStateToDo DateCreateSort { get; set; }
        public SortStateToDo Current { get; set; } // Значение выбранного свойства

        public SortViewModel(SortStateToDo sortOrder)
        {
            ContextSort = sortOrder == SortStateToDo.ContextAsc ? SortStateToDo.ContextDesc : SortStateToDo.ContextAsc;
            DateCreateSort = sortOrder == SortStateToDo.DateCreateAsc ? SortStateToDo.DateCreateDesc : SortStateToDo.DateCreateAsc;
            Current = sortOrder;
        }

    }
}
