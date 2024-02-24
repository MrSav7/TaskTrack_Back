using System.ComponentModel.DataAnnotations;

namespace KyrsachAPI.Models
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
    }
}
