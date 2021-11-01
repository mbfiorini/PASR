using AutoMapper;
using PASR.Authorization.Users;
using PASR.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Leads.Dto
{
    public class LeadMapProfile : Profile
    {
        public LeadMapProfile()
        {
            CreateMap<CreateLeadDto, Lead>();

            //Configurado por Atributo na classe LeadDto
            //CreateMap<LeadDto, Lead>().ReverseMap();

            //Lista de Resultados
            CreateMap<Lead, LeadListOutputDto>()
                .ForMember(ll => ll.FullName, opt => opt.MapFrom(l => $"{l.Name} {l.LastName}"));

            CreateMap<Lead, LeadEditDto>().ForMember(le => le.AssignedUserName, opt => opt.MapFrom(l => l.AssignedUser.UserName));
        }
    }
}
