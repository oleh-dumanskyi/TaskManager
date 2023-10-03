using System.Security.Claims;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces
{
    public interface IUserService
    {
        Task<Guid> UserSignUp(UserDTO request, CancellationToken token);
        Task<ClaimsIdentity> UserSignIn(UserSignInDTO request, CancellationToken token);
        Task<Guid> EditUser(Guid id, UserDTO request, CancellationToken token);
        Task<List<User>> GetUserList(CancellationToken token);
        Task<User> GetUser(Guid id, CancellationToken token);
        Task<Guid> DeleteUser(Guid id, CancellationToken token);
    }
}
