using Event_manager_API.Entities;
using Event_manager_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Location")]
    public class LocationController : ControllerBase
    {
        
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<LocationController> logger;
        private readonly IWebHostEnvironment env;
        
        public LocationController(
                    ApplicationDbContext context, 
                    IService service,
                    ServiceTransient serviceTransient, 
                    ServiceScoped serviceScoped,
                    ServiceSingleton serviceSingleton, 
                    ILogger<LocationController> logger,
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
        /// Get a list of Locations.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Location>>> GetAll()
        {
            logger.LogInformation("Getting Location List");
            return await dbContext.Location.ToListAsync();
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Location by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Location>> GetById(int id)
        {
            return await dbContext.Location.FirstOrDefaultAsync(x => x.Id == id);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns>A newly created Location</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new location follow this strcture
        ///     {
        ///        "createdAt": "2023-05-06T18:01:53.212Z",
        ///        "name": "Arena Monterrey",
        ///        "address": "Av. Francisco I. Madero 2500, Centro, 64010 Monterrey, N.L.",
        ///        "capacity": "0"
        ///     }
        ///
        /// </remarks>
       
        [HttpPost]
        
        public async Task<ActionResult> Post(Location location)
        {
            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == celular.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }
            */
            dbContext.Add(location);

            await dbContext.SaveChangesAsync();

            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Location.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="id"></param>
        /// <returns>A newly created Location</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a location follow this strcture, and specify id
        ///     {
        ///        "Id": "1",
        ///        "createdAt": "2023-05-06T18:01:53.212Z",
        ///        "name": "Arena Monterrey",
        ///        "address": "Av. Francisco I. Madero 2500, Centro, 64010 Monterrey, N.L.",
        ///        "capacity": "0"
        ///     }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Location location, int id)
        {
            if (location.Id != id)
            {
                return BadRequest("The Id does not match the one established in the URL.");
            }

            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == event_.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }*/

            dbContext.Update(location);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Location by Id.
        /// </summary>
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Location.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("Not found in the database");
            }
            dbContext.Remove(new Location()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
