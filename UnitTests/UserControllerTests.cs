using Loymax;
using Loymax.Controllers;
using Loymax.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class UserControllerTests
    {
        private const string Conn = "server=localhost;database=users;user=root;password=root;";

        [Fact]
        public void UpdateBalanceMultiThreads()
        {
            List<decimal> balanceBefore;
            List<decimal> balanceAfter;
            var repository = new UserRepository();
            var c1 = new UserController(repository);
            var c2 = new UserController(repository);
            var c3 = new UserController(repository);
            var c4 = new UserController(repository);
            var c5 = new UserController(repository);
            var c6 = new UserController(repository);
            var c7 = new UserController(repository);
            var c8 = new UserController(repository);
            var c9 = new UserController(repository);
            var c10 = new UserController(repository);
            using (var db = new LoymaxContext())
            {
                balanceBefore = db.Users.Select(x => x.Balance).ToList();
            }

            var result = Parallel.ForEach(new List<UserController> { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10 }, ChangeBalanceAmount);
            
            using (var db = new LoymaxContext())
            {
                balanceAfter = db.Users.Select(x => x.Balance).ToList();
                Assert.NotNull(balanceAfter);
            }
        }

        private static void ChangeBalanceAmount(UserController controller)
        {
            var rnd = new Random();
            var id = 1;
            var value = rnd.Next(1, 1000);
            controller.AddMoney(id, value);
            //controller.DeleteMoney(id, value);
        }
    }
}
