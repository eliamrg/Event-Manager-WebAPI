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
    [Route("UpcomingEvents")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class UpcomingEvents : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<UserController> logger;
        private readonly IMapper mapper;
        public UpcomingEvents(
                    ApplicationDbContext context,
                    ILogger<UserController> logger,
                    IMapper mapper
               )
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get User's upcoming events (30 days) by Id.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{UserId:int}")]
        public async Task<ActionResult<List<GetTicketDTO>>> GetByIdEventReminder(int UserId)
        {

            var userExists = await dbContext.User.AnyAsync(x => x.Id == UserId);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }

            var date30days = DateTime.Now.AddDays(30);
            var today = DateTime.Now;

            var UserEvents = await dbContext.Ticket.Where(x => x.UserId == UserId && x.Event.Date < date30days && x.Event.Date > today).Include(x => x.Event).ToListAsync();

            return mapper.Map<List<GetTicketDTO>>(UserEvents);

        }
    }
}