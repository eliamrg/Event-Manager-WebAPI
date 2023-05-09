using AutoMapper;
using Event_manager_API.DTOs.Get;
using Event_manager_API.DTOs.Set;
using Event_manager_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Coupon")]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<CouponController> logger;
        private readonly IMapper mapper;
        public CouponController(
                    ApplicationDbContext context,
                    ILogger<CouponController> logger,
                    IMapper mapper
               )
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Coupons.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetCouponDTO>>> GetAll()
        {
            logger.LogInformation("Getting Coupon List");
            var coupon = await dbContext.Coupon.ToListAsync();
            return mapper.Map<List<GetCouponDTO>>(coupon);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Coupon by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetCouponDTO>> GetById(int id)
        {
            var coupon = await dbContext.Coupon.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetCouponDTO>(coupon);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Coupon.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new coupon follow this strcture
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "couponname": "string",
        ///         "email": "coupon@example.com",
        ///         "password": "string",
        ///         "role": "string"
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] CouponDTO couponDTO)
        {
            var coupon = mapper.Map<Coupon>(couponDTO);
            dbContext.Add(coupon);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Coupon.
        /// </summary>
        /// <returns>A newly created Coupon</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a coupon follow this strcture, and specify id
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "couponname": "string",
        ///         "email": "coupon@example.com",
        ///         "password": "string",
        ///         "role": "coupon or admin"
        ///     }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutCoupon(CouponDTO couponDTO, [FromRoute] int id)
        {
            var exists = await dbContext.Coupon.AnyAsync(x => x.Id == id);
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
            var coupon = mapper.Map<Coupon>(couponDTO);
            coupon.Id = id;
            dbContext.Update(coupon);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Coupon by Id.
        /// </summary>
        /// 


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Coupon.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Coupon()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
