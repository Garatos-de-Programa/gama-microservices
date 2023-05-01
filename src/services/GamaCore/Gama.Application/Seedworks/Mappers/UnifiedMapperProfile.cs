using AutoMapper;
using Gama.Application.DataContracts.Commands.TrafficFineManagement;
using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Application.DataContracts.Responses.TrafficManagement;
using Gama.Application.DataContracts.Responses.UserManagement;
using Gama.Domain.Entities;

namespace Gama.Application.Seedworks.Mappers
{
    internal class UnifiedMapperProfile : Profile
    {
        public UnifiedMapperProfile()
        {
            CreateMap<TrafficFine, GetTrafficFineResponse>();

            CreateMap<CreateTrafficFineCommand, TrafficFine>();
            CreateMap<UpdateTrafficFineCommand, TrafficFine>();

            CreateMap<TrafficViolation, GetTrafficViolationResponse>();

            CreateMap<CreateTrafficViolationCommand, TrafficViolation>();
            CreateMap<UpdateTrafficViolationCommand, TrafficViolation>();
            CreateMap<CreateTrafficFineTrafficViolationCommand, TrafficViolation>();

            CreateMap<User, GetUserResonse>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(user => user.UserRoles.Select(ur => ur.Role.Name)));
            CreateMap<UserAddress, GetUserAddressResponse>();
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, UserCreatedResponse>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(user => user.UserRoles.Select(ur => ur.Role.Name)));
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
