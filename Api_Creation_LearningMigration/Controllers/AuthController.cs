using Api_Creation_LearningMigration.Context;
using Api_Creation_LearningMigration.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Api_Creation_LearningMigration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        
        private readonly ApplicationDbContext _context;
        private IConfiguration _config;
        private readonly TokenHelper _tokenHelper;
        public AuthController(ApplicationDbContext applicationDbContext,IConfiguration config, TokenHelper tokenHelper)
        {
            _context = applicationDbContext;
            _config = config;
            _tokenHelper = tokenHelper;
         }
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            if (login == null)
            {
                return BadRequest("Fields Cannot be Empty");
            }
            var user = _context.RegisterTbl.FirstOrDefault(u => u.Email == login.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email");
            }

            // Compare the provided password with the stored password
            if (user.Password != login.Password)
            {
                return Unauthorized("Invalid password");
            }
            try
            {
                // Generate JWT token
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    null,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                var loginDtata = new Login
                {
                    LoginTime = DateTime.Now,
                    Email = login.Email,
                    Password = login.Password,
                    token = tokenString
                };
                _context.LoginTbl.Add(loginDtata);
                _context.SaveChanges();
                return Ok(new { Token = tokenString });
            }
            catch(Exception ex)
            {
             
                throw new Exception("Exception Message : ", ex);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<Register>> Register(Register register)
        {
            if (register == null)
            {
                return BadRequest("Fields Cannot be Empty");
            }
            _context.RegisterTbl.Add(register);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("registerSp")]
        public async Task<ActionResult<Register>> RegisterSP([FromBody] Register register)
        {
            if (register == null)
            {
                return BadRequest("Fields Cannot be Empty");
            }

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddRecordRegister @Name = {0}, @Email = {1}, @Password = {2}, @Contact = {3}",
                register.Name, register.Email, register.Password, register.Contact
            );

            //        await _context.Database.ExecuteSqlInterpolatedAsync(
            //    $"EXEC AddRecordRegister @Name = {register.Name}, @Email = {register.Email}, @Password = {register.Password}, @Contact = {register.Contact}"
            //);

            return Ok();
        }


       
    }
}
