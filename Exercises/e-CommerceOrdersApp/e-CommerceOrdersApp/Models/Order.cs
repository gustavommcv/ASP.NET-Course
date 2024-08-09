using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using e_CommerceOrdersApp.Validators;

namespace e_CommerceOrdersApp.Models
{
    public class Order
    {
        [BindNever]
        public int? OrderNo { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [DataType(DataType.DateTime)]
        public DateTime? OrderDate { get; set; }

        [Required]
        public List<Product> Products { get; set; } = new List<Product>();

        [Required(ErrorMessage = "{0} can't be blank")]
        [PriceValidator]
        public double? InvoicePrice { get; set; }

        public void GenerateOrderNo()
        {
            if (!OrderNo.HasValue)
                OrderNo = new Random().Next(1, 99999);
        }
    }
}
