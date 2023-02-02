using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Restaurant_Api.Models;
using Restaurant_Api.Services;

namespace Restaurant_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        //private readonly MenuServices _menuServices;

        public MenuController()
        {
            //_menuServices = MenuServices;
        }

        // GET: api/Menu
        [HttpGet("GetallMenu/{GetallMenu}")]
        public IEnumerable<Menu> GetAllMenu()
        {
            return MenuServices.GetAllMenu();
        }

        // GET: api/Menu/5
        [HttpGet("GetMenu/{GetMenu}")]
        /*public ActionResult<Menu> Get(string id)
        {
            var menu = MenuServices.Get(id);
            if (menu == null)
                return NotFound();
            return menu;
        }*/
        public ActionResult<Menu> Get(string name)
        {
            return MenuServices.Get(name);
        }


        // POST: api/Menu
        [HttpPost("addMenu/{addMenu}")]
        public void CreateMenu(Menu menu)
        {
            MenuServices.Add(menu);
        }

        // PUT: api/Menu/5
        [HttpPut("UpdateMenu/{UpdateMenu}")]
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
        [HttpDelete("DeleteMenu/{DeleteMneu}")]
        public ActionResult<Menu> Delete(string name)
        {
            var menu = MenuServices.Get(name);
            if (menu == null)
                return NotFound();
            MenuServices.Delete(name);
            return menu;
        }
    }
}