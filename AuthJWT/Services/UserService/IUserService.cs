using AuthJWT.Entities;

namespace AuthJWT.Services.UserService;

public interface IUserService {
    User Authentication(string userName, string password);
    IEnumerable<User> GetAll();
}