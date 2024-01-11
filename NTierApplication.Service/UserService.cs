using NTierApplication.DataAccess.Models;
using NTierApplication.Repository;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public class UserService : IUserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<User> Login(LogInViewModel lUserModel)
    { 
        var users = _userRepository.GetAll();

        return users.Where(x => x.Email == lUserModel.Email).ToList();
    }

    public bool Register(RegisterUserViewModel rUserModel)
    {
        var newUserEntity = new User()
        {
            FirstName = rUserModel.FirstName,
            LastName = rUserModel.LastName,
            Email = rUserModel.Email,
            Password = rUserModel.Password
        };

        _userRepository.Insert(newUserEntity);
        var amount = _userRepository.SaveChanges();
        return amount > 0;

    }
}
