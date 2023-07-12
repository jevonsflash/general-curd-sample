using Volo.Abp.Settings;

namespace Matoapp.Settings;

public class MatoappSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MatoappSettings.MySetting1));
    }
}
