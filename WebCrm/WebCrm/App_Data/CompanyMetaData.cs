using System.ComponentModel.DataAnnotations;
using WebCrm.Models;

namespace WebCrm.App_Data
{
    [MetadataType(typeof(CompanyMetaData))]
    public partial class Company
    {
        public ApplicationUser CreateUserObject { get; set; }
    }

    class CompanyMetaData
    {
        [Display(Name = "Naziv")]
        [Required(ErrorMessage = "Obavezan unos")]
        public string Name { get; set; }
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        [Display(Name = "Poštanski broj")]
        public string Zip { get; set; }
        [Display(Name = "Grad")]
        public string City { get; set; }
        [Display(Name = "Država")]
        public string Country { get; set; }
        [Display(Name = "Korisnik koje je napravio unos")]
        public string CreateUser { get; set; }
    }
}