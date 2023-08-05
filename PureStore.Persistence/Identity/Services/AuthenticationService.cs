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

namespace PureStore.Persistence.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JWTSettings _jwtSettings;
    private readonly Keys _keys;

    public AuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
        IOptions<JWTSettings> jwtSettings, IOptions<Keys> keys)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _keys = keys.Value;
    }

    public async ValueTask<Response<object>> AuthenticateAsync(Auth auth)
    {
        var user = await _userManager.FindByNameAsync(auth.Username);
        var authResult = await _signInManager.PasswordSignInAsync(user, auth.Password, false, false);
        if (authResult.Succeeded)
        {
            var token = await JwtSecurityTokenAsync(user);
            return new Response<object>(token, "Authenticated successfully");
        }

        return new Response<object>(default, "Failed to authenticated, password or username is incorrect");
    }

    public ValueTask<Response<string>> GetApiKeyAsync(Auth auth)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<Response<object>> RegisterAsync(RegisterDTO user)
    {
        if (!await ValidateUserAsync(user))
            return new Response<object>(user, "Email or Username already taken");

        var registration = await _userManager.CreateAsync(user, user.Password);
        if (registration.Succeeded)
        {
            var appUser = await _userManager.FindByNameAsync(user.Username);
            if (appUser == null) throw new KeyNotFoundException("User not found please retry the registration process");

            return new Response<object>(await JwtSecurityTokenAsync(appUser), "Registred successfully");
        }

        return new Response<object>(null, "Registration failed check the credentials and info provided");
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

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_keys.CryptorKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }
}
