using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrogsNetwork.Forum.Settings;
using OrchardCore.ContentManagement;
using static Dapper.SqlMapper;

namespace FrogsNetwork.Forum.Models;

public class ForumPart : ContentPart
{   
    //[StringLengthMax]
    public virtual string Description { get; set; } = "";

    public virtual int ThreadCount { get; set; }
    public virtual int PostCount { get; set; }

    public virtual bool ThreadedPosts { get; set; }

    public virtual int Weight { get; set; }

    public virtual int ReplyCount
    {
        get { return PostCount >= ThreadCount ? PostCount - ThreadCount : 0; }
    }

    public string PostContentType { get; set; } = "";
    public List<ContentItem> Posts { get; set; } = new List<ContentItem>();

}
