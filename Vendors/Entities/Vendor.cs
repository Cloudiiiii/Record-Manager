using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendors.Entities
{
    public class Vendor
    {
        // adding "Required" and regex was the only way I know how to do validation, fingers crossed you didnt want it done another way
        public int VendorId { get; set; }

        [Required(ErrorMessage = "The name is missing")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "The address is missing")]
        public string? Address1 { get; set; }

        public string? Address2 { get; set; }
        [Required(ErrorMessage = "Missing")]
        public string? City { get; set; } = null!;
        [Required(ErrorMessage = "City is Missing")]
        [RegularExpression("^[A-Za-z]{2}", ErrorMessage = "Incorrect format")]
        public string? ProvinceOrState { get; set; } = null!;
        [Required(ErrorMessage = "Postal code is missing")]
        [RegularExpression("^(([0-9]{5}-[0-9]{4})|([0-9]{5})|([a-zA-Z][0-9][a-zA-Z]-? ?[0-9][a-zA-Z][0-9]))$", ErrorMessage = "Postal code is in incorrect format")]

        public string? ZipOrPostalCode { get; set; } = null!;
        [Required(ErrorMessage = "Phone number is missing")]
        [RegularExpression("^([0-9]{3})[-]?([0-9]{3})[-]?([0-9]{4})$", ErrorMessage = "Phone number is in incorrect format")]
        public string? VendorPhone { get; set; }

        public string? VendorContactLastName { get; set; }

        public string? VendorContactFirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}", ErrorMessage = "Email is in incorrect format")]
        public string? VendorContactEmail { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Invoice>? Invoices { get; set; }
    }
}
