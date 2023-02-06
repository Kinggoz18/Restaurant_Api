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
        [HttpGet("GetAllMenu/")]
        public IEnumerable<Menu> GetAllMenu()
        {
            return MenuServices.GetAllMenu();
        }

        // GET: api/Menu/5
        [HttpGet("GetMenu/{menuName}")]
        public ActionResult<Menu> Get(string menuName)
        {
            return MenuServices.Get(menuName);
        }


        // POST: api/Menu
        [HttpPost("AddMenu/")]
        public void CreateMenu(string MenuName)
        {
            Menu menu = new Menu
            {
                Name = MenuName,
                FoodList = new List<MenuItem>()
            };

            MenuServices.Add(menu);
        }

        // PUT: api/Menu/5
        [HttpPut("UpdateMenu/{menuName}")]
        public ActionResult<Menu> Update(string id, Menu menuName)
        {
            return MenuServices.UpdateMenu(id, menuName);
        }

        // DELETE: api/Menu/5
        [HttpDelete("DeleteMenu/{name}")]
        public void Delete(string name)
        {
            var menu = MenuServices.Get(name);
            if (menu == null)
                return;
            MenuServices.Delete(name);
        }
    }
}