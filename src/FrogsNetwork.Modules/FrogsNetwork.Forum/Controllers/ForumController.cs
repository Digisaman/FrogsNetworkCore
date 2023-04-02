using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Forum.Models;
using FrogsNetwork.Forum.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrogsNetwork.Forum.Controllers;
public class ForumController : Controller
{
    public ForumsViewModel ViewModel { get; set; }
    public ForumController()
    {
        ViewModel = new ForumsViewModel();
        
    }
    public async Task<ActionResult> Index()
    {
        return View(this.ViewModel);
    }
}
