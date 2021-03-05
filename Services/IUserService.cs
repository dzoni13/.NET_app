using IdeaTrading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaTrading.Services
{
    interface IUserService
    {
        IEnumerable<User> GetUsers();
        IEnumerable<object> GetCities();
        User GetUser(int id);
        int CreateUser(User user);
        void EditUser(User user);
        void DeleteUser(int id);
    }
}
