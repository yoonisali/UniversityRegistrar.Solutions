using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityRegistrar.Controllers
{
  public class MajorsController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public MajorsController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Major> model = _db.Majors.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Major major)
    {
      _db.Majors.Add(major);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Major thisMajor = _db.Majors
                              .Include(course => course.MajorCourses)
                              .ThenInclude(join => join.Course)
                              .Include(student => student.MajorStudents)
                              .ThenInclude(join => join.Student)
                              .FirstOrDefault(major => major.MajorId == id);
      return View(thisMajor);
    }
  }
}