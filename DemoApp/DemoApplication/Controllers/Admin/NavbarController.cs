using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Migrations;
using DemoApplication.ViewModels.Admin.Book.Add;
using DemoApplication.ViewModels.Admin.Navbar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AddViewModel = DemoApplication.ViewModels.Admin.Navbar.AddViewModel;
using Navbar = DemoApplication.Database.Models.Navbar;

namespace DemoApplication.Controllers.Admin
{
    [Route("admin/navbar")]
    public class NavbarController : Controller
    {
        private readonly DataContext _dataContext;


        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("list", Name = "admin-navbar-list")]
        public async Task<IActionResult> ListAsyc()
        {
            var model = await _dataContext.Navbars
                .Select(n => new ListItemViewModel(n.Id,n.Name, n.Order, n.IsBold, n.IsShowenHeader, n.IsShowenFooter))
                .ToListAsync();

            return View("~/Views/Admin/Navbar/ListAsync.cshtml", model);
        }



        #region Add

        [HttpGet("add", Name = "admin-navbar-add")]
        public async Task<IActionResult> AddAsync()
        {

            return View("~/Views/Admin/Navbar/AddAsync.cshtml");
        }

        [HttpPost("add", Name = "admin-navbar-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Navbar/AddAsync.cshtml", model);
            }

            if (_dataContext.Navbars.Any(a => a.Order == model.Order))
            {
                ModelState.AddModelError(String.Empty, "Author is not found");
                return View("~/Views/Admin/Navbar/AddAsync.cshtml", model);
            }

            var navbar = new Navbar
            {
                Name = model.Name,
                DirectedUrl = model.DirectedUrl,

                Order = model.Order,
                IsBold = model.IsBold,
                IsShowenHeader = model.IsShowenHeader,
                IsShowenFooter = model.IsShowenFooter,
            };

            await _dataContext.Navbars.AddAsync(navbar);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-navbar-list");
        }



        #endregion


        #region Update

        [HttpGet("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.Include(n => n.SubNavbars).FirstOrDefaultAsync(n => n.Id == id);

            var model = new UpdateViewModel
            {
                 Id = id,
                 Name = navbar.Name,
                 DirectedUrl = navbar.DirectedUrl,
                 Order = navbar.Order,
                 IsBold = navbar.IsBold,
                 IsShowenHeader = navbar.IsShowenHeader,
                 IsShowenFooter = navbar.IsShowenFooter,
             };
            return View("~/Views/Admin/Navbar/UpdateAsync.cshtml",model);

        }

        [HttpPost("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var navData = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);

            if (navData is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Navbar/UpdateAsync.cshtml", model);
            }
        
            navData.Name = model.Name;
            navData.Order = model.Order;
            navData.IsBold = model.IsBold;
            navData.IsShowenHeader = model.IsShowenHeader;
            navData.IsShowenFooter = model.IsShowenFooter;

            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-navbar-list");

        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-navbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);
            if (navbar is null)
            {
                return NotFound();
            }

             _dataContext.Navbars.Remove(navbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");
        }

        #endregion
    }
}

