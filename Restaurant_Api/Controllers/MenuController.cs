using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Restaurant_Api.Models;
using Restaurant_Api.Services;

namespace Restaurant_Api.Controllers
{
    [Route("Menu/api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        //private readonly MenuServices _menuServices;

        public MenuController()
        {
            //_menuServices = MenuServices;
        }

        // GET: api/Menu
        [HttpGet]
        public IEnumerable<Menu> GetAllMenu()
        {
            return MenuServices.GetAllMenu();
        }

        // GET: api/Menu/5
        [HttpGet("{GetMenu}")]
        /*public ActionResult<Menu> Get(string id)
        {
            var menu = MenuServices.Get(id);
            if (menu == null)
                return NotFound();
            return menu;
        }*/
        public ActionResult<Menu> Get(string id)
        {
            return MenuServices.Get(id);
        }


        // POST: api/Menu
        [HttpPost]
        public void CreateMenu(Menu menu)
        {
            MenuServices.Add(menu);
        }

        // PUT: api/Menu/5
        [HttpPut("{UpdateMenu}")]
        /* public ActionResult<Menu> UpdateMenu(string id, Menu newMenu)
         {
             var menu = MenuServices.Get(id);
             if (menu == null)
                 return NotFound();
             MenuServices.UpdateMenu(id, newMenu);
             return newMenu;
         }*/
        public ActionResult<Menu> Update(string id, Menu newMenu)
        {
            MenuServices.UpdateMenu(id, newMenu);
            return newMenu;
        }

        // DELETE: api/Menu/5
        [HttpDelete("{DeleteMneu}")]
        public ActionResult<Menu> Delete(string id)
        {
            var menu = MenuServices.Get(id);
            if (menu == null)
                return NotFound();
            MenuServices.Delete(id);
            return menu;
        }
    }
}