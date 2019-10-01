using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaxBackApp.Models
{
    public class TaskStatus
    {
        public int TaskStatusId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [DisplayName("Status")]
        public string TaskStatusDescription { get; set; }
        public ICollection<TaskEntry> TaskEntries { get; set; }
    }
}
