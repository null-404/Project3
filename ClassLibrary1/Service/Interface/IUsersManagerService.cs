using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service.Interface
{
    public interface IUsersManagerService
    {
        Task<Users> AddAsync(Users user);
        Task<Users> GetUserByUserNameAsync(string username);
        Task<Users> UpdateAsync(Users user);
    }
}
