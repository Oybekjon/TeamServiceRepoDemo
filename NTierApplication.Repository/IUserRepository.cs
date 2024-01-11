using NTierApplication.DataAccess.Models;

namespace NTierApplication.Repository;

public interface IUserRepository
{
    void Insert(User user);
    IQueryable<User> GetAll();
    int SaveChanges();
}
