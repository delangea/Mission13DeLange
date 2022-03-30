using Microsoft.AspNetCore.Mvc;
using Tester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tester.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private IBowlerRepository repo { get; set; }
        public TeamsViewComponent(IBowlerRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            //send team info and route data so that it works
            ViewBag.SelectedType = RouteData?.Values["team"];
            var teams = repo.Teams.Select(x => x.TeamName);
            return View("Default", teams);
        }
    }
}
