using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsNetwork.Freelancing.Models;
public class CompanyUser : BaseUser
{
    public virtual int Id { get; set; }

    public virtual string UserId { get; set; }

    public virtual string CompanyTel { get; set; } = "";

    public virtual string Website { get; set; } = "";

    public virtual string ContactPersonPosition { get; set; } = "";
        
    public virtual string ContactPersonName { get; set; } = "";

    public virtual string Activities { get; set; } = "";

    public virtual string CompanyName { get; set; } = "";


}
