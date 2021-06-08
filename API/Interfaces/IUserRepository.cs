using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using DatingApp.API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task <bool> SaveAllAsync();
        Task <IEnumerable<AppUser>> GetUsersAsync();  //return Appuser models collection type

        Task<AppUser> GetUserByIdAsync(int id);  //return AppUser models type
        Task <AppUser> GetUserByUsernameAsync(string username);
        Task<IEnumerable <MemberDto>> GetMembersAsync(); 
        Task <MemberDto> GetMemberAsync(string username);



    }
}