using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Goals.Dto
{
    public class GoalMapProfile : Profile
    {
        public GoalMapProfile()
        {
            CreateMap<Goal, GoalDto>();
            CreateMap<GoalDto, Goal>();
        }
    }
}
