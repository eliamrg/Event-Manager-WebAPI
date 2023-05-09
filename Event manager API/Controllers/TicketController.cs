using AutoMapper;
using Event_manager_API.DTOs.Get;
using Event_manager_API.DTOs.Set;
using Event_manager_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<TicketController> logger;
        private readonly IMapper mapper;
        public TicketController(
                    ApplicationDbContext context,
                    ILogger<TicketController> logger,
                    IMapper mapper
               )
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Tickets.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetTicketDTO>>> GetAll()
        {
            logger.LogInformation("Getting Ticket List");
            var ticket = await dbContext.Ticket.ToListAsync();
            return mapper.Map<List<GetTicketDTO>>(ticket);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Ticket by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetTicketDTO>> GetById(int id)
        {
            var ticket = await dbContext.Ticket.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetTicketDTO>(ticket);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Ticket.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new ticket follow this strcture
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "ticketname": "string",
        ///         "email": "ticket@example.com",
        ///         "password": "string",
        ///         "role": "string"
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] TicketDTO ticketDTO)
        {
            var ticket = mapper.Map<Ticket>(ticketDTO);
            dbContext.Add(ticket);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Ticket.
        /// </summary>
        /// <returns>A newly created Ticket</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a ticket follow this strcture, and specify id
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "ticketname": "string",
        ///         "email": "ticket@example.com",
        ///         "password": "string",
        ///         "role": "ticket or admin"
        ///     }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutTicket(TicketDTO ticketDTO, [FromRoute] int id)
        {
            var exists = await dbContext.Ticket.AnyAsync(x => x.Id == id);
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
            var ticket = mapper.Map<Ticket>(ticketDTO);
            ticket.Id = id;
            dbContext.Update(ticket);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Ticket by Id.
        /// </summary>
        /// 


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Ticket.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Ticket()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}