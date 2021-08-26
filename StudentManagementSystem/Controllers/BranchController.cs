using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Controllers
{
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BranchController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Branch> branches = _db.Branches;

            foreach(var branch in branches)
            {
                branch.StudentsCount = _db.Students.Count(s => s.BranchId == branch.Id);
            }
            _db.SaveChanges();

            return View(branches);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Branch branch)
        {
            if(ModelState.IsValid)
            {
                _db.Branches.Add(branch);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var branch = _db.Branches.Find(id);
            if(branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Branch branch)
        {
            if(ModelState.IsValid)
            {
                _db.Branches.Update(branch);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var branch = _db.Branches.Find(id);
            if(branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var branch = _db.Branches.Find(id);
            if(branch == null)
            {
                return NotFound();
            }
            _db.Branches.Remove(branch);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
