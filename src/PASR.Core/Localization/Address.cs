using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PASR.Authorization.Users;
using PASR.Leads;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Localization
{
    //This class is owned by Other Entities (Further Configuration in Fluent API)
    public class Address
    {
        protected Address()
        { }

        public Address(string street, string number, string district, string city, string federalUnity)
        {
            Street = street;
            Number = number;
            District = district;
            City = city;
            FederalUnity = federalUnity;
        }

        [Required]
        [StringLength(200)]
        public string Street { get; set; }

        [Required]
        [StringLength(10)]
        public string Number { get; set; }

        [Required]
        [StringLength(60)]
        public string District { get; set; }

        [Required]
        [StringLength(60)]
        public string City { get; set; }

        [Required]
        [StringLength(4)]
        public string FederalUnity { get; set; }

    }
}
