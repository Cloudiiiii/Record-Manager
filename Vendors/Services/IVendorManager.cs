using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendors.Entities;

namespace Vendors.Services
{
    public interface IVendorManager
    {
        /// <summary>
        /// Gets the vendor by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vendor? GetVendor(int id);
        /// <summary>
        /// Adds vendow
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public int AddNewVendor(Vendor vendor);
        /// <summary>
        /// Updates the vendor when edited
        /// </summary>
        /// <param name="vendor"></param>
        public void UpdateVendor(Vendor vendor);
        /// <summary>
        /// Deltes vendor by ID
        /// </summary>
        /// <param name="id"></param>
        public void DeleteVendor(int id);
        /// <summary>
        /// Return List of invoices
        /// </summary>
        /// <returns></returns>
        public List<Invoice> GetInvoices();
        /// <summary>
        /// Return invoice payment terms
        /// </summary>
        /// <returns></returns>
        public List<PaymentTerms> GetPaymentTerms();
        /// <summary>
        /// Return Invoice Line Items
        /// </summary>
        /// <returns></returns>
        public List<InvoiceLineItem> GetInvoiceLineItems();
        /// <summary>
        /// Array for sorted vendors
        /// </summary>
        /// <returns></returns>
        public string[] GetAlphabeticalGroups();
        /// <summary>
        /// Gets invoice by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Invoice GetInvoice(int id);
        /// <summary>
        /// Adds a new invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int AddNewInvoice(Invoice invoice);
    }
}