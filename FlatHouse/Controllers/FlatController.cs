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
        private FlatDAO dao = new FlatDAO();

        [HttpGet]
        public IActionResult GetFlats()
        {
            return Ok(dao.Get());
        }

        [HttpPost]
        public IActionResult CreateFlat([FromBody] FlatJSON flat)
        {
            dao.Insert(new Flat(5, 55));
            dao.Insert(new Flat(6, 105,"Whis"));
            if (flat.ResidentName == null)
            {
                dao.Insert(new Flat(flat.FlatNumber, flat.FloorArea));
                return Ok();
            }
           dao.Insert(new Flat(flat.FlatNumber, flat.FloorArea, flat.ResidentName));
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetFlat([FromRoute] int id)
        {
            return Ok(dao.Get().Find(_ => _.FlatNumber == id));
        }

        [HttpPut("{id}")]
        public IActionResult ChangeFlat([FromRoute] int id, [FromBody] FlatJSON flat)
        {
            Flat realFlat = flat.ResidentName == null ? new Flat(flat.FlatNumber, flat.FloorArea) : new Flat(flat.FlatNumber, flat.FloorArea, flat.ResidentName);
            return Ok(dao.Update(id, realFlat));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFlat([FromRoute] int id)
        {
            return Ok(dao.Delete(id));
        }
    }
}
