using AuthJWT.Entities;

namespace AuthJWT.Services.UserService;

public interface IUserService {
    Task<User> Authentication(string userName, string password);
    Task<IEnumerable<User>> GetAll();
}