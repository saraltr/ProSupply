using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using CSE_325_group_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;
using CSE_325_group_project.Data;

namespace CSE_325_group_project.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        // Constructor injection for DbContext and Logger
        public AuthController(AppDbContext dbContext, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
        {
            var identifier = request.Username?.Trim();
            if (string.IsNullOrEmpty(identifier))
            {
                identifier = request.Email?.Trim();
            }
            // Console.WriteLine($"Received Login Identifier: '{identifier}'");

            _logger.LogInformation("Attempting to sign in user: {Identifier}", identifier);

            if (string.IsNullOrEmpty(identifier))
            {
                return BadRequest(new { message = "Username or Email is required." });
            }

            var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower() == identifier.ToLower() || u.UserEmail.ToLower() == identifier.ToLower());

            // Console.WriteLine($"User found: {user?.Username}");

            if (user == null)
            {
                return BadRequest(new { message = "Invalid username or email." });
            }

            // Verify the password hash
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.UserPassword, request.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return BadRequest(new { message = "Invalid password." });
            }

            // Log successful login
            _logger.LogInformation("User {Username} logged in successfully.", request.Username);

            string role = GetRole(user.UserLevel);

            // Retrieve the JWT settings from configuration
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            // Check if the JWT key is available
            if (string.IsNullOrEmpty(jwtKey))
            {
                _logger.LogError("JWT Key is not configured properly.");
                return StatusCode(500, new { message = "Internal server error. JWT_KEY is missing." });
            }

            // generate JWT token for the authenticated user
            var claims = new[]
            {
                // claims to store in the payload
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.UserEmail),
                new Claim("UserId", user.User_id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            // creates a symmetric security key using the key from the app settings
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            // signing credentials using the symmetric key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // create the JWT token using the claims, issuer, audience, and signing credentials
            var token = new JwtSecurityToken(
                issuer: "prosupply", 
                audience: "prosupply",
                claims: claims,
                expires: DateTime.Now.AddHours(2), // set the token to expire in 2 hours
                signingCredentials: creds
            );

            // serialize the token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // return the token to the client
            return Ok(new { token = jwt });
        }

        // conver the user_levels as roles
        private string GetRole(int level)
        {
            return level switch
            {
                1 => "User",
                2 => "Manager",
                3 => "Supplier",
                4 => "Admin",
                _ => "User"
            };
        }

        //endpoint test
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API is working");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}
