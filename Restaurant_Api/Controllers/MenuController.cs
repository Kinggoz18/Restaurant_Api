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
        public ActionResult<Menu> GetMenu(string id)
        {
            var menu = MenuServices.Get(new ObjectId(id));
            if (menu == null)
            {
                return NotFound();
            }
            return menu;
        }

        // POST: api/Menu
        [HttpPost]
        public ActionResult<Menu> CreateMenu(Menu menu)
        {
            MenuServices.Add(menu);
            return CreatedAtAction("GetMenu", new { id = menu._id.ToString() }, menu);
        }

        // PUT: api/Menu/5
        [HttpPut("{UpdateMenu}")]
        public IActionResult UpdateMenu(string id, Menu menu)
        {
            if (id != menu._id.ToString())
            {
                return BadRequest();
            }
            MenuServices.UpdateMenu(menu);
            return NoContent();
        }

        // DELETE: api/Menu/5
        [HttpDelete("{DeleteMneu}")]
        public ActionResult<Menu> DeleteMenu(string id)
        {
            var menu = MenuServices.Get(new ObjectId(id));
            if (menu == null)
            {
                return NotFound();
            }
            MenuServices.Delete(new ObjectId(id));
            return menu;
        }
    }
}