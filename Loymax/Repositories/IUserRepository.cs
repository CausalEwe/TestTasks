namespace Loymax.Repositories
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetUser(int id);
        BalanceStatus AddMoney(User user, decimal count);
        BalanceStatus DeleteMoney(User user, decimal count);
    }
}
