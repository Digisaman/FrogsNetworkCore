using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsNetwork.Forum.Models;
public class Forum
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public string AuthorId { get; set; } = "";

    public DateTime CreatedTime { get; set; }
}

