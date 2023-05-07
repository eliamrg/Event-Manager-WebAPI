using Event_manager_API.Entities;
using Event_manager_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Follow")]
    public class FollowController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<FollowController> logger;
        private readonly IWebHostEnvironment env;
        
        public FollowController(
                    ApplicationDbContext context,
                    IService service,
                    ServiceTransient serviceTransient,
                    ServiceScoped serviceScoped,
                    ServiceSingleton serviceSingleton,
                    ILogger<FollowController> logger,
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
        /// Get a list of Follows.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Follow>>> GetAll()
        {
            logger.LogInformation("Getting Follow List");
            return await dbContext.Follow.Include(x => x.Admin).Include(x => x.User).ToListAsync();
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Follow by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Follow>> GetById(int id)
        {
            return await dbContext.Follow.FirstOrDefaultAsync(x => x.Id == id);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Follow.
        /// </summary>
        /// <param name="follow"></param>
        /// <returns>A newly created Follow</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new follow follow this strcture
        ///     {
        ///        "createdAt": "2023-05-06T18:01:53.212Z",
        ///        "name": "Arena Monterrey",
        ///        "address": "Av. Francisco I. Madero 2500, Centro, 64010 Monterrey, N.L.",
        ///        "capacity": "0"
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post(Follow follow)
        {
            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == celular.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }
            */
            dbContext.Add(follow);

            await dbContext.SaveChangesAsync();

            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Follow.
        /// </summary>
        /// <param name="follow"></param>
        /// <param name="id"></param>
        /// <returns>A newly created Follow</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a follow follow this strcture, and specify id
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
        public async Task<ActionResult> Put(Follow follow, int id)
        {
            if (follow.Id != id)
            {
                return BadRequest("The Id does not match the one established in the URL.");
            }

            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == event_.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }*/

            dbContext.Update(follow);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Follow by Id.
        /// </summary>

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Follow.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("Not found in the database");
            }
            dbContext.Remove(new Follow()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}