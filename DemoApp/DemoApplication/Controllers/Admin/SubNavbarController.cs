using System.Linq.Expressions;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.ViewModels.Admin.SubNavbar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Controllers.Admin
{
        [Route("admin/subnavbar")]
        public class SubNavbarController : Controller
        {
            private readonly DataContext _dataContext;


            public SubNavbarController(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

           #region List

            [HttpGet("list", Name = "admin-subnavbar-list")]
            public async Task<IActionResult> ListAsyc()
            {
            var model = await _dataContext.SubNavbar
                .Select
                (sn => new ListItemViewModel
                (sn.Id, sn.Name, sn.DirectedUrl, sn.Order, sn.Navbar.Name))
                .ToListAsync();

                return View("~/Views/Admin/SubNavbar/ListAsync.cshtml", model);
            }
        #endregion

         #region Add
        [HttpGet("add", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync()
            };
            return View("~/Views/Admin/SubNavbar/AddAsync.cshtml",model);
        }
        [HttpPost("add", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
           if(!ModelState.IsValid)
            {
                return View("~/Views/Admin/SubNavbar/AddAsync.cshtml", model);
            }
            
            var subNavbar = new SubNavbar
            {
                Name=model.Name,
                DirectedUrl =model.DirectedUrl,
                NavbarId =model.NavbarId,
                Order = model.Order,
            };
            await _dataContext.SubNavbar.AddAsync(subNavbar);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-subnavbar-list");
        }

        #endregion
         #region Update

        [HttpGet("subupdate/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var subnavbar = await _dataContext.SubNavbar.Include(sn => sn.Navbar).FirstOrDefaultAsync(sn => sn.Id == id);

            if (subnavbar == null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel
            {
                Id = subnavbar.Id,
                Name = subnavbar.Name,
                DirectedUrl=subnavbar.DirectedUrl,
                Order = subnavbar.Order,
                NavbarId = subnavbar.NavbarId,
                Navbar = _dataContext.Navbars.Select(n=> new NavbarListItemViewModel(n.Id,n.Name)).ToList()

            };
            return View("~/Views/Admin/SubNavbar/UpdateAsync.cshtml", model);

        }

        [HttpPost("subupdate/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var subNav = await _dataContext.SubNavbar.Include(n=>n.Navbar).FirstOrDefaultAsync(sn => sn.Id == model.Id);

            if (subNav is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/SubNavbar/UpdateAsync.cshtml", model);
            }
            if (!_dataContext.Navbars.Any(a => a.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "There is some problem,Try again");
                return View("~/Views/Admin/SubNavbar/UpdateAsync.cshtml", model);
            };

            subNav.Name = model.Name;
            subNav.Order = model.Order;
            subNav.DirectedUrl = model.DirectedUrl;
            subNav.NavbarId = model.NavbarId;


            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-subnavbar-list");

        }



        #endregion
 
         #region Delete
        [HttpPost("delete/{id}", Name = "admin-subnavbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var subnavbar = await _dataContext.SubNavbar.Include(s=>s.Navbar).FirstOrDefaultAsync(n => n.Id == id);
            if (subnavbar is null)
            {
                return NotFound();
            }

            _dataContext.SubNavbar.Remove(subnavbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }
   
        #endregion






    }

}
