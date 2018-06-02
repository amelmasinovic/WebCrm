using System;
using System.ComponentModel.DataAnnotations;

namespace WebCrm.Models.Model
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public ApplicationUser CreateUser { get; set; }
    }
}