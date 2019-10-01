using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaxBackApp.Models
{
    public class TaskCommentType
    {
        public int TaskCommentTypeId { get; set; }
        [DisplayName("Task Type Description")]
        [Column(TypeName = "nvarchar(250)")]
        public string TaskCommentTypeDescription { get; set; }

        public ICollection<TaskComment> TaskComments { get; set; }

    }
}
