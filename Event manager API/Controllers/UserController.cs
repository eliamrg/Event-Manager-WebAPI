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
            var userExists = await dbContext.User.AnyAsync(x => x.Id == id);
            if (!userExists)
            {
                return NotFound("That User does not exist");
            }
            var user = await dbContext.User.Include(DB => DB.Account).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetUserDTO>(user);
        }

        /// <summary>
        /// Get User's Favourites by Id.
        /// </summary>
        [HttpGet("Favourites/{Userid:int}")]
        public async Task<ActionResult<GetUserDTOwithFavourites>> GetByIdListFavourites(int Userid)
        {
            var userExists = await dbContext.User.AnyAsync(x => x.Id == Userid);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }
            var object_ = await dbContext.User
                .Include(DB => DB.Favourites)
                .FirstOrDefaultAsync(x => x.Id == Userid);
            return mapper.Map<GetUserDTOwithFavourites>(object_);
        }

        /// <summary>
        /// Get User's Followers by Id.
        /// </summary>
        [HttpGet("Followers/{Userid:int}")]
        public async Task<ActionResult<GetUserDTOwithFollowers>> GetByIdListFollowers(int Userid)
        {
            var userExists = await dbContext.User.AnyAsync(x => x.Id == Userid);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }
            var object_ = await dbContext.User
                .Include(DB => DB.Followers)
                .FirstOrDefaultAsync(x => x.Id == Userid);
            return mapper.Map<GetUserDTOwithFollowers>(object_);
        }

        /// <summary>
        /// Get User's Following by Id.
        /// </summary>
        [HttpGet("Following/{Userid:int}")]
        public async Task<ActionResult<GetUserDTOwithFollowing>> GetByIdListFollowing(int Userid)
        {
            var userExists = await dbContext.User.AnyAsync(x => x.Id == Userid);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }
            var object_ = await dbContext.User
                .Include(DB => DB.Following)
                .FirstOrDefaultAsync(x => x.Id == Userid);
            return mapper.Map<GetUserDTOwithFollowing>(object_);
        }

        /// <summary>
        /// Get User's Form Responses by Id.
        /// </summary>
        [HttpGet("Forms/{Userid:int}")]
        public async Task<ActionResult<GetUserDTOwithForms>> GetByIdListForms(int Userid)
        {
            var userExists = await dbContext.User.AnyAsync(x => x.Id == Userid);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }
            var object_ = await dbContext.User
                .Include(DB => DB.FormResponses)
                .FirstOrDefaultAsync(x => x.Id == Userid);
            return mapper.Map<GetUserDTOwithForms>(object_);
        }

        /// <summary>
        /// Get User's Tickets/Events by Id.
        /// </summary>
        [HttpGet("Tickets/{Userid:int}")]
        public async Task<ActionResult<GetUserDTOwithTickets>> GetByIdListTickets(int Userid)
        {
            var userExists = await dbContext.User.AnyAsync(x => x.Id == Userid);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }
            var object_ = await dbContext.User
                .Include(DB => DB.Tickets)
                .ThenInclude(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == Userid);
            return mapper.Map<GetUserDTOwithTickets>(object_);
        }


       



        //PATCH-----------------------------------------------------------------
        /// <summary>
        /// Update User's UserName
        /// </summary>
        /// 
        /// <remarks>
        /// Please Specify users id and new User Name
        ///
        /// </remarks>

        [HttpPatch("ChangeUserName/{Userid:int}/{UserName}")]
        public async Task<ActionResult> UpdateUserName([FromRoute]string UserName, [FromRoute] int Userid)
        {
            var exists = await dbContext.User.AnyAsync(x => x.Id == Userid);
            if (!exists)
            {
                return NotFound("Does not exist");
            }
            if (UserName==null || UserName.Length < 4)
            {
                return BadRequest();
            }
            var userDTO = await dbContext.User.FirstOrDefaultAsync(x => x.Id == Userid);
            var user = mapper.Map<ApplicationUser>(userDTO);
            user.Id = Userid;
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