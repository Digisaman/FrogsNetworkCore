using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace FrogsNetwork.Forums.Models;

public class ForumPart : ContentPart
{   
  
    //public virtual string Description { get; set; } = "";

    public virtual int ThreadCount { get; set; }
    public virtual int PostCount { get; set; }

    public virtual bool ThreadedPosts { get; set; }

    public virtual int Weight { get; set; }

    public virtual int ReplyCount
    {
        get { return PostCount >= ThreadCount ? PostCount - ThreadCount : 0; }
    }

    public TextField Description { get; set; }

    public HtmlField Body { get; set; }

    //public ForumField ForumField { get; set; }
    public string PostContentType { get; set; } = "Post";
    public List<ContentItem> Posts { get; set; } = new List<ContentItem>();



    
}
