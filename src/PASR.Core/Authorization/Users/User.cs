﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.Extensions;
using PASR.Calls;
using PASR.Leads;
using PASR.Localization;
using PASR.Teams;

namespace PASR.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "pars1234";

        [Required]
        public override string PhoneNumber { get; set; }

        public ICollection<Call> Calls { get; set; }

        public ICollection<Lead> Leads { get; set; }

        public Address Address { get; set; }

        public ICollection<Team> Teams { get; set; }   

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
