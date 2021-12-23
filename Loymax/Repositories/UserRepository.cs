using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Loymax.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User Create(User user)
        {
            using (var db = new LoymaxContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user;
            }
        }

        public User GetUser(int id)
        {
            using (var db = new LoymaxContext())
            {
                var user = db.Users.Single(x => x.Id == id);
                return user;
            }
        }

        public decimal AddMoney(User user, decimal count)
        {
            using (var db = new LoymaxContext())
            {
                user = db.Users.Single(x => x.Id == user.Id);
                user.Balance = +count;
                try
                {
                    db.SaveChanges();
                }
                catch(DbUpdateConcurrencyException exception)
                {
                    throw new Exception("");
                }

                return user.Balance;
            }
        }

        public decimal? DeleteMoney(User user, decimal count)
        {
            decimal? newBalance = null;
            using (var db = new LoymaxContext())
            {
                user = db.Users.Single(x => x.Id == user.Id);
                if (user.Balance > count)
                {
                    user.Balance = -count;
                    db.SaveChanges();
                    newBalance = user.Balance;
                }

                return newBalance;
            }
        }
    }
}
