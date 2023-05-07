using Event_manager_API.Entities;
using Event_manager_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Coupon")]
    public class CouponController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<CouponController> logger;
        private readonly IWebHostEnvironment env;
       
        public CouponController(
                    ApplicationDbContext context,
                    IService service,
                    ServiceTransient serviceTransient,
                    ServiceScoped serviceScoped,
                    ServiceSingleton serviceSingleton,
                    ILogger<CouponController> logger,
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
        /// Get a list of Coupons.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Coupon>>> GetAll()
        {
            logger.LogInformation("Getting Coupon List");
            return await dbContext.Coupon.ToListAsync();
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Coupon by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Coupon>> GetById(int id)
        {
            logger.LogInformation("Getting Coupon");
            return await dbContext.Coupon.FirstOrDefaultAsync(x => x.Id == id);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Coupon.
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns>A newly created Coupon</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new Coupon follow this strcture
        ///     {
        ///        "createdAt": "2023-05-06T18:01:53.212Z",
        ///        "name": "Arena Monterrey",
        ///        "address": "Av. Francisco I. Madero 2500, Centro, 64010 Monterrey, N.L.",
        ///        "capacity": "0"
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post(Coupon coupon)
        {
            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == celular.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }
            */
            dbContext.Add(coupon);

            await dbContext.SaveChangesAsync();
            logger.LogInformation("Coupon Added");
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Coupon.
        /// </summary>
        /// <param name="coupon"></param>
        /// <param name="id"></param>
        /// <returns>A newly created Coupon</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a Coupon follow this strcture, and specify id
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
        public async Task<ActionResult> Put(Coupon coupon, int id)
        {
            if (coupon.Id != id)
            {
                return BadRequest("The Id does not match the one established in the URL.");
            }

            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == event_.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }*/

            dbContext.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Coupon Updated");
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Coupon by Id.
        /// </summary>

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Coupon.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("Not found in the database");
            }
            dbContext.Remove(new Coupon()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Coupon Deleted");
            return Ok();
        }
    }
}
