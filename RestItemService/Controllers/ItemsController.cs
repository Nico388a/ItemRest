using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemLib.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestItemService.Controllers
{
    [Route("api/localItems")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private static List<Item> _items = new List<Item>()

        {
            new Item(1, "Bread", "Low", 33),
            new Item(2, "Bread", "Middle", 21),
            new Item(3, "Beer", "Low", 70.5),
            new Item(4, "Soda", "High", 21.4),
            new Item(5, "Milk", "Low", 55.8)
        };
        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _items;
        }

        //[HttpGet]
        //[Route("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult Get(int id)
        //{
        //    if (_items.Exists(i => i.ID == id))
        //    {
        //        return Ok(_items.Find(i => i.ID == id));
        //    }

        //    return NotFound($"Item id {id} not found");
        //}
        //GET api/<ItemsController>/5
        [HttpGet]
        [Route("{id}")]
        public Item Get(int id)
        {
            return _items.Find(i => i.ID == id);
        }

        //Get Substring
        [HttpGet]
        [Route("name/{substring}")]
        public IEnumerable<Item> GetFromSubstring(string substring)
        {
           return _items.FindAll(i => i.Name.Contains(substring, StringComparison.CurrentCultureIgnoreCase));
        }

        //Get Quality
        [HttpGet]
        [Route("quality/{quality}")]
        public IEnumerable<Item> GetQuality(string quality)
        {
            return _items.FindAll(i => i.Quality.ToLower() == quality.ToLower());
        }

        //Get Quantity
        [HttpGet]
        [Route("quantity/{quantity}")]
        public IEnumerable<Item> GetQuantity(double quantity)
        {
            return _items.FindAll(i => i.Quantity <= quantity);
        }

        //Get With Filter
        [HttpGet]
        [Route("search")]
        public IEnumerable<Item> GetWithFilter([FromQuery] FilterItem filter)
        {
       
        if (filter.HighQuantity == 0)
        {
            filter.HighQuantity = double.MaxValue;
        }
        else
        {
            filter.HighQuantity = filter.HighQuantity;
        }
        return _items.FindAll(i => i.Quantity > filter.LowQuantity && i.Quantity < filter.HighQuantity);
        }

        

        // POST api/<ItemsController>
        [HttpPost]
        [Route("{id}")]
        public void Post([FromBody] Item value)
        {
            _items.Add(value);
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        [Route("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            Item item = Get(id);
            if (item != null)
            {
                item.ID = value.ID;
                item.Name = value.Name;
                item.Quality = value.Quality;
                item.Quantity = value.Quantity;
            }
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        [Route("{id}")]
        public void Delete(int id)
        {
            Item item = Get(id);
            _items.Remove(item);
        }
    }
}
