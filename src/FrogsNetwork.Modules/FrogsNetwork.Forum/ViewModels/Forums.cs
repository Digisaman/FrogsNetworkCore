using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsNetwork.Forum.ViewModels;
public class ForumsViewModel
{
    public IEnumerable<Models.ForumPart> Forums { get; set; }

    public ForumsViewModel()
    {
        Forums= new List<Models.ForumPart>();   
    }
}

