using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaxBackApp.Models
{
    public class UserList
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [DisplayName("Username")]
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50)]
        public string UserName { get; set; }
        public List<TaskEntry> TaskEntries { get; set; }

    }
}
