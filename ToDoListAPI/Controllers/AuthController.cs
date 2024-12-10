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
        public IActionResult Register(User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
                return BadRequest("Username already exists.");

            // Hash the password
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (dbUser == null)
                return Unauthorized("Invalid credentials.");

            // Verify the hashed password
            var result = _passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, user.Password);
            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Invalid credentials.");

            // Generate token
            var token = _authService.GenerateToken(dbUser.Id, dbUser.Username);
            return Ok(new { Token = token });
        }
    }
}
