using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> studentList = _db.Students;

            foreach(var student in studentList)
            {
                student.Branch = _db.Branches.First(i => i.Id == student.BranchId);
            }
            return View(studentList);
        }

        public IActionResult Create()
        {
            StudentVM studentVM = new()
            {
                Student = new Student(),
                DropDown = _db.Branches.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(studentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentVM obj)
        {
            if(ModelState.IsValid)
            {
                _db.Students.Add(obj.Student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            obj.DropDown = _db.Branches.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(obj);
        }

        public IActionResult Update(int? id)
        {
            StudentVM studentVM = new()
            {
                Student = new Student(),
                DropDown = _db.Branches.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if(id == null || id == 0)
            {
                return NotFound();
            }

            studentVM.Student = _db.Students.Find(id);
            if(studentVM == null)
            {
                return NotFound();
            }
            return View(studentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(StudentVM studentVM)
        {
            if(ModelState.IsValid)
            {
                _db.Students.Update(studentVM.Student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentVM);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            StudentVM studentVM = new();

            studentVM.Student = _db.Students.Find(id);
            studentVM.BranchName = _db.Branches.Find(studentVM.Student.BranchId).Name;

            if(studentVM == null)
            {
                return NotFound();
            }
            return View(studentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var student = _db.Students.Find(id);
            if(student == null)
            {
                return NotFound();
            }

            _db.Students.Remove(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
