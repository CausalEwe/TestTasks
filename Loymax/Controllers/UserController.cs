using Loymax.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Loymax.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userReposity)
        {
            _userRepository = userReposity;
        }
        [HttpPost]
        [Route("register")] 
        public User Register ([FromBody]User user)
        {
            var newUser = _userRepository.Create(user);
            return newUser;
        }
        [HttpGet]
        [Route("getBalance/{id}")]
        public decimal GetBalance(int id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null) throw new Exception("user not found");
            return user.Balance;
        }
        [HttpPut]
        [Route("addMoney/{id}")]
        public decimal AddMoney(int id, decimal count)
        {
            var user = _userRepository.GetUser(id);
            if (user == null) throw new Exception("user not found");
            var newBalance = _userRepository.AddMoney(user, count);
            return newBalance;
        }
        [HttpPut]
        [Route("deleteMoney")]
        public decimal? DeleteMoney(int id, decimal count)
        {
            var user = _userRepository.GetUser(id);
            if (user == null) throw new Exception("user not found");
            var newBalance = _userRepository.DeleteMoney(user, count);
            if (newBalance == null) throw new Exception("На вашем счету недостаточно средств");
            return newBalance;
        }
    }
}
