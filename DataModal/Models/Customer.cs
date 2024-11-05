using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class Customer
    {

        public long? CustomerID { get; set; }
        [Required(ErrorMessage = "Name Can't be Blank")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone Can't be Blank")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email Can't be Blank")]
        public string Email { get; set; }

        public Address AddressDetails { get; set; }
        public int? Priority { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }


    }
}
