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

        private void UpdateBalanceMultiThreadsForBegin()
        {
            using (var db = new LoymaxContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                var newUsers = new List<User>(50);
                for (var i = 0; i < 50; i++)
                {
                    newUsers.Add(new User { Name = "test", LastName = "TestTest", BirthDay = new DateTime(1997, 1, 1), MiddleName = "testMiddleName", Balance = rnd.Next(0, 1000) });
                }
                db.AddRange(newUsers);
                db.SaveChanges();
            }
        }

        private List<UserController> GetUC(int count)
        {
            var repository = new UserRepository();
            var listUC = new List<UserController>(count);
            for (var i = 0; i < count; i++)
            {
                listUC.Add(new UserController(repository));
            }

            return listUC;
        }

        [Fact]
        public void UpdateBalanceMultiThreads()
        {
            List<decimal> balanceAfter;
            var listUC = GetUC(10);

            UpdateBalanceMultiThreadsForBegin();

            var result = Parallel.ForEach(
                from a in listUC
                from b in Enumerable.Range(1, 50)
                from c in Enumerable.Range(1, 2) //1 - добавить 2 - снять
                select (a, b, c), (x) => ChangeBalanceAmount(x.a, x.b, x.c));
            
            using (var db = new LoymaxContext())
            {
                balanceAfter = db.Users.Select(x => x.Balance).ToList();
                foreach (var balance in balanceAfter)
                {
                    Assert.True(balance >= 0);
                }
            }
        }

        private static void ChangeBalanceAmount(UserController controller, int id, int actionId)
        {
            var value = rnd.Next(1, 1000);
            if (actionId == 1)
            {
                controller.AddMoney(id, value);
            }
            else
            { 
                controller.DeleteMoney(id, value); 
            } 
        }

        
    }
}
