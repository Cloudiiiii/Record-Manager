using Vendors.Services;
using Vendors.Entities;
using Microsoft.EntityFrameworkCore;
using YHAssignment3.Models;
using YHAssignment3.DataAccess;

namespace YHAssignment3.Services
{
    public class VendorManager : IVendorManager
    {
        /// <summary>
        /// Groups array containing alphabetically grouped vendors
        /// </summary>
        public static string[] Groups = new string[] { "A-E", "F-K", "L-R", "S-Z" };

        public VendorManager(VendorDbContext vendorDbContext)
        {
            _vendorDbContext = vendorDbContext;
        }

        /// <summary>
        /// Return the grouped vendors
        /// </summary>
        /// <returns></returns>
        public string[] GetAlphabeticalGroups()
        {
            return Groups;
        }

        /// <summary>
        /// Get vendor by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vendor? GetVendor(int id)
        {
            return GetBaseQuery()
                .Where(v => v.VendorId == id)
                .FirstOrDefault();
        }

        /// <summary>
        /// New vendor is added
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public int AddNewVendor(Vendor vendor)
        {
            _vendorDbContext.Vendors.Add(vendor);
            _vendorDbContext.SaveChanges();
            return vendor.VendorId;
        }

        /// <summary>
        /// Vendor is updated after edit
        /// </summary>
        /// <param name="vendor"></param>
        public void UpdateVendor(Vendor vendor)
        {
            _vendorDbContext.Vendors.Update(vendor);
            _vendorDbContext.SaveChanges();
        }

        /// <summary>
        /// Vendor is deleted
        /// </summary>
        /// <param name="id"></param>
        public void DeleteVendor(int id)
        {
            var vendor = _vendorDbContext.Vendors.Find(id);
            _vendorDbContext.Vendors.Remove(vendor);
            _vendorDbContext.SaveChanges();
        }

        /// <summary>
        /// Returns and sorts invoices
        /// </summary>
        /// <returns></returns>
        public List<Invoice> GetInvoices()
        {
            return _vendorDbContext.Invoices.OrderBy(i => i.InvoiceId).ToList();
        }

        /// <summary>
        /// New invoice is added
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int AddNewInvoice(Invoice invoice)
        {
            _vendorDbContext.Invoices.Add(invoice);
            _vendorDbContext.SaveChanges();
            return invoice.InvoiceId;
        }

        /// <summary>
        /// Return and sorts Payment Terms
        /// </summary>
        /// <returns></returns>
        public List<PaymentTerms> GetPaymentTerms()
        {
            return _vendorDbContext.PaymentTerms.OrderBy(p => p.PaymentTermsId).ToList();
        }

        /// <summary>
        /// Returns and sorts Line Items
        /// </summary>
        /// <returns></returns>
        public List<InvoiceLineItem> GetInvoiceLineItems()
        {
            return _vendorDbContext.InvoiceLineItems.OrderBy(l => l.InvoiceLineItemId).ToList();
        }

        /// <summary>
        /// Invoice is returned by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Invoice GetInvoice(int id)
        {
            return _vendorDbContext.Invoices
                .Where(i => i.InvoiceId == id)
                .FirstOrDefault();
        }

        /// <summary>
        /// DB Relationships
        /// </summary>
        /// <returns></returns>
        private IQueryable<Vendor> GetBaseQuery()
        {
            return _vendorDbContext.Vendors
                .Include(v => v.Invoices)
                .Include(v => v.Invoices).ThenInclude(i => i.PaymentTerms)
                .Include(v => v.Invoices).ThenInclude(c => c.InvoiceLineItems);
        }

        private VendorDbContext _vendorDbContext;
    }
}
