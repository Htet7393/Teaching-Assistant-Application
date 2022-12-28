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
 *    This file contains code for the AdminController
 *         
 */


using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.NetworkInformation;
using TAapplication.Models;
using TAapplication.Areas.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;
using TAapplication.Data;


namespace TAapplication.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> addrolecs(string user_s, string role)
        {
            TAUser user = await _userManager.FindByIdAsync(user_s);
            bool check_role = await _userManager.IsInRoleAsync(user, role);
            if (!check_role)
            {
                await _userManager.AddToRoleAsync(user, role);
                return Ok(new { success = true, message = "added!" });
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, role);
                return Ok(new { success = true, message = "removed!" });
            }

        }



        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<TAUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(ILogger<AdminController> logger, UserManager<TAUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult EnrollmentTrends()
        {
            return View();
        }

        [HttpPost]
        public int[] GetEnrollmentData(string c_name)
        {
            int[] dummy = new int[0];
            string c = c_name.Replace(" ",""); // remove any white spaces
            var enroll = _context.Enrollment.ToArray();
         
            foreach (var en in enroll)
            {
                string[] temp = en.course_date.Split(',');
                string cd = temp[0].Replace(" ", ""); 
                string cmp = cd.ToUpper();
                string a = c.ToUpper();
                if (cmp == a)
                {
                    int[] data = new int[temp.Length - 1];
                    for (int i = 1; i < temp.Length; i++)
                    {
                        data[i - 1] = int.Parse(temp[i]);
                    }
                    return data;
                }
            }
            return dummy;
        }
    }
}
