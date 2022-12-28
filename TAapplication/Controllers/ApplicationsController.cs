/**
 * Author:    Htet Naing
 * Partner:   None
 * Date:      10 NOV 2022
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Htet Naing - This work may not be copied for use in Academic Coursework.
 *
 * I, Htet Naing, certify that I wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 *
 *    This file contains code for the ApplicationsController
 *         
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TAapplication.Areas.Data;
using TAapplication.Data;
using TAapplication.Models;


namespace TAapplication.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<TAUser> _um;

        public ApplicationsController(ApplicationDbContext context, UserManager<TAUser> um)
        {
            _context = context;
            _um = um;
        }

        [Authorize(Roles ="Administrator")]
        public IActionResult Index()
        {
            return View();
        }

   
        // GET: Applications
        [Authorize(Roles="Professor, Administrator")]
        public async Task<IActionResult> List()
        {
              return View(await _context.Application.Include(o=>o.User).ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Pursuing,Department,GPA,Hours,Availability,CompletedSemester,PersonalStatement,TransferSchool,LinkedIn,Resume")] Application application)
        {
            ModelState.Remove("User");
            application.User = await _um.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { application.Id });
            }
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Applicant, Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles ="Administrator, Applicant")]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var applicationToUpdate = _context.Application.Where(o => o.Id == id).Include(o => o.User).FirstOrDefault();
            if (await TryUpdateModelAsync<Application>(applicationToUpdate, "",
                a => a.Pursuing,
                a => a.Department,
                a => a.GPA,
                a => a.Hours,
                a => a.Availability,
                a => a.CompletedSemester,
                a => a.PersonalStatement,
                a => a.TransferSchool,
                a => a.LinkedIn,
                a => a.Resume
              
                    ))
            {
                try
                {
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(applicationToUpdate);
        }



        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {          
            if (_context.Application == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Application'  is null.");
            }
            var application = await _context.Application.FindAsync(id);
            if (application != null)
            {
                _context.Application.Remove(application);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool ApplicationExists(int id)
        {
          return _context.Application.Any(e => e.Id == id);
        }
    }
}
