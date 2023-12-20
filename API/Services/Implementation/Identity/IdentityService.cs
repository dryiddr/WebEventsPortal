using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Application.Auth;
using Application.Dtos.Auth;
using Domain.User;
using Persistence.Exceptions;
using Persistence.Infrastructure;
using Services.Interfaces.Identity;

namespace Services.Implementation.Identity;

public class IdentityService : IIdentityService
{
    private readonly IRepository<UserAuth> _userRepository;
    private readonly IHashService _hashService;

    public IdentityService(IRepository<UserAuth> userRepository, IHashService hashService)
    {
        (_userRepository) = (userRepository);
        _hashService = hashService;
    }

    public async Task<string> GetToken(AuthDto authDto)
    {
        var user = await _userRepository.Query().FirstOrDefaultAsync(auth => auth.Email == authDto.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), authDto);
        }

        //_hashService.VerifyPassword(authDto.Password, user.Password);
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Role, user.Role),
            new (ClaimTypes.Email, user.Email)
        };
        var now = DateTime.Now;
        var jwt = new JwtSecurityToken(
            issuer:AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: claims,
            expires: now.Add(TimeSpan.FromHours(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
    }
}