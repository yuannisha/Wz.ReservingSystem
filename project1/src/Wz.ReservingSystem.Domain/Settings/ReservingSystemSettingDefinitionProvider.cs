using Volo.Abp.Settings;

namespace Wz.ReservingSystem.Settings;

public class ReservingSystemSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ReservingSystemSettings.MySetting1));
    }
}
