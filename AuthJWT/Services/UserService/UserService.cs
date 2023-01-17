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
            Id = 1, FirstName = "moein", LastName = "fazeli", Username = "admin", Password = "1234",
            Role = "Admin"
        },
        new User
        {
            Id = 2, FirstName = "hassan", LastName = "saeedi", Username = "regularUser", Password = "1234",
            Role = "User"
        }
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
        return _users;
    }
}