using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendors.Entities
{
    public class PaymentTerms
    {
        public int PaymentTermsId { get; set; }

        public string Description { get; set; } = null!;

        public int DueDays { get; set; }

        public ICollection<Invoice>? Invoices { get; set; }
    }
}
