using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PureStore.Application.DTOs.Identity;
using PureStore.Application.Interfaces.Identity;
using PureStore.Domain.Common;
using PureStore.Persistence.Identity.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace PureStore.Persistence.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async ValueTask<AuthResponse> AuthenticateAsync(Auth auth)
    {
        var user = await _userManager.FindByNameAsync(auth.Username);
        var authResult = await _signInManager.PasswordSignInAsync(user, auth.Password, false, false);
        if (authResult.Succeeded)
        {
            var token = await JwtSecurityTokenAsync(user);
            return new()
            {
                Username = user.UserName,
                Email = user.Email,
                Token = token,
            };
        }

        return default;
    }

    public ValueTask<Response<string>> GetApiKeyAsync(Auth auth)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<AuthResponse> RegisterAsync(RegisterDTO user)
    {
        if (!await ValidateUserAsync(user))
            throw new KeyNotFoundException("Email or Username already taken");

        var registration = await _userManager.CreateAsync(user, user.Password);
        if (registration.Succeeded)
        {
            var appUser = await _userManager.FindByNameAsync(user.Username);
            if (appUser == null) throw new KeyNotFoundException("User not found please retry the registration process");

            return new()
            {
                Username = appUser.UserName,
                Email = appUser.Email,
                Token = await JwtSecurityTokenAsync(appUser)
            };
        }

        throw new OperationCanceledException("Registration failed check the credentials and info provided");
    }


    private async ValueTask<bool> ValidateUserAsync(RegisterDTO auth)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(auth.Email);
        var userWithSameUsername = await _userManager.FindByNameAsync(auth.Username);

        if (userWithSameEmail != null && userWithSameUsername != null)
            return false;

        if (userWithSameEmail != null || userWithSameUsername != null)
            return false;

        return true;
    }

    private async Task<string> JwtSecurityTokenAsync(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", roles[i]));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id.ToString()),
            new Claim("passwod", user.PasswordHash),
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration["JWTSettings:Issuer"],
            audience: _configuration["JWTSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWTSettings:DurationInMinutes"])),
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }
}
