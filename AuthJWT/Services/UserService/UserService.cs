using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthJWT.Config;
using AuthJWT.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthJWT.Services.UserService;

public class UserService : IUserService {
    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings) {
        _appSettings = appSettings.Value;
    }
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
    public User Authentication(string userName, string password) {
        var user = _users.FirstOrDefault(x => x.Username == userName && x.Password == password);

        // return null if user not found
        if (user == null) {
            return null;
        }

        // authentication successful so generate jwt token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var claims = new ClaimsIdentity();
        claims.AddClaims(new[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString())
        });

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = claims,
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);

        // remove password before returning
        user.Password = null;

        return user;
    }

    public IEnumerable<User> GetAll() {
        return _users.ToList();
    }
}