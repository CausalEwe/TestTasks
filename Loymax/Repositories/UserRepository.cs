namespace Loymax.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public User Create(User user)
        {
            var query = "insert into Users(Name, LastName, MiddleName, BirthDay) values(user.Name, user.LastName, user.MiddleName, user.BirthDay)";
            return user;
        }
        public decimal GetBalance(User user)
        {
            decimal balance = 0;
            var query = "select Amount from Users where Name = User.Name and LastName = user.LastName";
            return balance;
        }
        public decimal AddMoney(User user, decimal count)
        {
            decimal amount = 0;

            var query = "update Amount from Users Set Amount + count where Name = User.Name and LastName = user.LastName";
            return amount;
        }
        public decimal DeleteMoney(User user, decimal count)
        {
            decimal amount = 0;
            var balance = GetBalance(user);
            var query = "query";
            if (balance > count) query = "update Amount from Users Set Amount - count where Name = User.Name and LastName = user.LastName";
            return amount;
        }
    }
}
