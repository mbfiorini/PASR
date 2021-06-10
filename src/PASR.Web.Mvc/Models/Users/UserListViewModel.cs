using System.Collections.Generic;
using PASR.Roles.Dto;

namespace PASR.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
