using Volo.Abp.Reflection;

namespace Matoapp.Common.Permissions;

public class CommonPermissions
{
    public const string GroupName = "Common";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CommonPermissions));
    }
}
