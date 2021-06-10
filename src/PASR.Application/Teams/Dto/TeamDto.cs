using Abp.Application.Services.Dto;
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
    public class TeamDto : EntityDto
    {
        [Required]
        [StringLength(30)]
        public string TeamName { get; set; }

        [Required]
        [StringLength(200)]
        public string TeamDescription { get; set; }

        public IList<UserDto> Users { get; set; }

        public GoalDto Goal { get; set; }
    }
}
