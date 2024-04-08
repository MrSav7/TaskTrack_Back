using System.ComponentModel.DataAnnotations;

namespace KyrsachAPI.Models
{
    public class TaskCustomer
    {
        [Key]
        public int TaskCustomerId { get; set; }
        public string TaskCustomerFirstName { get; set; } = null!;
        public string TaskCustomerLastName { get; set; } = null!;
        public string TaskCustomerMiddleName { get; set; }
        public string TaskCustomerPhone { get; set; } = null!;
        public int TaskId { get; set;}
    }
}
