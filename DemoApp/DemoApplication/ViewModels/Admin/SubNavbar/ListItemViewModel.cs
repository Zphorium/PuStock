namespace DemoApplication.ViewModels.Admin.SubNavbar
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DirectedUrl { get; set; }
        public int Order { get; set; }
        public string Navbar { get; set; }

        public ListItemViewModel(int id,string name, string directedUrl, int order, string navbar)
        {
            Id=id;
            Name = name;
            DirectedUrl = directedUrl;
            Order = order;
            Navbar = navbar;
        }
        
    }
}
