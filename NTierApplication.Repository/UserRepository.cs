using NTierApplication.DataAccess;
using NTierApplication.DataAccess.Models;

namespace NTierApplication.Repository;

public class UserRepository : IUserRepository
{
    private MainContext DbContext;

    public UserRepository(MainContext dbContext)
    {
        DbContext = dbContext;
    }

    public IQueryable<User> GetAll()
    {
        return DbContext.Users;
    }

    public void Insert(User user)
    {
        DbContext.Users.Add(user);
    }

    public int SaveChanges()
    {
        return DbContext.SaveChanges();
    }
}
