using System.ComponentModel.DataAnnotations;

namespace ITI.ElectroDev.Presentation
{
    public class BrandModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter The Title")]
        [MaxLength(500, ErrorMessage = "Must be less than 500 Chars")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Must Enter images")]
        //public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Must Choose Category")]
        [Display(Name="Category")]
        public int CategoryId { get; set; }
    }
}
