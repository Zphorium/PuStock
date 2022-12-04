namespace DemoApplication.ViewModels.Admin.Navbar
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public bool IsShowenHeader { get; set; }
        public bool IsShowenFooter { get; set; }
        public ListItemViewModel(int id,string name, int order, bool isBold, bool isShowenHeader, bool isShowenFooter)
        {
            Id = id;
            Name = name;
            Order = order;
            IsBold = isBold;
            IsShowenHeader = isShowenHeader;
            IsShowenFooter = isShowenFooter;
        }

    }
}
