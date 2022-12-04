using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.ViewCompanents
{
    [ViewComponent(Name ="NavbarHeader")]
    public class NavbarHeaderViewCompanent : ViewComponent
    {
        private readonly DataContext _dataContext;

        public NavbarHeaderViewCompanent(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
                _dataContext.Navbars.Include
                (n => n.SubNavbars.OrderBy
                (sn => sn.Order)).Where(n => n.IsShowenHeader == true).OrderBy(n => n.Order).ToList();
            return View("~/Views/Shared/Components/NavbarHeader.cshtml",model);
        }
    }
}
