﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context; 

        public HomeController(JobDbContext dbContext) //JobDbContext
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).Include(j => j.JobSkills).ToList(); //this was different

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            //List<Employer> employers = context.Employers.ToList();
            //List<Skill> skills = context.Skills.ToList();

            AddJobViewModel addJobViewModel = new AddJobViewModel(context.Employers.ToList(), context.Skills.ToList()); ;
            
            return View(addJobViewModel);
        }

        
        public IActionResult ProcessAddJobForm(AddJobViewModel viewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                
                Job newJob = new Job
                {
                    Name = viewModel.Name,
                    EmployerId = viewModel.EmployerId,
                    Employer = context.Employers.Find(viewModel.EmployerId), //Didn't have this

                };

                foreach (string item in selectedSkills)
                {
                    JobSkill newJobSkill = new JobSkill
                    {
                        Job = newJob,
                        JobId = newJob.Id,
                        SkillId = int.Parse(item),
                        //Skill = context.Skills.Find(int.Parse(item)) //they didnt have this
                    };
                    context.JobSkills.Add(newJobSkill);
                }
                context.Jobs.Add(newJob);
                context.SaveChanges();

                return Redirect("Index");
            }
            
            return View("AddJob", viewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
