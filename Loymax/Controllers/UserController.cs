using Loymax.Repositories;
using System;
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
        public string AddMoney(int id, decimal count)
        {
            var user = _userRepository.GetUser(id);
            if (user == null) throw new Exception("user not found");
            var status = _userRepository.AddMoney(user, count);
            if (status == BalanceStatus.Busy)
            {
                return "В данный момент пополнение невозможно, счет находится в обработке";
            }
            var balance = GetBalance(id);
            return $"Вы пополнили баланс. Сейчас на счету {balance} рублей";
        }
        [HttpPut]
        [Route("deleteMoney")]
        public string DeleteMoney(int id, decimal count)
        {
            var user = _userRepository.GetUser(id);
            if (user == null) throw new Exception("user not found");
            var status = _userRepository.DeleteMoney(user, count);
            if (status == BalanceStatus.Busy)
            {
                return "В данный момент невозможно списать средства, счет находится в обработке";
            }
            var balance = GetBalance(id);
            if (status == BalanceStatus.NotEnought)
            {
                return $"На вашем счету не хватает средств. Ваш баланс {balance}";
            }
            return $"Списание средств прошло успешно. Сейчас на счету {balance} рублей";
        }
    }
}
