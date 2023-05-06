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
        public FavouriteController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Favourites.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Favourite>>> GetAll()
        {
            return await dbContext.Favourite.ToListAsync();
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Favourite by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Favourite>> GetById(int id)
        {
            return await dbContext.Favourite.FirstOrDefaultAsync(x => x.Id == id);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Favourite.
        /// </summary>
        /// <param name="favourite"></param>
        /// <returns>A newly created Favourite</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new favourite follow this strcture
        ///     {
        ///        "createdAt": "2023-05-06T18:01:53.212Z",
        ///        "name": "Arena Monterrey",
        ///        "address": "Av. Francisco I. Madero 2500, Centro, 64010 Monterrey, N.L.",
        ///        "capacity": "0"
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post(Favourite favourite)
        {
            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == celular.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }
            */
            dbContext.Add(favourite);

            await dbContext.SaveChangesAsync();

            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Favourite.
        /// </summary>
        /// <param name="favourite"></param>
        /// <param name="id"></param>
        /// <returns>A newly created Favourite</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a favourite follow this strcture, and specify id
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
        public async Task<ActionResult> Put(Favourite favourite, int id)
        {
            if (favourite.Id != id)
            {
                return BadRequest("The Id does not match the one established in the URL.");
            }

            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == event_.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }*/

            dbContext.Update(favourite);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Favourite by Id.
        /// </summary>

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Favourite.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("Not found in the database");
            }
            dbContext.Remove(new Favourite()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
