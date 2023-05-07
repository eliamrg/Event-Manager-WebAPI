using Event_manager_API.Entities;
using Event_manager_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_manager_API.Controllers
{
    
    [ApiController]
    [Route("Event")]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<EventController> logger;
        private readonly IWebHostEnvironment env;
        
        public EventController(
                    ApplicationDbContext context,
                    IService service,
                    ServiceTransient serviceTransient,
                    ServiceScoped serviceScoped,
                    ServiceSingleton serviceSingleton,
                    ILogger<EventController> logger,
                    IWebHostEnvironment env
               )
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Events.
        /// </summary>

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Event>>> GetAll()
        {
            logger.LogInformation("Getting Event List");
            return await dbContext.Event.ToListAsync();
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Event by Id.
        /// </summary>
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Event>> GetById(int id)
        {
            return await dbContext.Event.FirstOrDefaultAsync(x => x.Id == id);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add an Event.
        /// </summary>
        /// <param name="event_"></param>
        /// <returns>A newly created Event</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new Event follow this strcture
        ///     {
        ///        "createdAt": "2023-05-06T18:01:53.212Z",
        ///        "name": "Arena Monterrey",
        ///        "address": "Av. Francisco I. Madero 2500, Centro, 64010 Monterrey, N.L.",
        ///        "capacity": "0"
        ///     }
        ///
        /// </remarks>
        /// 

        [HttpPost]
        public async Task<ActionResult> Post(Event event_)
        {
            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == celular.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }
            */
            dbContext.Add(event_);

            await dbContext.SaveChangesAsync();

            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update an Event.
        /// </summary>
        /// <param name="event_"></param>
        /// <param name="id"></param>
        /// <returns>A newly created Event</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update an Event follow this strcture, and specify id
        ///     {
        ///        "Id": "1",
        ///        "createdAt": "2023-05-06T18:01:53.212Z",
        ///        "name": "Arena Monterrey",
        ///        "address": "Av. Francisco I. Madero 2500, Centro, 64010 Monterrey, N.L.",
        ///        "capacity": "0"
        ///     }
        ///
        /// </remarks>
        /// 

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Event event_, int id)
        {
            if (event_.Id != id)
            {
                return BadRequest("The Id does not match the one established in the URL.");
            }

            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == event_.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }*/

            dbContext.Update(event_);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Event by Id.
        /// </summary>
        /// 

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Event.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("Not found in the database");
            }
            dbContext.Remove(new Event()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
