using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class FilterViewModel
    {
        public SelectList DateCreates { get; set; }
        public string? SelectedDate { get; set; }
        public string SelectedContext { get; set; }
        public FilterViewModel(List<string> dateCreates, string? dateCreate, string context)
        {
            dateCreates.Insert(0, new DateTime(0).Date.ToString("dd/MM/yyyy"));
            DateCreates = new SelectList(dateCreates);
            //db.ToDo.Select(p => p.DateCreate.Date.ToString("dd/MM/yyyy")).Distinct().ToList();
            SelectedDate = dateCreate;
            SelectedContext = context;
        }
    }
}
