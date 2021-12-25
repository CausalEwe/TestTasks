﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Loymax.Repositories
{
    public enum BalanceStatus
    {
        Ok = 0,
        Busy = 1,
        NotEnought = 2,
    }
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

        public BalanceStatus AddMoney(User user, decimal count)
        {
            using (var db = new LoymaxContext())
            {
                user = db.Users.Single(x => x.Id == user.Id);
                user.Balance = +count;
                try
                {
                    db.SaveChanges();
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    return BalanceStatus.Busy;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return BalanceStatus.Ok;
            }
        }

        public BalanceStatus DeleteMoney(User user, decimal count)
        {
            using (var db = new LoymaxContext())
            {
                user = db.Users.Single(x => x.Id == user.Id);
                if (user.Balance > count)
                {
                    try
                    {
                        user.Balance = -count;
                        if (user.Balance < 0) return BalanceStatus.NotEnought;
                        db.SaveChanges();
                        return BalanceStatus.Ok;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        return BalanceStatus.Busy;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return BalanceStatus.NotEnought;
            }
        }
    }
}
