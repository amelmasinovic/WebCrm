using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCrm.Models.Model
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public ApplicationUser CreateUser { get; set; }
    }
}