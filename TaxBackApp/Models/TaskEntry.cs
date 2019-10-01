using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaxBackApp.Models
{
    public class TaskEntry
    {
        public int TaskEntryId { get; set; }
        [Required]
        [DisplayName("Task Created On")]
        public DateTimeOffset DateCreated { get; set; }
        [Required]
        [DisplayName("Due By")]
        public DateTimeOffset DateDue { get; set; }
        [Required]
        [DisplayName("Task Description")]
        [Column(TypeName = "nvarchar(250)")]
        [StringLength(250)]
        public string TaskEntryDescription { get; set; }

        [DisplayName("Task Status")]
        public int TaskStatusId { get; set; }
        public TaskStatus TaskStatus { get; set; }

        [DisplayName("Task Type")]
        public int TaskTypeId { get; set; }
        public TaskType TaskType { get; set; }

        [DisplayName("Assigned to")]
        public int? UserId { get; set; }
        public UserList UserList { get; set; }

        [DisplayName("Next Action Required By")]
        public DateTimeOffset DateNextAction { get; set; }

        public List<TaskComment> TaskComments { get; set; }

    }
}
