using Volo.Abp.Settings;

namespace Ezrie.CRM.Settings;

public class CrmSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CrmSettings.MySetting1));
    }
}
