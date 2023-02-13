using Vendors.Entities;

namespace YHAssignment3.Models
{
    public class AlphabeticalVendorGroupsModel
    {
        public ICollection<string>? Groups { get; set; }
        public string? ActiveGroupName { get; set; }
        public List<Vendor>? Vendors { get; set; }
    }
}
