using AuthJWT.Entities;

namespace AuthJWT.Services.UserService;

public class UserService : IUserService {
    private readonly List<User> _users = new List<User>
    {
        new User
        {
            Id = 1, FirstName = "Meysam", LastName = "Ganjali", Username = "admin", Password = "1234",
            Role = "Admin"
        },
        new User
        {
            Id = 2, FirstName = "Farahnaz", LastName = "Gholami", Username = "regularUser1", Password = "1234",
            Role = "User"
        },
        new User
        {
            Id = 2, FirstName = "Ali Ghorban", LastName = "Ganjali", Username = "regularUser2", Password = "1234",
            Role = "User"
        },
        new User
        {
            Id = 2, FirstName = "Mahsa", LastName = "Ganjali", Username = "asmin2", Password = "1234",
            Role = "Admin"
        },
    };
    public Task<User> AuthenticationAsync(string userName, string password) {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync() {
        throw new NotImplementedException();
    }
}