using System;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Api.Models;
using Restaurant_Api.Services;

namespace Restaurant_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuItemController : ControllerBase
    {
        public MenuItemController()
        {
        }

        //Get All Menu items
        [HttpGet]
        [Route("GetAllMenuItems/")]
        public ActionResult<List<MenuItem>> GetAllMenuItems() => MenuItemServices.GetAll();

        //Get a particular menu item
        [HttpGet]
        [Route("GetMenuItem/{itemId}")]
        public ActionResult<MenuItem> GetItemByName(string itemId) => MenuItemServices.Get(itemId);

        //Add a new menu item
        [HttpPost]
        [Route("AddMenuItem/")]
        public ActionResult<MenuItem> AddMenuItem(MenuItem menuItem) => MenuItemServices.Add(menuItem);

        //Removes a  menu item
        [HttpDelete]
        [Route("RemoveMenuItem/{itemId}")]
        public void RemoveMenuItem(string itemId) => MenuItemServices.Remove(itemId);

        //Updates a  menu item
        [HttpPut]
        [Route("UpdateMenuItem/{itemId}")]
        public void UpdateMenuItem(string itemId, MenuItem newItem) => MenuItemServices.Update(itemId, newItem);

        [HttpPost]
        [Route("MenuItemImage/")]
        public async void UpdateItemImage(IFormFile file, string FileName) => MenuItemServices.AddImage(file, FileName);

    }
}

