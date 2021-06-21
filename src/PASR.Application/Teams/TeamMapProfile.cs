using AutoMapper;
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
        }
        
    }
}
