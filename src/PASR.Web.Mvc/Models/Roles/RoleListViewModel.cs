using System.Collections.Generic;
using PASR.Roles.Dto;

namespace PASR.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
