using ITI.ElectroDev.Models;
using System.ComponentModel.DataAnnotations;

namespace ITI.ElectroDev.Presentation
{
    public class ProductCreateModel
    {

        

        public int id { set; get;  }

        [StringLength(250)]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [MaxLength(255, ErrorMessage = "لا يزيد عن 255 حرف")]
        [MinLength(6, ErrorMessage = "لا يقل عن 4 حروف")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [MaxLength(500, ErrorMessage = "لا يزيد عن 255 حرف")]
        [ MinLength(10, ErrorMessage = "لا يقل عن 4 حروف")]
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Display(Name="Discount on this Product")]
        public int Discount { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب *")]
        [Display(Name="Product Price")]
        public int Price { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب *")]
        [Display(Name = "Product Quantity")]
        public int Quantity { get; set; }

        [Required, Display(Name = "Product Images")]
        public List<IFormFile> Images { get; set; }

        [Display(Name = "Product Brand")]
        [Required]
        public int BrandId { get; set; }


    }

}

