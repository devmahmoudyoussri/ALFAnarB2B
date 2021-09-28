using fahim.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace fahim.Permissions
{
    public class fahimPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(fahimPermissions.GroupName);

            myGroup.AddPermission(fahimPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
            myGroup.AddPermission(fahimPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(fahimPermissions.MyPermission1, L("Permission:MyPermission1"));

            var contractPermission = myGroup.AddPermission(fahimPermissions.Contracts.Default, L("Permission:Contracts"));
            contractPermission.AddChild(fahimPermissions.Contracts.Create, L("Permission:Create"));
            contractPermission.AddChild(fahimPermissions.Contracts.Edit, L("Permission:Edit"));
            contractPermission.AddChild(fahimPermissions.Contracts.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<fahimResource>(name);
        }
    }
}