using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ITI.ElectroDev.Presentation
{
    public class CategoryCreateModel
    {
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "This Field is Required")]
        [MaxLength(200, ErrorMessage = "لا يزيد عن 200")]
        [MinLength(4, ErrorMessage = "Must Be More Than Or Equals 4 Chars.")]
        public string Name { get; set; }
    }
}
