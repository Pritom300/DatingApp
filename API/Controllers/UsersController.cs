using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using DatingApp.API.Data;
using DatingApp.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task <ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users= await _context.Users.ToListAsync();
            return users;
        }



        [Authorize]
        //api/users/3
        [HttpGet("{id}")]
        public async Task <ActionResult<AppUser>> GetUser(int id)
        {
            var user= await _context.Users.FindAsync(id);
            return user;
        }
    }
}