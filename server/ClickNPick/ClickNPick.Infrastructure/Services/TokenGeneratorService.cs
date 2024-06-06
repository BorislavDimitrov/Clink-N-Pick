using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClickNPick.Infrastructure.Services;

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly string SecretKey;
    private readonly string Issuer;
    private readonly string Audience;
    private readonly UserManager<User> _userManager;

    public TokenGeneratorService(IConfiguration configuration, UserManager<User> userManager)
    {
        SecretKey =  configuration.GetValue<string>("Jwt:SecretKey");
        Issuer = configuration.GetValue<string>("Jwt:Issuer");
        Audience = configuration.GetValue<string>("Jwt:Audience");
        _userManager = userManager;
    }

    public async Task<string> GenerateToken(User user)
    {
        var credentials = GetSigningCredentials();

        var claims = await GetClaims(user);

        var token = new JwtSecurityToken(Issuer,
          Audience,
          claims,
          expires: DateTime.Now.AddDays(1),
          signingCredentials: credentials);

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return serializedToken;
    }

    private SigningCredentials GetSigningCredentials()
    {

        if (string.IsNullOrEmpty(SecretKey))
        {
            throw new Exception("jwtKey cannot be null while generating the token");
        }
        var jwtEncodedSecurityKey = Encoding.ASCII.GetBytes(SecretKey);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(jwtEncodedSecurityKey),
            SecurityAlgorithms.HmacSha256Signature);

        return credentials;
    }

    private async Task<Claim[]>  GetClaims(User user)
    {
        var claims = new List<Claim> {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),

        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {              
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        }

        return claims.ToArray();
    }   
}

