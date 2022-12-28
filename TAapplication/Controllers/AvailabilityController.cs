/**
 * Author:    Htet Naing
 * Partner:   None
 * Date:      29 NOV 2022
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
 *    This file contains code for the AvailabilityController
 *         
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Net.NetworkInformation;
using TAapplication.Areas.Data;
using TAapplication.Data;


namespace TAapplication.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<TAUser> _userManager;

        public AvailabilityController(ApplicationDbContext context, UserManager<TAUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }
    

        [Authorize]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null || _context.Availability == null)
            {
                return NotFound();
            }

            var availability = await _context.Availability
                .FirstOrDefaultAsync(m => m.Id == id);
            if (availability == null)
            {
                return NotFound();
            }

            return View(availability);
        }


        [HttpPost]
        public string get_avail(string userid, int avid)
        {
            //var users = _userManager.Users.ToArray();
            var avails = _context.Availability.ToArray();
            foreach(var av in avails)
            {
                if (avid == av.Id)
                {
                    return av.Monday+av.Tuesday+av.Wednesday+av.Thursday+av.Friday;
                }
            }
            return "ok";
        }


        [HttpPost]
        public async Task<IActionResult> save_res(string userid, int avid, string ans)
        {
            //var users = _userManager.Users.ToArray();         
            var avails =  _context.Availability.ToArray();
            string mon = "";
            string tue = "";
            string wed = "";
            string th = "";
            string fri = "";
            foreach (var av in avails)
            {
                if (avid == av.Id)
                {
                    for(int i = 0; i < 240; i++)
                    {
                        if(i < 48)
                        {
                            mon += ans[i];
                        }else if(i < 96)
                        {
                            tue += ans[i];
                        }
                        else if(i < 144)
                        {
                            wed += ans[i];
                        }
                        else if(i < 192)
                        {
                            th+= ans[i];
                        }
                        else
                        {
                            fri += ans[i];
                        }
                    }

                    av.Monday = mon;
                    av.Tuesday= tue;
                    av.Wednesday= wed;
                    av.Thursday= th;
                    av.Friday= fri;

                    await _context.SaveChangesAsync();
                    return Ok(new { success = true, message = "added!" });
                }
            }
            return Ok(new { success = true, message = "added!" });
        }
    }
}
