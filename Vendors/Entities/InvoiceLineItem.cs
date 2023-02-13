using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendors.Entities
{
    public class InvoiceLineItem
    {
        public int InvoiceLineItemId { get; set; }

        public double? Amount { get; set; }

        public string? Description { get; set; }

        public int? InvoiceId { get; set; }

        public Invoice? Invoice { get; set; }
    }
}
