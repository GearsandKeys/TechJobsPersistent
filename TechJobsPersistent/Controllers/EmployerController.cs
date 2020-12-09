using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private EmployerDbContext context;
        public EmployerController(EmployerDbContext dbContext)
        {
            context = dbContext;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();
            
            return View(employers);
        }

        public IActionResult Add()
        {
            List<Employer> employers = context.Employers.ToList();
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel(employers);
            
            return View(addEmployerViewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            if (ModelState.IsValid)
            {
                Employer theEmployer = context.Employers.Find(addEmployerViewModel.Name)
                
                return Redirect("/Employers");
            }
            
            return View(addEmployerViewModel);
        }

        public IActionResult About(int id)
        {
            return View();
        }
    }
}
