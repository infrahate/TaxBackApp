using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxBackApp.Models
{
    public class TaxBackContext : DbContext
    {
        public TaxBackContext(DbContextOptions<TaxBackContext> options):base(options)
        {

        }

        public DbSet<TaskEntry> TaskEntries { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }
        public DbSet<UserList> Users { get; set; }
        public DbSet<TaskCommentType> TaskCommentTypes { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }

    }
}
