using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsNetwork.Forum.Settings;
public class ForumFieldSettings
{
    public string ForumContentItemId { get; set; }
    public bool DefaultThreadedPosts { get; set; }
    public string ThreadType { get; set; } = "";
    public string PostType { get; set; } = "";
}
