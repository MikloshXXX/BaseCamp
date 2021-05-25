using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlatHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        private static List<Flat> flatHouse = new List<Flat>
        {
            new Flat(25.5f),
            new Flat(33f, "Mykola"),
            new Flat(47.7f, "Pavel"),
            new Flat(54f)
        };

        [HttpGet]
        public IActionResult GetFlats()
        {
            return Ok(flatHouse);
        }

        [HttpGet("{id}")]
        public IActionResult GetFlat([FromRoute]int id)
        {
            return Ok(flatHouse[id]);
        }

        [HttpPost]
        public IActionResult CreateFlat([FromBody]float flatArea)
        {
            flatHouse.Add(new Flat(flatArea));
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult ChangeFlat([FromRoute]int id, [FromBody] float floatArea)
        {
            flatHouse[id] = new Flat(floatArea);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteFlat([FromRoute]int id)
        {
            flatHouse.RemoveAt(id);
            return Ok();
        }
    }
}
