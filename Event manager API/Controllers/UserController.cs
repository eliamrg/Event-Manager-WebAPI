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
    [Route("User")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
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
            var user=await dbContext.User.Include(DB => DB.Account).ToListAsync();
            return mapper.Map<List<GetUserDTO>>(user);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get User by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetUserDTO>> GetById(int id)
        {
            var user = await dbContext.User.Include(DB => DB.Account).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetUserDTO>(user);
        }

        /// <summary>
        /// Get User's Favourites by Id.
        /// </summary>
        [HttpGet("Favourites/{id:int}")]
        public async Task<ActionResult<GetUserDTOwithFavourites>> GetByIdListFavourites(int id)
        {
            var object_ = await dbContext.User
                .Include(DB => DB.Favourites)
                .FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetUserDTOwithFavourites>(object_);
        }

        /// <summary>
        /// Get User's Followers by Id.
        /// </summary>
        [HttpGet("Followers/{id:int}")]
        public async Task<ActionResult<GetUserDTOwithFollowers>> GetByIdListFollowers(int id)
        {
            var object_ = await dbContext.User
                .Include(DB => DB.Followers)
                .FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetUserDTOwithFollowers>(object_);
        }

        /// <summary>
        /// Get User's Following by Id.
        /// </summary>
        [HttpGet("Following/{id:int}")]
        public async Task<ActionResult<GetUserDTOwithFollowing>> GetByIdListFollowing(int id)
        {
            var object_ = await dbContext.User
                .Include(DB => DB.Following)
                .FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetUserDTOwithFollowing>(object_);
        }

        /// <summary>
        /// Get User's Form Responses by Id.
        /// </summary>
        [HttpGet("Forms/{id:int}")]
        public async Task<ActionResult<GetUserDTOwithForms>> GetByIdListForms(int id)
        {
            var object_ = await dbContext.User
                .Include(DB => DB.FormResponses)
                .FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetUserDTOwithForms>(object_);
        }

        /// <summary>
        /// Get User's Tickets by Id.
        /// </summary>
        [HttpGet("Tickets/{id:int}")]
        public async Task<ActionResult<GetUserDTOwithTickets>> GetByIdListTickets(int id)
        {
            var object_ = await dbContext.User
                .Include(DB => DB.Tickets)
                .FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetUserDTOwithTickets>(object_);
        }



        //PATCH-----------------------------------------------------------------
        /// <summary>
        /// Update User's UserName
        /// </summary>
        /// 
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update user follow this strcture, and specify id
        ///     {
        ///         "username": "string",
        ///     }
        ///
        /// </remarks>

        [HttpPatch("ChangeUserName/{id:int}/{UserName}")]
        public async Task<ActionResult> UpdateUserName([FromRoute]string UserName, [FromRoute] int id)
        {
            var exists = await dbContext.User.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("Does not exist");
            }
            var userDTO = await dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
            var user = mapper.Map<ApplicationUser>(userDTO);
            user.Id = id;
            user.Username=UserName;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }



        /*
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

        public async Task<ActionResult> Post([FromBody] ApplicationUserDTO userDTO)
        {
            var user = mapper.Map<ApplicationUser>(userDTO);
            dbContext.Add(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        */

        //UPDATE------------------------------------------------------------------------------------------------
        /*
                /// <summary>
                /// Update User.
                /// </summary>
                /// <returns>A newly created User</returns>
                /// <remarks>
                /// Sample request:
                ///
                ///     To Update user follow this strcture, and specify id
                ///     {
                ///         "username": "string",
                ///         "email": "user@example.com",
                ///         "password": "string",
                ///         "role": "user or admin"
                ///     }
                ///
                /// </remarks>

                [HttpPut("{id:int}")]
                public async Task<ActionResult> PutUser ( ApplicationUserDTO userDTO, [FromRoute]int id)
                {
                    var exists = await dbContext.User.AnyAsync(x => x.Id == id);
                    if (!exists)
                    {
                        return NotFound("Does not exist");
                    }

                    var user = mapper.Map<ApplicationUser>(userDTO);
                    user.Id = id;

                    dbContext.Update(user);
                    await dbContext.SaveChangesAsync();
                    return Ok();
                }*/
        /*
        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete User.
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
            dbContext.Remove(new ApplicationUser()
                { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }

       */
    }
}