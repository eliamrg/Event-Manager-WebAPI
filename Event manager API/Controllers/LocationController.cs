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
    [Route("Location")]
    
    public class LocationController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<LocationController> logger;
        private readonly IMapper mapper;
        public LocationController(
                    ApplicationDbContext context,
                    ILogger<LocationController> logger,
                    IMapper mapper
               )
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Locations.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetLocationDTO>>> GetAll()
        {
            logger.LogInformation("Getting Location List");
            var location = await dbContext.Location.ToListAsync();
            return mapper.Map<List<GetLocationDTO>>(location);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Location by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetLocationDTO>> GetById(int id)
        {
            
            var location = await dbContext.Location.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetLocationDTO>(location);
        }

        //GET BY ID WITH EVENTLIST-------------------------------------------------------------------------------

        /// <summary>
        /// Get Location EventsList by Id.
        /// </summary>
        [HttpGet("EventsList/{LocationId:int}")]
        public async Task<ActionResult<GetLocationDTOwithEvents>> GetByIdEventList(int LocationId)
        {

            var location = await dbContext.Location
                .Include(DB => DB.EventsList)
                .FirstOrDefaultAsync(x => x.Id == LocationId);
            return mapper.Map<GetLocationDTOwithEvents>(location);
        }



        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Location.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new location follow this strcture
        ///     {
        ///         "name": "string",
        ///         "address": "string",
        ///         "capacity": 0
        ///      }
        ///
        /// </remarks>

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> Post([FromBody] LocationDTO locationDTO)
        {
            var location = mapper.Map<Location>(locationDTO);
            dbContext.Add(location);
            location.CreatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update Location.
        /// </summary>
        /// <returns>A newly created Location</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update location follow this strcture, and specify id
        ///     {
        ///         "name": "string",
        ///         "address": "string",
        ///         "capacity": 0
        ///      }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> PutLocation(LocationDTO locationDTO, [FromRoute] int id)
        {
            var exists = await dbContext.Location.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("Does not exist");
            }

            var location = mapper.Map<Location>(locationDTO);
            location.Id = id;
            location.CreatedAt = DateTime.Now;
            dbContext.Update(location);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Location .
        /// </summary>
        /// 

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Location.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Location()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
