using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMatrix.Data;
using TheMatrix.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using TheMatrix.Models;

namespace TheMatrix.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _conf;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration conf, IMapper mapper)
        {
            _repo = repo;
            _conf = conf;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ModelState", "ModelState invalid");
                return BadRequest(ModelState);
            }
            // Make user name lower case
            user.UserName = user.UserName.ToLower();

            // If duplicate user name return bad request here
            if (await _repo.UserExists(user.UserName))
            {
                ModelState.AddModelError("UserName", "User name already exists");
                return BadRequest(ModelState);
            }

            // Need method in AuthRepo to test for this

            var newUser = await _repo.Register(_mapper.Map<RegisterUserDTO, User>(user), user.Password);
            // Temporary return result for testing
            return Created("api/auth/register", _mapper.Map<User, DetailUserDTO>(newUser));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {
            var storedUser = await _repo.Login(user.UserName, user.Password);
            if (storedUser == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_conf.GetSection("TokenSettings:JWTKey").Value);

            // Create Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, storedUser.ID.ToString()),
                    new Claim(ClaimTypes.Name, storedUser.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Temporary return value for testing
            return Ok(new { TokenString = tokenString, user = new { ID = storedUser.ID, Name = storedUser.UserName } });
        }
    }
}