﻿using bigschool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace bigschool.Controllers.Api
{
    public class CoursesController: ApiController
    {
        public ApplicationDbContext _dbContext { get; set; }
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult Cancel (int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.Single(c => c.id == id && c.LectuterId == userId);
            if (course.IsCanceled)
                return NotFound();
            course.IsCanceled = true;
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}