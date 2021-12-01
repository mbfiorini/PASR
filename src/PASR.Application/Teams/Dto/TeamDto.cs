using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PASR.Goals.Dto;
using PASR.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Teams.Dto
{
    [AutoMap(typeof(Team))]
    public class TeamDto : EntityDto
    {
        [Required]
        [StringLength(PASRConsts.MaxNamesLength)]
        public string TeamName { get; set; }

        [Required]
        [StringLength(200)]
        public string TeamDescription { get; set; }

        public IList<UserDto> SDRs { get; set; }

        [Required]
        public UserDto SalesManager { get; set; }

        public GoalDto Goal { get; set; }
    }
}
