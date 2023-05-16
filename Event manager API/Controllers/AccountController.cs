using AutoMapper;
using Azure.Identity;
using Event_manager_API.DTOs.Auth;
using Event_manager_API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController:ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IMapper mapper;


        public AccountController(
                UserManager<IdentityUser> userManager, 
                IConfiguration configuration, 
                SignInManager<IdentityUser> signInManager,
                 ApplicationDbContext dbContext,
                 IMapper mapper
            )
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.dbContext= dbContext;
            this.mapper = mapper;
        }


        /// <summary>
        /// Add a User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Follow this strcture
        ///     {
        ///         "username": "string",
        ///         "email": "user@example.com",
        ///         "password": "string",
        ///         "role": "string"    user/admin
        ///     }
        ///
        /// </remarks>
        [HttpPost("Register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(RegisterUser registerUser)
        {

            var user = new IdentityUser { UserName = registerUser.Email, Email = registerUser.Email };
            var result = await userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {

                var userid = user.Id; 

                ApplicationUser userDto=new ApplicationUser();
                userDto.CreatedAt= DateTime.Now;
                userDto.Email=registerUser.Email;
                userDto.Role = registerUser.Role;
                userDto.Username=registerUser.Username;
                userDto.AccountId = userid;
                
                var applicationUser = mapper.Map<ApplicationUser>(userDto);
                dbContext.Add(applicationUser);
                await dbContext.SaveChangesAsync();


                UserCredentials userCredentials = new UserCredentials();
                userCredentials.Email=registerUser.Email;
                userCredentials.Password=registerUser.Password;

                if (userDto.Role == "admin")
                {
                    var userAdmin = await userManager.FindByEmailAsync(userDto.Email);
                    await userManager.AddClaimAsync(userAdmin, new Claim("IsAdmin", "1"));
                }

                return await BuildToken(userCredentials);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        /// <summary>
        /// SignIn.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Follow this strcture
        ///     {
        ///         "email": "user@example.com",
        ///         "password": "string",
        ///     }
        ///
        /// </remarks>
        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentials credentials)
        {

            var user=await userManager.FindByNameAsync(credentials.Email);
            var userEmail = user.Email;
            var result = await signInManager.PasswordSignInAsync(userEmail, credentials.Password, isPersistent: false, lockoutOnFailure: false);
            
            
            if (result.Succeeded)
            {
                return await BuildToken(credentials);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Make a User admin.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Follow this strcture
        ///     {
        ///         "email": "user@example.com",
        ///     }
        ///
        /// </remarks>
        [HttpPost("MakeAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<AuthenticationResponse>> Admin(EditAdmin editAdmin)
        {
            var exists = await dbContext.User.AnyAsync(x => x.Email == editAdmin.Email);
            if (!exists)
            {
                return NotFound("Does not exist");
            }

            var applicationUser = await dbContext.User.FirstOrDefaultAsync(x => x.Email==editAdmin.Email);
            applicationUser.Role = "admin";
            dbContext.Update(applicationUser);
            await dbContext.SaveChangesAsync();


            var user = await userManager.FindByEmailAsync(editAdmin.Email);
            await userManager.AddClaimAsync(user, new Claim("IsAdmin", "1"));
            return NoContent();
        }
        /// <summary>
        /// Remove a User admin.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Follow this strcture
        ///     {
        ///         "email": "user@example.com",
        ///     }
        ///
        /// </remarks>
        [HttpPost("RemoveAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<AuthenticationResponse>> RemoveAdmin(EditAdmin editAdmin)
        {

            var exists = await dbContext.User.AnyAsync(x => x.Email == editAdmin.Email);
            if (!exists)
            {
                return NotFound("Does not exist");
            }

            var applicationUser = await dbContext.User.FirstOrDefaultAsync(x => x.Email == editAdmin.Email);
            applicationUser.Role = "user";
            dbContext.Update(applicationUser);
            await dbContext.SaveChangesAsync();

            var user = await userManager.FindByEmailAsync(editAdmin.Email);
            await userManager.RemoveClaimAsync(user, new Claim("IsAdmin", "1"));
            return NoContent();
        }

        /// <summary>
        /// Renew Token.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Follow this strcture
        ///     {
        ///         "email": "user@example.com",
        ///     }
        ///
        /// </remarks>
        [HttpGet("RenewToken")]
        
        public async Task<ActionResult<AuthenticationResponse>> RenewToken(EditAdmin editAdmin)
        {

            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var credentials = new UserCredentials()
            {
                Email = email,
            };
            return await BuildToken(credentials);
        }

        private  async Task<AuthenticationResponse> BuildToken(UserCredentials credentials)
        {
            var claims = new List<Claim>
            {
                new Claim("email",credentials.Email),
               
            };
            var user = await userManager.FindByEmailAsync(credentials.Email);
            var claimsDB = await userManager.GetClaimsAsync(user);
            claims.AddRange(claimsDB);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);

            var securityToken= new JwtSecurityToken(issuer:null,audience:null,claims:claims,expires:expiration,signingCredentials:creds);
            return new AuthenticationResponse()
            {
                Token=new JwtSecurityTokenHandler().WriteToken(securityToken),
                ExpirationDate=expiration
            };
        }
    }
}
