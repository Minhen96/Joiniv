using AutoMapper;
using Joiniv.Domain.Entities;
using Joiniv.Contracts.Auth;

namespace Joiniv.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Request -> Entity
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Entity -> Response
            CreateMap<User, AuthResponse>();
        }
    }
}