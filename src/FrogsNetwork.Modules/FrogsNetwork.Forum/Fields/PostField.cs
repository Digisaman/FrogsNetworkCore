using OrchardCore.ContentManagement;

namespace FrogsNetwork.Forum.Fields;

public class PostField : ContentField
{
    public string PostContentItemId { get; set; } = "";
    public string[] ThreadContentItemIds { get; set; } = Array.Empty<string>();

}
