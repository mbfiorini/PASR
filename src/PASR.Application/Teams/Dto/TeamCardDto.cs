using Abp.Application.Services.Dto;

namespace PASR.Application.Teams.Dto
{
    public class TeamCardDto : EntityDto
    {
        public string TeamName { get; set; }

        public string TeamDescription { get; set; }

        public int GoalPoints { get; set; }

        public int GoalTotal { get; set; }

        public string SdmUser { get; set; }

        public string SdmFullname { get; set; }
    }
}