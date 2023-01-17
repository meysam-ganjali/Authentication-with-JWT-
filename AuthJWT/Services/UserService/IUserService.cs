namespace AuthJWT.Services.UserService;

public interface IUserService {
    Task<User> AuthenticationAsync(string userName, string password);
    Task<IEnumerable<User>> GetAllAsync();
}

public class UserService : IUserService {

}