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
    [Route("Favourite")]

    public class FavouriteController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<FavouriteController> logger;
        private readonly IMapper mapper;
        public FavouriteController(
                    ApplicationDbContext context,
                    ILogger<FavouriteController> logger,
                    IMapper mapper
               )
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Favourites.
        /// </summary>
        
        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<List<GetFavouriteDTO>>> GetAll()
        {
            logger.LogInformation("Getting Favourite List");
            var favourite = await dbContext.Favourite.ToListAsync();
            return mapper.Map<List<GetFavouriteDTO>>(favourite);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Favourite by Id.
        /// </summary>
        
        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<GetFavouriteDTO>> GetById(int id)
        {
            var favourite = await dbContext.Favourite.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetFavouriteDTO>(favourite);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Favourite.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new favourite follow this strcture
        ///     {
        ///         "userId": 0,
        ///         "eventId": 0
        ///     }
        ///
        /// USE USER ID, NOT ACCOUNT ID
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] FavouriteDTO favouriteDTO)
        {
            
            
            var userExists = await dbContext.User.AnyAsync(x => x.Id == favouriteDTO.UserId);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }

            var eventExists = await dbContext.Event.AnyAsync(x => x.Id == favouriteDTO.EventId);
            if (!eventExists)
            {
                return BadRequest("That Event does not exist");
            }

            var favourite = mapper.Map<Favourite>(favouriteDTO);
            favourite.CreatedAt = DateTime.Now;
            dbContext.Add(favourite);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update Favourite.
        /// </summary>
        /// <returns>A newly created Favourite</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update favourite follow this strcture, and specify id
        ///     {
        ///         "userId": 0,
        ///         "eventId": 0
        ///     }
        ///
        /// USE USER ID, NOT ACCOUNT ID
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutFavourite(FavouriteDTO favouriteDTO, [FromRoute] int id)
        {
            var exists = await dbContext.Favourite.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("Does not exist");
            }

            var userExists = await dbContext.User.AnyAsync(x => x.Id == favouriteDTO.UserId);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }

            var eventExists = await dbContext.Event.AnyAsync(x => x.Id == favouriteDTO.EventId);
            if (!eventExists)
            {
                return BadRequest("That Event does not exist");
            }

            var favourite = mapper.Map<Favourite>(favouriteDTO);
            favourite.Id = id;
            favourite.CreatedAt = DateTime.Now;
            dbContext.Update(favourite);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Favourite.
        /// </summary>
        /// 


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Favourite.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Favourite()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}