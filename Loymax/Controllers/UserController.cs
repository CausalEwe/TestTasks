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
        private const string busyAddMessage = "В данный момент пополнение невозможно, счет находится в обработке";
        private const string busyDeleteMessage = "В данный момент невозможно списать средства, счет находится в обработке";
        private const string notAmountMessage = "На вашем счету не хватает средств. Ваш баланс {0}";
        private const string successfulAddMessage = "Вы пополнили баланс. Сейчас на счету {0} рублей";
        private const string successfulDeleteMessage = "Списание средств прошло успешно. Сейчас на счету {0} рублей";

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
            var user = GetUser(id);

            return user.Balance;
        }

        [HttpGet]
        [Route("getUser/{id}")]
        public User GetUser(int id)
        {
            try
            {
                var user = _userRepository.GetUser(id);
                return user;
            }
            catch
            {
                throw new Exception("user not found");
            }
        }

        [HttpPut]
        [Route("addMoney/{id}")]
        public string AddMoney(int id, decimal count)
        {
            var user = GetUser(id);
            var status = _userRepository.AddMoney(user, count);
            if (status == BalanceStatus.Busy) return busyAddMessage;
            var balance = GetBalance(id);

            return String.Format(successfulAddMessage, balance);
        }

        [HttpPut]
        [Route("deleteMoney")]
        public string DeleteMoney(int id, decimal count)
        {
            var user = GetUser(id);
            var status = _userRepository.DeleteMoney(user, count);
            if (status == BalanceStatus.Busy) return busyDeleteMessage;
            var balance = GetBalance(id);
            if (status == BalanceStatus.NotEnought) return String.Format(notAmountMessage, balance);

            return String.Format(successfulDeleteMessage, balance);
        }
    }
}
