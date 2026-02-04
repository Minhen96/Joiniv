using Joiniv.Domain.Interfaces;
using Joiniv.Application.Interfaces;
using Joiniv.Contracts.Auth;
using Joiniv.Domain.Exceptions;
using Joiniv.Domain.Entities;
using AutoMapper;

namespace Joiniv.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IJwtTokenService jwtTokenService, IUserRepository userRepository, IMapper mapper)
        {
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new ApiException("Email already exists", 400);
            }

            var user = _mapper.Map<User>(request);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new ApiException("Invalid email or password", 401);
            }

            var token = _jwtTokenService.GenerateToken(user);
            AuthResponse response = _mapper.Map<AuthResponse>(user);
            response.Token = token;
            return response;
        }
    }
}