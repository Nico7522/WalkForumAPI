
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Abstract;

public interface IRefreshTokenGenerator
{

     string GenerateRefreshToken();

}
