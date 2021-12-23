namespace Loymax.Repositories
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetUser(int id);
        decimal AddMoney(User user, decimal count);
        decimal? DeleteMoney(User user, decimal count);
    }
}
