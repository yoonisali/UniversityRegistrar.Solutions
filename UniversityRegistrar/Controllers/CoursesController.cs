using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityRegistrar.Controllers
{
  public class CoursesController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public CoursesController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Course> model = _db.Courses.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Course course)
    {
      _db.Courses.Add(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Course thisCourse = _db.Courses
                              .Include(course => course.JoinEntities)
                              .ThenInclude(join => join.Student)
                              .Include(course => course.MajorCourses)
                              .ThenInclude(join => join.Major)

                              .FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }

    public ActionResult AddMajor(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(courses => courses.CourseId == id);
      ViewBag.MajorId = new SelectList(_db.Majors, "MajorId", "Name");
      return View(thisCourse);
    }

    [HttpPost]
    public ActionResult AddMajor(Course course, int majorId)
    {
#nullable enable
      MajorCourse? joinEntity = _db.MajorCourses.FirstOrDefault(join => (join.MajorId == majorId && join.CourseId == course.CourseId));
#nullable disable
      if (joinEntity == null && majorId != 0)
      {
        _db.MajorCourses.Add(new MajorCourse() { MajorId = majorId, CourseId = course.CourseId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = course.CourseId });
    }

    [HttpGet("/courses/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Course thisCourse = _db.Courses
                              .Include(course => course.JoinEntities)
                              .ThenInclude(join => join.Student)
                              .Include(course => course.MajorCourses)
                              .ThenInclude(join => join.Major)
                              .FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }

    [HttpPost("/courses/{id}/edit")]
    public ActionResult Edit(Course course)
    {
      _db.Courses.Update(course);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = course.CourseId });
    }

    public ActionResult DeleteMajorCourse(int id)
    {
      MajorCourse joinEntry = _db.MajorCourses.FirstOrDefault(entry => entry.MajorCourseId == id);
      Major thisMajor = _db.Majors.FirstOrDefault(entry => entry.MajorId == joinEntry.MajorId);
      ViewBag.Major = thisMajor.Name;
      return View(joinEntry);
    }

    [HttpPost, ActionName("DeleteMajorCourse")]
    public ActionResult DeleteMajorCourseConfirm(int id)
    {
      MajorCourse joinEntry = _db.MajorCourses.FirstOrDefault(entry => entry.MajorCourseId == id);
      _db.MajorCourses.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.CourseId });
    }

    public ActionResult DeleteEnrollment(int id)
    {
      Enrollment joinEntry = _db.Enrollments.FirstOrDefault(entry => entry.EnrollmentId == id);
      Student thisStudent = _db.Students.FirstOrDefault(entry => entry.StudentId == joinEntry.StudentId);
      ViewBag.Student = thisStudent.Name;
      return View(joinEntry);
    }

    [HttpPost, ActionName("DeleteEnrollment")]
    public ActionResult DeleteEnrollmentConfirm(int id)
    {
      Enrollment joinEntry = _db.Enrollments.FirstOrDefault(entry => entry.EnrollmentId == id);
      _db.Enrollments.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.CourseId });
    }
  }
}
