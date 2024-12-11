using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Data;
using ToDoListAPI.Data.Entities;
using ToDoListAPI.Service;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(AppDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string password)
        {
            var user = new User();
            user.Username = username;
            user.Password = password;
            if (_context.Users.Any(u => u.Username == username))
                return BadRequest("Username already exists.");

            // Hash the password
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            //user.Password = password;
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login(string Username, string Password)
        {
            //var user = new User();
            var dbUser = _context.Users.FirstOrDefault(u => u.Username == Username);
            if (dbUser == null)
                return Unauthorized("Invalid credentials.");

            // Verify the hashed password
            var result = _passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, Password);
            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Invalid credentials.");

            // Generate token
            var token = _authService.GenerateToken(dbUser.Id, dbUser.Username);
            return Ok(new { Token = token });
        }
    }
}
