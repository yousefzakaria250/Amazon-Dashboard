using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Xml.Linq;

namespace ITI.ElectroDev.Presentation
{
    public class EditUserViewModel
    {
        //public EditUserViewModel()
        //{

        //    Claims = new List<String>();
        //    Roles = new List<String>();

        //}
        public string Id { get; set; }

        [Required, MaxLength(200), MinLength(3), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, MaxLength(200), MinLength(3), Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, MaxLength(15), MinLength(3)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required, EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        //public IList<string> Roles { get; set; }
        //public IList<string> Claims { get; set; }

    }
}