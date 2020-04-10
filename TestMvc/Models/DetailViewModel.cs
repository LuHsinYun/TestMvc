using System.ComponentModel.DataAnnotations;

namespace TestMvc.Models
{
    public class DetailViewModel
    {
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        public int Price { get; set; }

        [Display(Name = "Cost")]
        public int Cost { get; set; }
    }
}