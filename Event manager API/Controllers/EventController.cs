using AutoMapper;
using Event_manager_API.DTOs.Get;
using Event_manager_API.DTOs.Set;
using Event_manager_API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Event")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
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
            var event_ = await dbContext.Event.Include(db=> db.Admin).Include(db=>db.Location).ToListAsync();
            return mapper.Map<List<GetEventDTO>>(event_);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Event by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetEventDTO>> GetById(int id)
        {
            var event_ = await dbContext.Event.Include(db => db.Admin).Include(db => db.Location).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetEventDTO>(event_);
        }

        //GET BY NAME-------------------------------------------------------------------------------

        /// <summary>
        /// Get Event by Name.
        /// </summary>
        [HttpGet("Name/{name}")]
        public async Task<ActionResult<List<GetEventDTO>>> GetByName(string name)
        {
            var event_ = await dbContext.Event.Where(x => x.Name == name).Include(db => db.Admin).Include(db => db.Location).ToListAsync();
            return mapper.Map<List<GetEventDTO>>(event_);
        }
        //GET BY DATE-------------------------------------------------------------------------------

        /// <summary>
        /// Get Event by Date.
        /// </summary>
        [HttpGet("Date/{date}")]
        public async Task<ActionResult<List<GetEventDTO>>> GetByDate([FromHeader]DateTime date)
        {
            var event_ = await dbContext.Event.Where(x => x.Date == date).Include(db => db.Admin).Include(db => db.Location).ToListAsync();
            return mapper.Map<List<GetEventDTO>>(event_);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Event by LocationId.
        /// </summary>
        [HttpGet("Location/{LocationId:int}")]
        public async Task<ActionResult<List<GetEventDTO>>> GetByLocationId(int LocationId)
        {
            var event_ = await dbContext.Event.Where(x => x.LocationId == LocationId).Include(db => db.Admin).Include(db => db.Location).ToListAsync();
            return mapper.Map<List<GetEventDTO>>(event_);
        }

        /// <summary>
        /// Get Event and coupons list by Id.
        /// </summary>
        [HttpGet("Coupons/{EventId:int}")]
        public async Task<ActionResult<GetEventDTOwithCoupons>> GetByIdListCoupons(int EventId)
        {

            var object_ = await dbContext.Event
                
                .Include(DB => DB.Coupons)
                .FirstOrDefaultAsync(x => x.Id == EventId);
            return mapper.Map<GetEventDTOwithCoupons>(object_);
        }

        /// <summary>
        /// Get Event and Form Responses by Id.
        /// </summary>
        [HttpGet("Forms/{EventId:int}")]
        public async Task<ActionResult<GetEventDTOwithForms>> GetByIdListForms(int EventId)
        {

            var object_ = await dbContext.Event
                .Include(DB => DB.FormResponses)
                .FirstOrDefaultAsync(x => x.Id == EventId);
            return mapper.Map<GetEventDTOwithForms>(object_);
        }

        
        /// <summary>
        /// Get Event and Tickets list by Id.
        /// </summary>
        [HttpGet("Tickets/{EventId:int}")]
        public async Task<ActionResult<GetEventDTOwithTickets>> GetByIdListTickets(int EventId)
        {

            var object_ = await dbContext.Event
                .Include(DB => DB.Tickets)
                .FirstOrDefaultAsync(x => x.Id == EventId);
            return mapper.Map<GetEventDTOwithTickets>(object_);
        }

        /// <summary>
        /// Get Popular Events.
        /// </summary>
        [HttpGet("Popular")]
        [AllowAnonymous]
        public async Task<ActionResult<List<GetEventDTO>>> GetPopularEvents()
        {

            //FROM THE EVENTS CREATED ON THE LAST 30 DAYS SELECT THE 25 WITH MORE TICKETS SOLD
            var date30days = DateTime.Now.AddDays(30);
            var today = DateTime.Now;

            var object_ = await dbContext.Event
                .Include(DB => DB.FormResponses)
                .Where(x => x.CreatedAt < date30days && x.CreatedAt > today)
                .OrderBy(x=> x.ticketsSold)
                .Take(25)
                .ToListAsync();

            
            return mapper.Map<List<GetEventDTO>>(object_);
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
        ///         "name": "string",
        ///         "description": "string",
        ///         "ticketPrice": 0,
        ///         "eventCapacity": 0,
        ///         "date": "2023-05-09T02:48:00.083Z",
        ///         "adminId": 0,
        ///         "locationId": 0
        ///     }
        ///
        /// USE USER ID, NOT ACCOUNT ID
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] EventDTO event_DTO)
        {
            

            var adminExists = await dbContext.User.AnyAsync(x => (x.Id == event_DTO.AdminId && x.Role=="admin"));
            if (!adminExists)
            {
                return BadRequest("That Administrator does not exist");
            }

            var locationExists = await dbContext.Location.AnyAsync(x => x.Id == event_DTO.LocationId );
            if (!locationExists)
            {
                return BadRequest("That Location does not exist");
            }

            var event_ = mapper.Map<Event>(event_DTO);
            event_.CreatedAt = DateTime.Now;
            event_.ticketsSold = 0;
            dbContext.Add(event_);
            await dbContext.SaveChangesAsync();

            //Create coupon with no benefits

            CouponDTO couponDTO = new CouponDTO();

            couponDTO.Code = "NoCode";
            couponDTO.EventId = event_.Id;
            couponDTO.Description = "No Benefits";
            couponDTO.DiscountPercentage = 0;  
            var coupon = mapper.Map<Coupon>(couponDTO);
            coupon.CreatedAt = DateTime.Now;
            dbContext.Add(coupon);
            await dbContext.SaveChangesAsync();

            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update Event.
        /// </summary>
        /// <returns>A newly created Event</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update event_ follow this strcture, and specify id
        ///     {
        ///         "name": "string",
        ///         "description": "string",
        ///         "ticketPrice": 0,
        ///         "eventCapacity": 0,
        ///         "date": "2023-05-09T02:48:00.083Z",
        ///         "adminId": 0,
        ///         "locationId": 0
        ///     }
        ///
        /// USE USER ID, NOT ACCOUNT ID
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutEvent(EventDTO event_DTO, [FromRoute] int id)
        {
            var exists = await dbContext.Event.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            var adminExists = await dbContext.User.AnyAsync(x => (x.Id == event_DTO.AdminId && x.Role == "admin"));
            if (!adminExists)
            {
                return BadRequest("That Administrator does not exist");
            }

            var locationExists = await dbContext.Location.AnyAsync(x => x.Id == event_DTO.LocationId);
            if (!locationExists)
            {
                return BadRequest("That Location does not exist");
            }

            var event_ = mapper.Map<Event>(event_DTO);
            event_.Id = id;
            event_.CreatedAt = DateTime.Now;
            dbContext.Update(event_);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Event.
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
