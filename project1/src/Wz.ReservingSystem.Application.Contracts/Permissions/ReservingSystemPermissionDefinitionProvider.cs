using Wz.ReservingSystem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Wz.ReservingSystem.Permissions;

public class ReservingSystemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ReservingSystemPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ReservingSystemPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ReservingSystemResource>(name);
    }
}
