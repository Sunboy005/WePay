using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wepay.Models.DTOs;
using wepay.Models;
using AutoMapper;

using wepay.Service.Interface;

namespace wepay.Service;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<User> _signInManager;
    

    private User _user;

    public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
        _signInManager = signInManager;        
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<bool> LoginUser(UserForLoginDto userForLoginDto)
    {
        _user = await _userManager.FindByEmailAsync(userForLoginDto.Email);
        if (_user == null)
        {
            return false;
        }

        var passwordCorrect = await _userManager.CheckPasswordAsync(_user, userForLoginDto.Password);

        if (!passwordCorrect)
        {
            return false;
        }
        return true;
    }

    public async Task<IdentityResult> ChangePassword(string email, string newPassword)
    {
        var user = await  _userManager.FindByEmailAsync(email);
        var hasedPassword = _userManager.PasswordHasher.HashPassword(user, newPassword);
        if (hasedPassword == null)
        {
            return IdentityResult.Failed();
        }

        user.PasswordHash = hasedPassword;
        var result = await _userManager.UpdateAsync(user);
        return result;
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

    
    public async Task<IdentityResult> ConfirmUserEmail(UserForEmailConfirmationDto userForEmailConfirmationDto)
    {
        var user = await _userManager.FindByEmailAsync(userForEmailConfirmationDto.Email);
        if (user == null)
        {
            throw new Exception("User not Found");
        }

        user.EmailConfirmed = true;
        var result =await _userManager.UpdateAsync(user);       
        return result;
    }


}
