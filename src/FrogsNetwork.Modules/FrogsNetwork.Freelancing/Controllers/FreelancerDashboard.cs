using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrogsNetwork.Freelancing.Controllers;
[Authorize]
public class FreelancerDashboardController : Controller
{
    public FreelancerDashboardController()
    {
        
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
}
