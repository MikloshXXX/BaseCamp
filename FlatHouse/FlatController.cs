using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlatHouse
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        DAOFlat dao = new DAOFlat();
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(dao.Get());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(dao.Get().Find(_ => _.ApartmentNumber == id));
        }
        [HttpPost]
        public IActionResult Post([FromBody] FlatJSON flat)
        {
            return Ok(dao.Insert(new Flat(flat.ApartmentNumber,flat.FloorArea,flat.ResidentName)));
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FlatJSON flat)
        {
            return Ok(dao.Update(id, new Flat(flat.ApartmentNumber, flat.FloorArea, flat.ResidentName)));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteFlat([FromRoute] int id)
        {
            return Ok(dao.Delete(id));
        }
    }
}
