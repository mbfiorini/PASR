using AutoMapper;

namespace PASR.Calls.Dto
{
    public class CallMapProfile : Profile
    {
        public CallMapProfile()
        {
            CreateMap<CreateCallDto,Call>();

            CreateMap<Call,CallDto>().ReverseMap();
        }

    }
}