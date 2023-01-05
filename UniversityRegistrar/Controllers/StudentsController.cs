using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityRegistrar.Controllers
{
  public class StudentsController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public StudentsController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Student> model = _db.Students.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student)
    {
      if (!ModelState.IsValid)
      {
        return View(student);
      }
      else
      {
        _db.Students.Add(student);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Student thisStudent = _db.Students
                              .Include(student => student.JoinEntities)
                              .ThenInclude(join => join.Course)
                              .Include(student => student.MajorStudents)
                              .ThenInclude(join => join.Major)
                              .FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }

    public ActionResult AddCourse(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult AddCourse(Student student, int courseId)
    {
#nullable enable
      Enrollment? joinEntity = _db.Enrollments.FirstOrDefault(join => (join.CourseId == courseId && join.StudentId == student.StudentId));
#nullable disable
      if (joinEntity == null && courseId != 0)
      {
        _db.Enrollments.Add(new Enrollment() { CourseId = courseId, StudentId = student.StudentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = student.StudentId });
    }

    public ActionResult AddMajor(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
      ViewBag.MajorId = new SelectList(_db.Majors, "MajorId", "Name");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult AddMajor(Student student, int majorId)
    {
#nullable enable
      MajorStudent? joinEntity = _db.MajorStudents.FirstOrDefault(join => (join.MajorId == majorId && join.StudentId == student.StudentId));
#nullable disable
      if (joinEntity == null && majorId != 0)
      {
        _db.MajorStudents.Add(new MajorStudent() { MajorId = majorId, StudentId = student.StudentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = student.StudentId });
    }

    [HttpGet("/students/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Student thisStudent = _db.Students
                              .Include(student => student.JoinEntities)
                              .ThenInclude(join => join.Course)
                              .Include(student => student.MajorStudents)
                              .ThenInclude(join => join.Major)
                              .FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }

    [HttpPost("/students/{id}/edit")]
    public ActionResult Edit(Student student)
    {
      _db.Students.Update(student);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = student.StudentId });
    }

    public ActionResult DeleteMajorStudent(int id)
    {
      MajorStudent joinEntry = _db.MajorStudents.FirstOrDefault(entry => entry.MajorStudentId == id);
      Major thisMajor = _db.Majors.FirstOrDefault(entry => entry.MajorId == joinEntry.MajorId);
      ViewBag.Major = thisMajor.Name;
      return View(joinEntry);
    }

    [HttpPost, ActionName("DeleteMajorStudent")]
    public ActionResult DeleteMajorStudentConfirm(int id)
    {
      MajorStudent joinEntry = _db.MajorStudents.FirstOrDefault(entry => entry.MajorStudentId == id);
      _db.MajorStudents.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.StudentId });
    }

    public ActionResult DeleteEnrollment(int id)
    {
      Enrollment joinEntry = _db.Enrollments.FirstOrDefault(entry => entry.EnrollmentId == id);
      Course thisCourse = _db.Courses.FirstOrDefault(entry => entry.CourseId == joinEntry.CourseId);
      ViewBag.Course = thisCourse.Name;
      return View(joinEntry);
    }

    [HttpPost, ActionName("DeleteEnrollment")]
    public ActionResult DeleteEnrollmentConfirm(int id)
    {
      Enrollment joinEntry = _db.Enrollments.FirstOrDefault(entry => entry.EnrollmentId == id);
      _db.Enrollments.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.StudentId });
    }
  }
}