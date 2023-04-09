using System;
using System.Collections.Generic;
using System.Linq;
using YesSql.Indexes;

namespace FrogsNetwork.Forum.Indexing;
public class ForumIndex : MapIndex
{
    public string ForumContentItemId { get; set; }
    public string ContentItemId { get; set; }
    public string ContentType { get; set; }
    public string ContentPart { get; set; }
    public string ContentField { get; set; }
    public string PostContentItemId { get; set; }
    public bool Published { get; set; }
    public bool Latest { get; set; }
}
