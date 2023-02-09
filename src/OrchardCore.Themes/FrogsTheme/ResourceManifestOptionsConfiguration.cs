using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace FrogsNetwork.Themes.FrogsTheme
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            _manifest
                .DefineStyle("FrogsTheme-bootstrap-oc")
                .SetUrl("~/FrogsTheme/css/bootstrap-oc.min.css", "~/FrogsTheme/css/bootstrap-oc.css")
                .SetVersion("1.0.0");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
