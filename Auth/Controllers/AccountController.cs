using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Data.Context;
using Vacation.API.Controllers;
using Auth.DTOs;
using Microsoft.EntityFrameworkCore;
using Auth.EventBus;

namespace Auth.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AuthDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEventBusHanlder _eventBus;

        public AccountController(AuthDbContext context, IConfiguration configuration, IEventBusHanlder eventBus)
        {
            _context = context;
            _configuration = configuration;
            _eventBus = eventBus;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login_Request request)
        {

            var user = await _context.Users
                .Where(x => x.Username == request.Username && x.Password == request.Password)
                .SingleOrDefaultAsync();

            if (user is null)
                return BadRequest(new {Error="Invalid Username Or Password!"});

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()), 
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            _eventBus.Publish(new NotificationEvent() { Message = $"Dear {user.Username} Welcome To Auth Site." });

            return Ok(new { token = tokenHandler.WriteToken(token) }); 

        }
    }
}
