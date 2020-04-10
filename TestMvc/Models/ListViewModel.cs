using System.ComponentModel.DataAnnotations;

namespace TestMvc.Models
{
    public class ListViewModel
    {
        [Display(Name = "Order Id")]
        public string OrderId { get; set; }

        [Display(Name = "Order Item")]
        public string OrderItem { get; set; }

        [Display(Name = "Price")]
        public int Price { get; set; }

        [Display(Name = "Cost")]
        public int Cost { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        public string ProductId { get; set; }

        public int StatusCode { get; set; }
    }
}