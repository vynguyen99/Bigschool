using bigschool.Models;
using bigschool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bigschool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        
        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var viewModel= new CourseViewModel
            {
               Categories=_dbContext.Categories.ToList()
            };
           
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create( CourseViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
             LectuterId = User.Identity.GetUserId(),
            DateTime = viewModel.GetDateTime(),
            Categoryid = viewModel.Category,
            Place = viewModel.Place
            };
            _dbContext.Course.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Index","Home");

        }
    }
}