using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.ViewCompanents
{
    [ViewComponent(Name = "NavbarFooter")]
    public class NavbarFooterViewCompanent : ViewComponent
    {
        private readonly DataContext _dataContext;

        public NavbarFooterViewCompanent(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
                _dataContext.Navbars.Include
                (n => n.SubNavbars.OrderBy
                (sn => sn.Order)).Where(n => n.IsShowenFooter == true).ToList();
            return View("~/Views/Shared/Components/NavbarFooter.cshtml", model);
        }
    }
}
