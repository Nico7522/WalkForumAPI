
using System.Security.Cryptography;
using WalkForum.Application.Abstract;
using WalkForum.Domain.Entities;

namespace WalkForum.Infrastructure.Security;

internal class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string GenerateRefreshToken()
    {
       var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }


}
