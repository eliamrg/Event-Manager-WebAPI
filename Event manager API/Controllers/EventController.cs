using AutoMapper;
using Event_manager_API.DTOs.Get;
using Event_manager_API.DTOs.Set;
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
        private readonly ILogger<EventController> logger;
        private readonly IMapper mapper;
        public EventController(
                    ApplicationDbContext context,
                    ILogger<EventController> logger,
                    IMapper mapper
               )
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Events.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetEventDTO>>> GetAll()
        {
            logger.LogInformation("Getting Event List");
            var event_ = await dbContext.Event.ToListAsync();
            return mapper.Map<List<GetEventDTO>>(event_);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Event by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetEventDTO>> GetById(int id)
        {
            var event_ = await dbContext.Event.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetEventDTO>(event_);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Event.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new event_ follow this strcture
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "event_name": "string",
        ///         "email": "event_@example.com",
        ///         "password": "string",
        ///         "role": "string"
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] EventDTO event_DTO)
        {
            var event_ = mapper.Map<Event>(event_DTO);
            dbContext.Add(event_);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Event.
        /// </summary>
        /// <returns>A newly created Event</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a event_ follow this strcture, and specify id
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "event_name": "string",
        ///         "email": "event_@example.com",
        ///         "password": "string",
        ///         "role": "event_ or admin"
        ///     }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutEvent(EventDTO event_DTO, [FromRoute] int id)
        {
            var exists = await dbContext.Event.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("Does not exist");
            }

            /*
            var relationshipExists = await dbContext.Relationship.AnyAsync(x => x.Id == Table.RelationshipId);
            if (!relationshipExists)
            {
                return BadRequest("Does relationship does not exist");
            }*/
            var event_ = mapper.Map<Event>(event_DTO);
            event_.Id = id;
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
                return NotFound();
            }
            dbContext.Remove(new Event()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
