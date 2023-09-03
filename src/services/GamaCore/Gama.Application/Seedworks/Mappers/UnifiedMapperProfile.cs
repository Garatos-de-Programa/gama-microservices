using AutoMapper;
using Gama.Application.Seedworks.Pagination;
using Gama.Application.UseCases.OccurrenceAgg.Commands;
using Gama.Application.UseCases.OccurrenceAgg.Responses;
using Gama.Application.UseCases.TrafficFineAgg.Commands;
using Gama.Application.UseCases.TrafficFineAgg.Responses;
using Gama.Application.UseCases.UserAgg.Commands;
using Gama.Application.UseCases.UserAgg.Responses;
using Gama.Domain.Entities.OccurrencesAgg;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Seedworks.Mappers
{
    internal class UnifiedMapperProfile : Profile
    {
        public UnifiedMapperProfile()
        {
            CreateMap<TrafficFine, GetTrafficFineResponse>()
                .ForMember(dest => dest.TrafficViolations,
                    opt => opt.MapFrom(trafficFine =>
                        trafficFine.TrafficFineTrafficViolations!.Select(tv => tv.TrafficViolation)))
                .ForMember(dest => dest.LicensePlate,
                    opt => opt.MapFrom(trafficFine =>
                        MercosulLicensePlate.Format(trafficFine.LicensePlate!)));

            CreateMap<CreateTrafficFineCommand, TrafficFine>()
                .ForMember(dest => dest.TrafficFineTrafficViolations,
                    opt => opt.MapFrom(trafficFine =>
                        trafficFine.TrafficViolations!.Select(tv => new TrafficFineTrafficViolation()
                        {
                            TrafficViolation = new TrafficViolation()
                            {
                                Id = tv.Id,
                            }
                        })));

            CreateMap<UpdateTrafficFineCommand, TrafficFine>();

            CreateMap<TrafficViolation, GetTrafficViolationResponse>();

            CreateMap<CreateTrafficViolationCommand, TrafficViolation>();
            CreateMap<UpdateTrafficViolationCommand, TrafficViolation>();
            CreateMap<CreateTrafficFineTrafficViolationCommand, TrafficViolation>();

            CreateMap<User, GetUserResonse>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(user => user.Roles.Select(ur => ur.Role.Name)));
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, UserCreatedResponse>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(user => user.Roles.Select(ur => ur.Role.Name)));
            CreateMap<UpdateUserCommand, User>();

            CreateMap<User, GetUsersResponse>();

            CreateMap<OffsetPage<User>, OffsetPageResponse<GetUsersResponse>>();
            CreateMap<OffsetPage<TrafficFine>, OffsetPageResponse<GetTrafficFineResponse>>();
            CreateMap<OffsetPage<Occurrence>, OffsetPageResponse<SearchOcurrenceResponse>>();

            CreateMap<Occurrence, GetOccurrenceResponse>()
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(o => o.Status.Name))
                .ForMember(dest => dest.OccurrenceUrgencyLevel, opt => opt.MapFrom(o => o.OccurrenceUrgencyLevel.Name))
                .ForMember(dest => dest.OccurrenceType, opt => opt.MapFrom(o => o.OccurrenceType.Name));
            CreateMap<Occurrence, SearchOcurrenceResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(o => o.Status.Name))
                .ForMember(dest => dest.OccurrenceUrgencyLevel, opt => opt.MapFrom(o => o.OccurrenceUrgencyLevel.Name))
                .ForMember(dest => dest.OccurrenceType, opt => opt.MapFrom(o => o.OccurrenceType.Name));
            CreateMap<CreateOccurrenceCommand, Occurrence>();

            CreateMap<GetTrafficFineFileResponse, TrafficFineFile>();
        }
    }
}
