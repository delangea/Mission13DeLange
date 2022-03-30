using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tester.Models;

namespace Tester.Controllers
{
    public class HomeController : Controller
    {
        //set up repository
        private IBowlerRepository _repo;

        public HomeController(IBowlerRepository temp)
        {
            _repo = temp;
        }

        //index
        public IActionResult Index(string team)
        {
            ViewBag.IsIndex = true;
            var teams = _repo.Teams.ToList();
            //get list of bowlers according to selected team, or all if none is selected
            var thing = _repo.Bowlers
                .Where(b => b.Team.TeamName == team || team == null).ToList();

            //used to set header
            if (team != null)
            {
                ViewBag.Selected = team;
            }
            return View(thing);
        }

        [HttpPost]
        public IActionResult Index(int BowlerID)
        {
            //deletes bowler when submitted
            var bowler = _repo.Bowlers.Where(x => x.BowlerID == BowlerID).FirstOrDefault();
            _repo.DeleteBowler(bowler);
            return RedirectToAction("Index", "");
        }

        public IActionResult EditBowlers(int BowlerID = 0)
        {
            Bowler b = new Bowler();
            //if it is not 0, it is an edit form
            if (BowlerID != 0)
            {
                //get correct bowler
                b = _repo.Bowlers.Where(x => x.BowlerID == BowlerID).FirstOrDefault();
                ViewBag.IsEdit = true;
            }
            //otherwise, we are creating a new bowler
            else
            {
                ViewBag.IsEdit = false;
            }
            ViewBag.Teams = _repo.Teams.Distinct().ToList();
            return View(b);
        }
        [HttpPost]
        public IActionResult EditBowlers(Bowler b)
        {
            if (ModelState.IsValid)
            {
                if (b.BowlerID != 0)
                {
                    //update bowler
                    _repo.SaveBowler(b);
                }
                else
                {
                    //create new bowler
                    //autoincrementing wasnt working so this is the work around
                    int max = _repo.Bowlers.OrderByDescending(x => x.BowlerID).First().BowlerID + 1;
                    b.BowlerID = max;
                    _repo.CreateBowler(b);
                }
                return RedirectToAction("Index");
            }
            //stuff to return model errors
            if (b.BowlerID != 0)
            {
                ViewBag.IsEdit = true;
            }
            else
            {
                ViewBag.IsEdit = false;
            }
            ViewBag.Teams = _repo.Teams.Distinct().ToList();
            return View(b);
        }

    }
}
