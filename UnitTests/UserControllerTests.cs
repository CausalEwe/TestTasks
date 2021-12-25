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
        private static Random rnd = new Random();
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

            var result = Parallel.ForEach(from a in new List<UserController> { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10 }
                                          from b in Enumerable.Range(1, 50)
                                          from c in Enumerable.Range(1, 2) //1 - добавить 2 - отнять
                                          select (a, b, c), (x) => ChangeBalanceAmount(x.a, x.b, x.c));
            
            using (var db = new LoymaxContext())
            {
                balanceAfter = db.Users.Select(x => x.Balance).ToList();
                foreach (var balance in balanceAfter)
                {
                    Assert.True(balance > 0);
                }
            }
        }

        private static void ChangeBalanceAmount(UserController controller, int id, int actionId)
        {
            var value = rnd.Next(1, 1000);
            if (actionId == 1) controller.AddMoney(id, value);
            else controller.DeleteMoney(id, value);
        }
    }
}
