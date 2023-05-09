using AutoMapper;
using Event_manager_API.DTOs.Get;
using Event_manager_API.DTOs.Set;
using Event_manager_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Follow")]
    public class FollowController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<FollowController> logger;
        private readonly IMapper mapper;
        public FollowController(
                    ApplicationDbContext context,
                    ILogger<FollowController> logger,
                    IMapper mapper
               )
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Follows.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetFollowDTO>>> GetAll()
        {
            logger.LogInformation("Getting Follow List");
            var follow = await dbContext.Follow.ToListAsync();
            return mapper.Map<List<GetFollowDTO>>(follow);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Follow by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetFollowDTO>> GetById(int id)
        {
            var follow = await dbContext.Follow.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetFollowDTO>(follow);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Follow.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new follow follow this strcture
        ///     {
        ///         "createdAt": "2023-05-09T03:05:01.100Z",
        ///         "userId": 0,
        ///         "adminId": 0
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] FollowDTO followDTO)
        {
            
            var userExists = await dbContext.User.AnyAsync(x => x.Id == followDTO.UserId);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }

            var adminExists = await dbContext.User.AnyAsync(x => (x.Id == followDTO.AdminId && x.Role == "admin"));
            if (!adminExists)
            {
                return BadRequest("That Administrator does not exist");
            }
            var follow = mapper.Map<Follow>(followDTO);
            dbContext.Add(follow);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Follow.
        /// </summary>
        /// <returns>A newly created Follow</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a follow follow this strcture, and specify id
        ///     {
        ///         "createdAt": "2023-05-09T03:05:01.100Z",
        ///         "userId": 0,
        ///         "adminId": 0
        ///     }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutFollow(FollowDTO followDTO, [FromRoute] int id)
        {
            var exists = await dbContext.Follow.AnyAsync(x => x.Id == id);
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
            var follow = mapper.Map<Follow>(followDTO);
            follow.Id = id;
            dbContext.Update(follow);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Follow by Id.
        /// </summary>
        /// 


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Follow.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Follow()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}