using Event_manager_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_manager_API.Controllers
{
    
    [ApiController]
    [Route("Event")]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EventController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        //GET ALL
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Event>>> GetAll()
        {
            return await dbContext.Event.ToListAsync();
        }

        //GET BY ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Event>> GetById(int id)
        {
            return await dbContext.Event.FirstOrDefaultAsync(x => x.Id == id);
        }


        //POST EVENT
        [HttpPost]
        public async Task<ActionResult> Post(Event event_)
        {
            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == celular.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("No existe la marca");
            }
            */
            dbContext.Add(event_);

            await dbContext.SaveChangesAsync();

            return Ok();
        }


        //UPDATE EVENT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Event event_, int id)
        {
            if (event_.Id != id)
            {
                return BadRequest("El Id del celular no coincide con el establecido en la URL.");
            }

            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == event_.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("No existe la marca");
            }*/

            dbContext.Update(event_);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE EVENT
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Event.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("No se encontro el celular en la base de datos");
            }
            dbContext.Remove(new Event()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
