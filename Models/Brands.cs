using System.ComponentModel.DataAnnotations;

namespace KyrsachAPI.Models
{
    public class Brands
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
