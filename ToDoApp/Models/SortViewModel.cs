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
        public bool Up { get; set; } //Значение по возр или убыв

        public SortViewModel(SortStateToDo sortOrder)
        {
            ContextSort = SortStateToDo.ContextAsc;
            DateCreateSort = SortStateToDo.DateCreateAsc;
            Up = true;

            if(sortOrder == SortStateToDo.ContextDesc || sortOrder == SortStateToDo.DateCreateDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortStateToDo.ContextDesc:
                    Current = ContextSort = SortStateToDo.ContextAsc;
                    break;
                case SortStateToDo.DateCreateAsc:
                    Current = DateCreateSort = SortStateToDo.DateCreateDesc;
                    break;
                case SortStateToDo.DateCreateDesc:
                    Current = DateCreateSort = SortStateToDo.DateCreateAsc;
                    break;
                default:
                    Current = ContextSort = SortStateToDo.ContextDesc;
                    break;
            }
        }

    }
}
