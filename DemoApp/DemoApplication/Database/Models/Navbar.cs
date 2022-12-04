using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Navbar:BaseEntity
    {
        public string Name { get; set; }
        public string DirectedUrl { get; set; }
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public bool IsShowenHeader { get; set; }
        public bool IsShowenFooter { get; set; }

        public List<SubNavbar> SubNavbars { get; set; }
    }
}
