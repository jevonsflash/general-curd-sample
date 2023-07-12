using Matoapp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Matoapp.Permissions;

public class MatoappPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MatoappPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MatoappPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MatoappResource>(name);
    }
}
