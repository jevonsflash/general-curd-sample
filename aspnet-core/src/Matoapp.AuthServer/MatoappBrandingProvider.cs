using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Matoapp;

[Dependency(ReplaceServices = true)]
public class MatoappBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Matoapp";
}
