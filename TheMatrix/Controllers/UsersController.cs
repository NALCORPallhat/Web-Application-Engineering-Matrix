using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TheMatrix.Data;
using TheMatrix.DTOs;
using TheMatrix.Models;

namespace TheMatrix.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    // [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _conf;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repo, IConfiguration conf, IMapper mapper)
        {
            _repo = repo;
            _conf = conf;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BriefUserDTO>> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usersBriefData = _mapper.Map<IEnumerable<BriefUserDTO>>(users);
            return usersBriefData;
        }

        [HttpGet("getuser")]
        public async Task<DetailUserDTO> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var userDetails = _mapper.Map<DetailUserDTO>(user);
            return userDetails;
        }

        [HttpGet("getusernotdto")]
        public async Task<User> GetUserNotDTO(int id)
        {
            return await _repo.GetUser(id);
        }
    }
}