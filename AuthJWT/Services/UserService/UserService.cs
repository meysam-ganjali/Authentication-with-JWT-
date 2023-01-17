using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthJWT.Config;
using AuthJWT.Data;
using AuthJWT.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthJWT.Services.UserService;

public class UserService : IUserService {
    private readonly AppSettings _appSettings;
    private readonly DatabaseContext _db;
    public UserService(IOptions<AppSettings> appSettings,DatabaseContext db) {
        _appSettings = appSettings.Value;
        _db = db;
    }

    public async Task<User> Authentication(string userName, string password)
    {

        var user = await _db.Users.FirstOrDefaultAsync(x => x.Username.Equals(userName) && x.Password.Equals(password));

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
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role,user.Role),
        });

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = claims,
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);
        await _db.SaveChangesAsync();
        // remove password before returning
        user.Password = null;

        return user;
    }

    public async Task<IEnumerable<User>> GetAll() {
        return await _db.Users.ToListAsync();
    }
}