using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wepay.Models;
using wepay.Repository;
using wepay.Repository.Interface;
using wepay.Service.Interface;

namespace wepay.Service;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<User> _signInManager;
    private readonly IRepositoryManager _repositoryManager;

    private User _user;

    public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper, IConfiguration configuration, IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<bool> LoginUser(UserForLoginDto userForLoginDto)
    {
        _user = await _userManager.FindByEmailAsync(userForLoginDto.Email);
        if (_user == null || _user.EmailConfirmed == false)
        {
            return false;
        }
        var result = await _signInManager.PasswordSignInAsync(_user, userForLoginDto.Password, false, false);

        if (result.Succeeded == false)
        {
            return false;
        }
        return true;
    }


    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>{
            new Claim(ClaimTypes.Name, _user.UserName)
            };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private SigningCredentials GetSigningCredentials()
    {

        var key = Encoding.UTF8.GetBytes("OUR_WEPAY_VERY_SECRET_KEY_THAT_SHOULD_NOT_LEAK_OUTSIDE_THIS_ENVIRONMENT");
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
        issuer: jwtSettings["validIssuer"],
        audience: jwtSettings["validAudience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

    public async Task LogoutUser()
    {

        await _signInManager.SignOutAsync();
    }
}
