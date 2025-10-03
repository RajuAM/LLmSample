using PlacementLMS.DTOs;
using PlacementLMS.Models;

namespace PlacementLMS.Services
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(RegisterDto registerDto);
        Task<string> AuthenticateUserAsync(LoginDto loginDto);
    }
}