using NTierApplication.DataAccess.Models;
using NTierApplication.Errors;
using NTierApplication.Repository;
using NTierApplication.Service.Helpers;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private ITokenService _tokenSerivce;

    public UserService(IUserRepository userRepository, ITokenService tokenSerivce)
    {
        _userRepository = userRepository;
        _tokenSerivce = tokenSerivce;
    }

    public ResponseModelLogin Login(LogInViewModel lUserModel)
    {

        if (lUserModel == null)
        {
            throw new ArgumentNullException(nameof(lUserModel));
        }

        var users = _userRepository.GetAll();
        var check = false;
        string token = null;
       

        var entityUser =  users.Where(x => x.Email == lUserModel.UserName).FirstOrDefault();

        if (entityUser == null)
            throw new EntryNotFoundException(nameof(entityUser));
        
        check = BCrypt.Net.BCrypt.Verify(lUserModel.Password, entityUser.Password);

        if (check)
        {
            token = _tokenSerivce.GenerateToken(entityUser);
        }

        return new ResponseModelLogin()
        {
            AccessToken = token,
            TokenType = "Bearer",
            Expires = 86400
        };

    }

    public bool Register(RegisterUserViewModel rUserModel)
    {

        rUserModel.Password
           = BCrypt.Net.BCrypt.HashPassword(rUserModel.Password);

        var newUserEntity = new User()
        {
            FirstName = rUserModel.FirstName,
            LastName = rUserModel.LastName,
            Email = rUserModel.Email,
            Password = rUserModel.Password
        };

        var users = _userRepository.GetAll();

        var res = users.Select(x => x.Email).Any(x => x == rUserModel.Email);
        if(!res)
        {
            _userRepository.Insert(newUserEntity);
        }
        
        var amount = _userRepository.SaveChanges();
        return amount > 0;

    }
}
