﻿using AutoMapper;
using Gama.Application.DataContracts.Commands.OccurrenceManagement;
using Gama.Application.DataContracts.Commands.TrafficFineManagement;
using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Application.DataContracts.Responses.OccurrenceManagement;
using Gama.Application.DataContracts.Responses.Pagination;
using Gama.Application.DataContracts.Responses.TrafficManagement;
using Gama.Application.DataContracts.Responses.UserManagement;
using Gama.Application.Seedworks.Pagination;
using Gama.Domain.Models.Occurrences;
using Gama.Domain.Models.TrafficFines;
using Gama.Domain.Models.Users;

namespace Gama.Application.Seedworks.Mappers
{
    internal class UnifiedMapperProfile : Profile
    {
        public UnifiedMapperProfile()
        {
            CreateMap<TrafficFine, GetTrafficFineResponse>()
                .ForMember(dest => dest.TrafficViolations,
                    opt => opt.MapFrom(trafficFine =>
                        trafficFine.TrafficFineTrafficViolations.Select(tv => tv.TrafficViolation)));

            CreateMap<CreateTrafficFineCommand, TrafficFine>()
                .ForMember(dest => dest.TrafficFineTrafficViolations,
                    opt => opt.MapFrom(trafficFine =>
                        trafficFine.TrafficViolations.Select(tv => new TrafficFineTrafficViolation()
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
