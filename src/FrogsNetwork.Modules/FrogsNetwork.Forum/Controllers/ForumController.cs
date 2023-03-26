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
        ViewModel = new ForumsViewModel
        {
            Forums = new List<Models.Forum>
            {
                new Models.Forum
                {
                     AuthorId = "admin",
                      CreatedTime = DateTime.Now, Description = "forum one",
                       Id = 1,
                        Title = "forum 1"

                } 
            }
        };
    }
    public async Task<ActionResult> Index()
    {
        return View(this.ViewModel);
    }
}
