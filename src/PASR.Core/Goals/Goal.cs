using Abp.Domain.Entities;
using PASR.Teams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Goals
{
    public class Goal : Entity<int>, IPassivable
    {
        public Goal(decimal score, DateTime startDate, DateTime endDate)
        {
            Score = score;
            StartDate = startDate;
            EndDate = endDate;
        }
        protected Goal() { }

        [Required]
        public decimal Score { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public decimal CurrentScore { get; set; }
        
        public bool IsActive { get; set; }

        public int GetExpirationDays(DateTime dateTime)
        {

            var expirationDays = (EndDate - dateTime).Days;

            return (expirationDays  > 0) ? expirationDays : 0;
        }

    }
}
