using AutoMapper;
using PASR.Application.Teams.Dto;
using PASR.Teams.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Teams
{
    public class TeamMapProfile : Profile
    {
        public TeamMapProfile()
        {
            CreateMap<CreateTeamDto, Team>();
            
            CreateMap<TeamDto, Team>().ReverseMap();

            CreateMap<Team, TeamCardDto>()
                .ForMember((tc) => tc.SdmUser,(opt) => opt.MapFrom((t) => t.SalesManager));

        }
        
    }
}
