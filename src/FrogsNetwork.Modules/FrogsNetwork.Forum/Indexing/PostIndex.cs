using YesSql.Indexes;

namespace FrogsNetwork.Forum.Indexing;

public class PostIndex : MapIndex
{
    public string PostContentItemId { get; set; }
    public string ContentItemId { get; set; }
    public string ContentType { get; set; }
    public string ContentPart { get; set; }
    public string ContentField { get; set; }
    public string ThreadContentItemId { get; set; }
    public bool Published { get; set; }
    public bool Latest { get; set; }
}
