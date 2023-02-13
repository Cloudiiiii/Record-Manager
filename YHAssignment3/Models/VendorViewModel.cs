using Vendors.Entities;

namespace YHAssignment3.Models
{
    public class VendorViewModel
    {
        public Vendor? ActiveVendor { get; set; }
        public List<Invoice>? Invoices { get; set; }
        public Invoice? newInvoice { get; set; }
        public string? lastLowerBound { get; set; }
        public string? lastUpperBound { get; set; }
    }
}
