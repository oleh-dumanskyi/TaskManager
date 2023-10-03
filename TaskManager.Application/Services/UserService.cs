using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.DTOs;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ITaskManagerDbContext _context;

        public UserService(ITaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> UserSignUp(UserDTO request, CancellationToken token)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Surname = request.Surname,
                Password = request.Password,
                Name = request.Name,
            };

            await _context.Users.AddAsync(user, token);
            await _context.SaveChangesAsync(token);

            return user.Id;
        }

        public async Task<ClaimsIdentity> UserSignIn(UserSignInDTO request, CancellationToken token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(l => l.Email == request.Email
                                                                     && l.Password == request.Password, token);
            if (user == null)
            {
                throw new NotFoundException(nameof(UserSignInDTO), request.Email + "/" + request.Password);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            return claimsIdentity;
        }

        public async Task<Guid> EditUser(Guid id, UserDTO request, CancellationToken token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(l=>l.Id == id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Email + "/" + request.Password);
            }

            user.Email = request.Email;
            user.Password = request.Password;
            user.Name = request.Name;
            user.Surname = request.Surname;

            _context.Users.Update(user);
            await _context.SaveChangesAsync(token);

            return user.Id;
        }

        public async Task<List<User>> GetUserList(CancellationToken token)
        {
            var users = await _context.Users.ToListAsync(token);
            return users;
        }

        public async Task<User> GetUser(Guid id, CancellationToken token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(l=>l.Id == id, token);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), id);
            }
            return user;
        }

        public async Task<Guid> DeleteUser(Guid id, CancellationToken token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(l => l.Id == id, token);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), id);
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(token);
            return user.Id;
        }
    }
}
