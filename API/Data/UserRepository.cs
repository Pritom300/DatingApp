using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.Data;
using DatingApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
             _mapper = mapper;
            _context = context;
        }

       //Optimal Solution Automapper Start find by username
        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
            .Where(x => x.UserName == username)

            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

      

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
            .ProjectTo <MemberDto> (_mapper.ConfigurationProvider)
            .ToListAsync();
        }

          //Optimal Solution Of Automapper End find all of users



          //......................................................//




        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
           
         //normal automapper process find by username  
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }
        

        //normal automapper process find all of users
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}