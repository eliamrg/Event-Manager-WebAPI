using AutoMapper;
using Event_manager_API.DTOs.Set;
using Event_manager_API.Entities;
using Event_manager_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<UserController> logger;
        private readonly IWebHostEnvironment env;
        private readonly Mapper mapper;
        public UserController(
                    ApplicationDbContext context,
                    IService service,
                    ServiceTransient serviceTransient,
                    ServiceScoped serviceScoped,
                    ServiceSingleton serviceSingleton,
                    ILogger<UserController> logger,
                    IWebHostEnvironment env,
                    Mapper mapper
               )
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
            this.mapper = mapper;
        }


        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Users.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            logger.LogInformation("Getting User List");
            return await dbContext.User.ToListAsync();
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get User by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            return await dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a User.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>A newly created User</returns>
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
            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == celular.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }
            */
            var user=mapper.Map<User>(userDTO);
            dbContext.Add(user);

            await dbContext.SaveChangesAsync();

            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a User.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
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
        ///         "role": "string"
        ///     }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(User user, int id)
        {
            if (user.Id != id)
            {
                return BadRequest("The Id does not match the one established in the URL.");
            }

            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == event_.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }*/

            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete User by Id.
        /// </summary>

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.User.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("Not found in the database");
            }
            dbContext.Remove(new User()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}