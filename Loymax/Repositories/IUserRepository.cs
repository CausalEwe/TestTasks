namespace Loymax.Repositories
{
    public interface IUserRepository
    {
        User Create(User user);
        decimal GetBalance(User user);
        decimal AddMoney(User user, decimal count);
        decimal DeleteMoney(User user, decimal count);
    }
}
