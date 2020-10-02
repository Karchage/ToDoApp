using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Entity
{
    public class ToDo
    {
        public int Id { get; set; }
        public string LoginUser { get; set; }
        public string Context { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateFor { get; set; }
        public bool Done { get; set; }


        public DateTime GetDateDue(DateTime DateDue, DateTime DateFor)
        {
            DateDue = DateFor.Add(DateDue.TimeOfDay);
            return DateDue;
        }
    }
}
