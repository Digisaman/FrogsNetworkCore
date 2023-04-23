using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrogsNetwork.Freelancing.Controllers;
[Authorize]
public class CompanyDashboardController : Controller
{
    public CompanyDashboardController()
    {
        
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
}
