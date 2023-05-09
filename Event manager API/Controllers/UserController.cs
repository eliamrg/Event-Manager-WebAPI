using AutoMapper;
using Event_manager_API.DTOs.Get;
using Event_manager_API.DTOs.Set;
using Event_manager_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<UserController> logger;
        private readonly IMapper mapper;
        public UserController(
                    ApplicationDbContext context,
                    ILogger<UserController> logger,
                    IMapper mapper
               )
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Users.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetUserDTO>>> GetAll()
        {
            logger.LogInformation("Getting User List");
            var user=await dbContext.User.ToListAsync();
            return mapper.Map<List<GetUserDTO>>(user);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get User by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetUserDTO>> GetById(int id)
        {
            var user = await dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetUserDTO>(user);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new user follow this strcture
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "username": "string",
        ///         "email": "user@example.com",
        ///         "password": "string",
        ///         "role": "string"
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] UserDTO userDTO)
        {
            var user = mapper.Map<User>(userDTO);
            dbContext.Add(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a User.
        /// </summary>
        /// <returns>A newly created User</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a user follow this strcture, and specify id
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "username": "string",
        ///         "email": "user@example.com",
        ///         "password": "string",
        ///         "role": "user or admin"
        ///     }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutUser ( UserDTO userDTO, [FromRoute]int id)
        {
            var exists = await dbContext.User.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("Does not exist");
            }

            var user = mapper.Map<User>(userDTO);
            user.Id = id;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete User by Id.
        /// </summary>
        /// 


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.User.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new User()
                { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }

       
    }
}