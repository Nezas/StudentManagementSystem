using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models.ViewModels
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public string BranchName { get; set; }
        public IEnumerable<SelectListItem> DropDown { get; set; }
    }
}
