using AirMAY.Domain.Models;
using AirMAY.Domain.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirMAY.Services
{
    public class LoginService
    {
        public User User { get; set; }
        private readonly UserRepository _userRepositor;
        public LoginService(UserRepository userRepository)
        {
            _userRepositor = userRepository;
        }

        public async Task<bool> IsLoginInAsync(string login, string password)
        {
            return (await _userRepositor.FindByConditionAsync(x => x.Login == login && x.Password == password)).Any();
        }
        public async Task RegisterIn(User user)
        {
            await _userRepositor.Add(user);
        }

        public async Task<User> GetUser(string login)
        {
            return (await _userRepositor.FindByConditionAsync(x => x.Login == login)).FirstOrDefault();
        }
    }
}
