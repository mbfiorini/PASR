using Abp.Domain.Entities;
using JetBrains.Annotations;
using PASR.Authorization.Users;
using PASR.Goals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Teams
{
    public class Team : Entity<int>
    {
        protected Team()
        {}

        public Team(string teamName, string teamDescription)
        {
            TeamName = teamName;
            TeamDescription = teamDescription;
        }

        [Required]
        [StringLength(PASRConsts.MaxNamesLength)]
        public string TeamName { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string TeamDescription { get; set; }

        public ICollection<User> Users { get; set; }

        public Goal Goal { get; set; }

    }
}
