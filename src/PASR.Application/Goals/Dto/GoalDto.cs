using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Goals.Dto
{
    public class GoalDto : EntityDto
    {
        [Required]
        public decimal Score { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
    }
}