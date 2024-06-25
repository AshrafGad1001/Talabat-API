using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class CartItemDTO
    {
        /// <summary>
        /// im dotNET 8 String by Default Is Required
        /// </summary>

        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,int.MaxValue, ErrorMessage="one Item at least")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="Ba7bk")]
        public decimal price { get; set; }
        [Required]
        public string pictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
