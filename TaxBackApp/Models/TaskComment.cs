using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaxBackApp.Models
{
    public class TaskComment
    {
        public int TaskCommentId { get; set; }
        [Required]
        [DisplayName("Task Comment Description")]
        [Column(TypeName = "nvarchar(250)")]
        public string TaskCommentDescription { get; set; }

        [DisplayName("Task Type")]
        public int TaskCommentTypeId { get; set; }
        public TaskCommentType TaskCommentType { get; set; }

        [DisplayName("Task Comment Due By")]
        public DateTimeOffset ReminderDate { get; set; }


        [DisplayName("Assign To Task")]
        public int? TaskEntryId { get; set; }
        public TaskEntry TaskEntry { get; set; }

    }
}
