using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using secondProject.Dtos.ThingsToDoDtos;
using secondProject.Interfaces;
using secondProject.Models;

namespace secondProject.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ThingsToDoController : ControllerBase
    {
        private readonly IThingsToDoRepository _thingsToDoRepository;
        private readonly IMapper _mapper;

        public ThingsToDoController(IThingsToDoRepository thingsToDoRepository,IMapper mapper)
        {
            _thingsToDoRepository = thingsToDoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ThingsToDo>))]
        [ProducesResponseType(400)]
        public IActionResult  GetThingsToDo()
        {
            var thingsToDo =  _mapper.Map<List<ThingsToDo>>(_thingsToDoRepository.GetThingsToDoList());
            if (thingsToDo == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest("The model state was found not valid.");
            return Ok(thingsToDo);
        }

        [HttpGet("{thingsToDoId}")]
        [ProducesResponseType(200, Type = typeof(ThingsToDo))]
        [ProducesResponseType(400)]
        public IActionResult GetThingsToDoById(int thingsToDoId)
        {
            var thingsToDo = _thingsToDoRepository.GetThingsToDo(thingsToDoId);
            if (thingsToDo == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest("The model state was found not valid.");
            return Ok(thingsToDo);
        }

        [HttpGet("thingsToDo/{name}")]
        [ProducesResponseType(200, Type = typeof(ThingsToDo))]
        [ProducesResponseType(400)]
        public IActionResult searchByName(string name)
        {
            var thingsToDo = _thingsToDoRepository.searchByName(name);
            if(thingsToDo == null) return NotFound();
            return Ok(thingsToDo);
        }

        [HttpPost]
        public IActionResult CreateThingsToDo([FromBody] ThingsToDoDto thingsToDoDto)
        {

            var thingsToDo = _thingsToDoRepository.GetThingsToDoList().Where(t => t.Name.ToUpper().Trim() == thingsToDoDto.Name.ToUpper().Trim())
            .FirstOrDefault();

            if (thingsToDo != null)
            {
                ModelState.AddModelError("", "Country already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            //var thingsToDoMap = _mapper.Map<ThingsToDo>(thingsToDoDto);
            var thingsToDoMap = new ThingsToDo
            {
                Name = thingsToDoDto.Name,
                Price = thingsToDoDto.Price,
                ImageUrls = thingsToDoDto.ImageUrls,
            };

            if (!_thingsToDoRepository.CreateThingsToDo(thingsToDoMap))
            {
                ModelState.AddModelError("", "Something went wrong.Please Try again later.");
                return StatusCode(500, ModelState);
            }
            return Ok("Things to do Created Successfully!");
        }


        [HttpDelete("{thingsToDoId}")]
        public IActionResult DeleteThingsToDo(int thingsToDoId)
        {
            if (!_thingsToDoRepository.ThingsToDoExists(thingsToDoId)) return BadRequest("Things to do does not exist!");

            var thingsToDo = _thingsToDoRepository.GetThingsToDo(thingsToDoId);
            if (!ModelState.IsValid) return BadRequest("The model state was found not valid.");

            if(!_thingsToDoRepository.DeleteThingsToDo(thingsToDo)) return BadRequest("Could not delete things to do.");

            return Ok("Deleted Successfully!");
        }

        [HttpPut("{thingsToDoId}")]
        public IActionResult UpdateThingsToDo(int thingsToDoId, [FromBody] ThingsToDoDto thingsToDo)
        {
            if (!_thingsToDoRepository.ThingsToDoExists(thingsToDoId)) return BadRequest("Things to do does not exist!");
            if(thingsToDo == null) return BadRequest(ModelState);

            var existingThingsToDo = _thingsToDoRepository.GetThingsToDo(thingsToDoId);

            existingThingsToDo.Name = thingsToDo.Name;
            existingThingsToDo.Price = thingsToDo.Price;
            existingThingsToDo.ImageUrls = thingsToDo.ImageUrls;
            
            if (!_thingsToDoRepository.UpdateThingsToDo(existingThingsToDo))
            {
                ModelState.AddModelError("", "Unsuccessfull operation. Try again!");
                return StatusCode(500, ModelState);
            }
            return Ok("Succes");
        }
    }
}
