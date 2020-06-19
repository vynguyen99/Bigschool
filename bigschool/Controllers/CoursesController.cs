using bigschool.Models;
using bigschool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Heading = "Add Courses"
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lectuter)
                .Include(l => l.Category)
                .ToList();
            var viewModel = new CourseViewModel
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Course
                .Where(c => c.LectuterId == userId && c.DateTime > DateTime.Now)

                .Include(l => l.Lectuter)
                .Include(l => l.Category)
                .ToList();

            return View(courses);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.Single(c => c.id == id && c.LectuterId == userId);
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Date = course.DateTime.ToString("dd/MM/yyyy"),
                Time = course.DateTime.ToString("HH:mm"),
                Category = course.Categoryid,
                Place = course.Place,
                Heading = "Edit Courses",
                Id = course.id
            };


            return View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
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
            return RedirectToAction("Index", "Home");

        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Update(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.Single(c => c.id == viewModel.Id && c.LectuterId == userId);

            course.Place = viewModel.Place;
            course.DateTime = viewModel.GetDateTime();
            course.Categoryid = viewModel.Category;
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");


        }
    }
}