using Vendors.Entities;

namespace YHAssignment3.Models
{
    public class InvoiceViewModel
    {
        public List<InvoiceLineItem>? InvoiceLineItems { get; set; }
        public List<Invoice>? Invoices { get; set; }
        public List<PaymentTerms>? PaymentTerms { get; set; }
        public Vendor? ActiveVendor { get; set; }
        public InvoiceLineItem? NewInvoiceLineItem { get; set; }
        public Invoice? ActiveInvoice { get; set; }
        public Invoice? NewInvoice { get; set; }

        public ICollection<string>? Groups { get; set; }

        public double? CalculateTotal(InvoiceViewModel manageInvoiceViewModel)
        {
            foreach (var lineItem in manageInvoiceViewModel.ActiveInvoice.InvoiceLineItems)
            {
                manageInvoiceViewModel.ActiveInvoice.PaymentTotal += lineItem.Amount;
            }

            return manageInvoiceViewModel.ActiveInvoice.PaymentTotal;
        }
    }
}
