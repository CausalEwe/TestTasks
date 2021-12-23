using Loymax.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Loymax.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
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
            if (user == null) throw new Exception("invalid user");
            var newUser = _userRepository.Create(user);
            if (newUser == null) throw new Exception("invalid user");
            return newUser;
        }
        public decimal GetBalance([FromBody][Required] User user)
        {
            if (user == null) throw new Exception("invalid user");
            
            var balance = _userRepository.GetBalance(user);
            return balance;
        }
        public decimal AddMoney([FromBody][Required] User user, decimal count)
        {
            if (user == null) throw new Exception("invalid user");
            var balance = _userRepository.AddMoney(user, count);
            return balance;
        }
        public decimal DeleteMoney([FromBody][Required] User user, decimal count)
        {
            if (user == null) throw new Exception("invalid user");
            var balance = _userRepository.DeleteMoney(user, count);
            return balance;
        }
    }
}
