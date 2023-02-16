using ITI.ElectroDev.Models;
using System.ComponentModel.DataAnnotations;

namespace ITI.ElectroDev.Presentation
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="You must select customer")]
        [Display(Name ="Order For User: ")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "You must select product")]
        [Display(Name = "Product: ")]
        public int ProductId { get; set; }
        [Display(Name = "Total Price: ")]
        public double TotalPrice { get; set; }
        [Display(Name ="Type Of Order: ")]
        public string Type { get; set; }
        [Display(Name = "Status Of Order: ")]
        public string Status { get; set; }
        [Display(Name = "Date: ")]
        public DateTime CreatedAt { get; set; }
    }
}
