using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaxBackApp.Models
{
    public class TaskType
    {
        public int TaskTypeId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string TaskTypeDescription { get; set; }
        public ICollection<TaskEntry> TaskEntries { get; set; }
    }
}
