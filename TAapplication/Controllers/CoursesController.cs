/**
 * Author:    Htet Naing
 * Partner:   None
 * Date:      19 NOV 2022
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
 *    This file contains code for the CoursesController
 *         
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TAapplication.Data;
using TAapplication.Models;

namespace TAapplication.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Funciton to change note
        [HttpPost]
        public async Task<IActionResult> change_note(string new_note, int id)
        {
            var Courses = _context.Course.ToArray();
            foreach (Course c in Courses)
            {
                if (c.Id == id)
                {
                    c.Note = new_note;
                    break;
                }
            }
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Note Updated!" });
        }

        // GET: Courses
        [Authorize(Roles = "Administrator, Professor")]
        public async Task<IActionResult> List()
        {
              return View(await _context.Course.ToListAsync());
        }
      
        // GET: Courses/View/5
        public async Task<IActionResult> View(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        [Authorize(Roles = "Administrator")]
        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Semester,Year,Title,CourseNumber,Section,Description,ProfessorUnid,ProfessorName,TimeAndDay,Location,Credit,Enrollment,Note")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var courseToUpdate = _context.Course.Where(o => o.Id == id).FirstOrDefault();
            if (await TryUpdateModelAsync<Course>(courseToUpdate, "",
                s => s.Semester,
                s => s.Year,
                s => s.Title,
                s => s.CourseNumber,
                s => s.Section,
                s => s.Description,
                s => s.ProfessorUnid,
                s => s.ProfessorName,
                s => s.TimeAndDay,
                s => s.Location,
                s => s.Credit,
                s => s.Enrollment,
                s => s.Note

                    ))
            {
                try
                {
                    _context.SaveChanges();

                    return RedirectToAction("List");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(courseToUpdate);
        }
       
        // GET: Courses/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Course == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                _context.Course.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool CourseExists(int id)
        {
          return _context.Course.Any(e => e.Id == id);
        }
    }
}
