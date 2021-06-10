using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PASR.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Leads.Dto
{
    [AutoMap(typeof(Address))]
    public class AddressDto
    {
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
