using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCrm.Models.Model
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Forename { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ApplicationUser CreateUser { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}