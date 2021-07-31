using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using API.Controllers;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{

    [Authorize]
    public class UsersController : BaseApiController
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;

            _userRepository = userRepository;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            //var users = await _userRepository.GetUsersAsync();

           // var usersToReturn=_mapper.Map<IEnumerable<MemberDto>>(users);

            //return Ok(usersToReturn);

           var users=await _userRepository.GetMembersAsync();
           return Ok(users);
        }




        //api/users/3
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
             return await _userRepository.GetMemberAsync(username);
        }




        [HttpPut]
        public async Task <ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
             var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             var user = await _userRepository.GetUserByUsernameAsync(username);

             _mapper.Map(memberUpdateDto, user);  // user.City = memberUpdateDto.City (we donn't need to do that if we use AutoMapper)

            _userRepository.Update(user);

             if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}