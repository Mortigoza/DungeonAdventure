namespace DungeonAdventure
{
    public interface IUserRepository
    {
        void Register(User user);
        User GetUser(string username);
        bool IsUserRegistered(string username);
    }
}