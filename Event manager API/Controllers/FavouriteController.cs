using AutoMapper;
using Event_manager_API.DTOs.Get;
using Event_manager_API.DTOs.Set;
using Event_manager_API.Entities;
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
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "favouritename": "string",
        ///         "email": "favourite@example.com",
        ///         "password": "string",
        ///         "role": "string"
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] FavouriteDTO favouriteDTO)
        {
            var favourite = mapper.Map<Favourite>(favouriteDTO);
            dbContext.Add(favourite);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Favourite.
        /// </summary>
        /// <returns>A newly created Favourite</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a favourite follow this strcture, and specify id
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "favouritename": "string",
        ///         "email": "favourite@example.com",
        ///         "password": "string",
        ///         "role": "favourite or admin"
        ///     }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutFavourite(FavouriteDTO favouriteDTO, [FromRoute] int id)
        {
            var exists = await dbContext.Favourite.AnyAsync(x => x.Id == id);
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
            var favourite = mapper.Map<Favourite>(favouriteDTO);
            favourite.Id = id;
            dbContext.Update(favourite);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Favourite by Id.
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