using AutoMapper;

namespace PASR.Calls.Dto
{
    public class CallMapProfile : Profile
    {
        public CallMapProfile()
        {
            CreateMap<CreateCallDto,Call>();

            CreateMap<Call,CallDto>()
                .ForMember(dto => dto.Duration, 
                           opt => opt.MapFrom(c => c.GetTimeSpent().ToString("c")))
                .ReverseMap();
        }

    }
}