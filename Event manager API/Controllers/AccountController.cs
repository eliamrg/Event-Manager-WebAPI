using Azure.Identity;
using Event_manager_API.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        

        public AccountController(
                UserManager<IdentityUser> userManager, 
                IConfiguration configuration, 
                SignInManager<IdentityUser> signInManager,
                 ApplicationDbContext dbContext
            )
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.dbContext= dbContext;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(UserCredentials credentials)
        {
            var user = new IdentityUser { UserName = credentials.Email, Email = credentials.Email };
            var result = await userManager.CreateAsync(user, credentials.Password);

            if (result.Succeeded)
            {
                return await BuildToken(credentials);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

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

        [HttpPost("MakeAdmin")]
        public async Task<ActionResult<AuthenticationResponse>> Admin(EditAdmin editAdmin)
        {
            var user = await userManager.FindByEmailAsync(editAdmin.Email);
            await userManager.AddClaimAsync(user, new Claim("IsAdmin", "1"));
            return NoContent();
        }
        [HttpPost("RemoveAdmin")]
        public async Task<ActionResult<AuthenticationResponse>> RemoveAdmin(EditAdmin editAdmin)
        {
            var user = await userManager.FindByEmailAsync(editAdmin.Email);
            await userManager.RemoveClaimAsync(user, new Claim("IsAdmin", "1"));
            return NoContent();
        }

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
