using System.Collections.Generic;
using PASR.Roles.Dto;

namespace PASR.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}