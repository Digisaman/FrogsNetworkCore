using OrchardCore.ContentManagement;

namespace FrogsNetwork.Forum.Models;

public class PostPart : ContentPart
{
    public virtual int? RepliedOn { get; set; }

    //[StringLengthMax]
    public virtual string Text { get; set; } = "";

    public virtual string Format { get; set; } = "";

    //public ThreadPart ThreadPart
    //{
    //    get { return this.As<ICommonPart>().Container.As<ThreadPart>(); }
    //    set { this.As<ICommonPart>().Container = value; }
    //}

    public bool IsParentThread()
    {
        return RepliedOn == null;
    }
}
