using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsNetwork.Forums.ViewModels;
public class ForumPartViewModel
{
    //public virtual string Description { get; set; } = "";

    public virtual int ThreadCount { get; set; }
    public virtual int PostCount { get; set; }

    public virtual bool ThreadedPosts { get; set; }

    public virtual int Weight { get; set; }

    public virtual int ReplyCount
    {
        get; set;
    }

    public virtual string Title { get; set; }

    public virtual string Description { get; set; }

    public virtual string Id { get; set; }
}
