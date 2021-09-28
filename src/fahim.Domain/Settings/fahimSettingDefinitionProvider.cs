using Volo.Abp.Settings;

namespace fahim.Settings
{
    public class fahimSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(fahimSettings.MySetting1));
        }
    }
}
