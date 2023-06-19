using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsNetwork.Forums.ViewModels;
public class ForumListViewModel
{
    public IEnumerable<ForumPartViewModel> Forums { get; set; }
    public ForumListViewModel()
    {
        Forums = new List<ForumPartViewModel>();
    }
}
