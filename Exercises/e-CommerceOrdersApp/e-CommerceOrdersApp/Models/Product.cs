using System.ComponentModel.DataAnnotations;

namespace e_CommerceOrdersApp.Models
{
    public class Product
    {
        [Required(ErrorMessage = "{0} can't be blank")]
        public int? Code { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        public int? Quantity { get; set; }

        public override string ToString()
        {
            return $"{Code}, {Price}, {Quantity}";
        }
    }
}
