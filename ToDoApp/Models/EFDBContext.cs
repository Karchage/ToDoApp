using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entity;

namespace ToDoApp.Models
{
    public class EFDBContext : DbContext
    {
        public DbSet<ToDo> ToDo { get; set; }
        public EFDBContext(DbContextOptions<EFDBContext> options) :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
