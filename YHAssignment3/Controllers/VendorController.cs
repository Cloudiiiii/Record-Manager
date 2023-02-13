using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendors.Entities;
using Vendors.Services;
using YHAssignment3.DataAccess;
using YHAssignment3.Models;

namespace YHAssignment3.Controllers
{
    public class VendorController : Controller
    {
        public VendorController(IVendorManager vendorManager, VendorDbContext vendorDbContext)
        {
            _vendorDbContext = vendorDbContext;
            _vendorManager = vendorManager;
        }

        // Vendors are grouped alphabetically
        [HttpGet("/Vendor/groups/{lowerBound}-{upperBound}")]
        public IActionResult GetVendors(string lowerBound, string upperBound)
        {
            var vendors = _vendorDbContext.Vendors
                .Where(v => v.IsDeleted == false
                && v.Name.ToLower().Substring(0, 1).CompareTo(lowerBound) >= 0
                    && v.Name.ToLower().Substring(0, 1).CompareTo(upperBound) <= 0)
                .OrderBy(v => v.Name)
                .ToList();

            AlphabeticalVendorGroupsModel vendorsByGroupViewModel = new AlphabeticalVendorGroupsModel()
            {
                Vendors = vendors,
                ActiveGroupName = lowerBound + "-" + upperBound,
                Groups = _vendorManager.GetAlphabeticalGroups()
            };

            return View("VendorsByGroup", vendorsByGroupViewModel);
        }

        // Request to add a new vendor
        [HttpGet("/Vendors/add-request/{lowerBound}-{upperBound}")]
        public IActionResult GetAddVendor(string lowerBound, string upperBound)
        {
            VendorViewModel vendorViewModel = new VendorViewModel()
            {
                Invoices = _vendorManager.GetInvoices(),
                newInvoice = new Invoice(),
                ActiveVendor = new Vendor(),
                lastLowerBound = lowerBound,
                lastUpperBound = upperBound
            };

            return View("AddVendor", vendorViewModel);
        }

        // New vendor is added
        [HttpPost("/Vendors")]
        public IActionResult AddNewVendor(VendorViewModel vendorViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error with form";
                vendorViewModel.Invoices = _vendorManager.GetInvoices();
                return View("AddVendor", vendorViewModel);
            }
            else
            {
                ViewBag.ErrorMessage = string.Empty;
                _vendorManager.AddNewVendor(vendorViewModel.ActiveVendor);
                return RedirectToAction("groups", new RouteValueDictionary(
                    new { action = GetVendors("A", "E"), id = "A-E" }));
            }
        }

        // Edit request is created
        [HttpGet("/Vendor/{id}/edit-request")]
        public IActionResult GetEditRequest(int id)
        {
            VendorViewModel vendorViewModel = new VendorViewModel()
            {
                Invoices = _vendorManager.GetInvoices(),
                ActiveVendor = _vendorManager.GetVendor(id)
            };

            return View("EditVendor", vendorViewModel);
        }

        // Edit reqiest gets processed
        [HttpPost("/Vendor/edit-requests")]
        public IActionResult ProcessEditRequest(VendorViewModel vendorViewModel)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error with form";
                vendorViewModel.Invoices = _vendorManager.GetInvoices();
                return View("EditVendor", vendorViewModel);
            }
            else
            {
                ViewBag.ErrorMessage = string.Empty;
                _vendorManager.UpdateVendor(vendorViewModel.ActiveVendor);

                return RedirectToAction("groups", new RouteValueDictionary(
                    new { action = GetVendors("A", "E"), id = "A-E" }));
            }
        }

        // Temporarily deletes vendor
        [HttpGet("/Vendor")]
        public IActionResult ProcessTempDelete(int id)
        {
            var vendor = _vendorManager.GetVendor(id);
            vendor.IsDeleted = true;
            TempData["LastActionMessage"] = $"The vendor \"{vendor.Name}\" was deleted. ";

            _vendorDbContext.SaveChanges();

            return RedirectToAction("groups", new RouteValueDictionary(
                new { action = GetVendors("A", "E"), id = "A-E" }));
        }

        // Undo the temp delete
        public IActionResult UndoDelete()
        {
            foreach (Vendor vendor in _vendorDbContext.Vendors)
            {
                if (!_vendorDbContext.Vendors.Find(vendor.VendorId).IsDeleted)
                {
                    if (vendor.IsDeleted)
                    {
                        _vendorDbContext.Vendors.Remove(vendor);
                    }
                }
                else
                {
                    vendor.IsDeleted = false;
                    break;
                }
            }

            _vendorDbContext.SaveChanges();

            return RedirectToAction("groups", new RouteValueDictionary(
             new { action = GetVendors("A", "E"), id = "A-E" }));
        }

        // Gets the invoices by it's ID
        [HttpGet("/Vendor/{vendorId}/invoice/{invoiceId}")]
        public IActionResult GetInvoices(int vendorId, int invoiceId)
        {
            InvoiceViewModel manageInvoiceViewModel = new InvoiceViewModel()
            {
                Invoices = _vendorManager.GetVendor(vendorId).Invoices.ToList(),
                NewInvoice = new Invoice(),
                ActiveVendor = _vendorManager.GetVendor(vendorId),
                PaymentTerms = _vendorManager.GetPaymentTerms(),
                Groups = _vendorManager.GetAlphabeticalGroups(),
                ActiveInvoice = _vendorManager.GetInvoice(invoiceId),
                InvoiceLineItems = _vendorManager.GetInvoiceLineItems(),
            };

            return View("ManageInvoice", manageInvoiceViewModel);
        }

        // New invoice is added
        [HttpPost("/Vendor/{vendorId}/invoice/{invoiceId}")]
        public IActionResult AddNewInvoice(int vendorId, int invoiceId, InvoiceViewModel manageInvoiceViewModel)
        {
            var vendor = _vendorManager.GetVendor(vendorId);

            vendor.Invoices.Add(manageInvoiceViewModel.NewInvoice);
            _vendorDbContext.SaveChanges();

            return RedirectToAction("GetInvoices", "Invoice", new { vendorId = vendorId, invoiceId = invoiceId });
        }

        private VendorDbContext _vendorDbContext;
        private IVendorManager _vendorManager;
    }
}
