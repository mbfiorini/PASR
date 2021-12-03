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

        public Team(string name, string description, User salesManager)
        {
            Name = name;
            Description = description;
            SalesManager = salesManager;
        }

        [Required]
        [StringLength(PASRConsts.MaxNamesLength)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public User SalesManager { get; set; }

        public ICollection<User> SDRs { get; set; }

        public ICollection<Goal> Goals { get; set; }
    }
}
