using Microsoft.EntityFrameworkCore;
using Project3.Data.Data;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service
{
    public class UserManagerService : IUsersManagerService
    {
        private readonly Project3DB _project3DB;
        public UserManagerService(Project3DB _project3DB)
        {
            this._project3DB = _project3DB;
        }



        public async Task<Users> GetUserByUserNameAsync(string username)
        {
            return await _project3DB.Users.Where(m => m.username == username).SingleOrDefaultAsync();
        }
        public async Task<Users> AddAsync(Users user)
        {
            return await _project3DB.AddAsync(user);
        }

        public async Task<Users> UpdateAsync(Users user)
        {
            return await _project3DB.UpdateAsync(user);
        }
    }
}
