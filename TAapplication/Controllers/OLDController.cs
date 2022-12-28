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
 *    This file contains CSS code for the OldController
 *         
 */

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TAapplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace TAapplication.Controllers
{
    [Authorize]
    public class OLDController : Controller
    {
        private readonly ILogger<OLDController> _logger;

        public OLDController(ILogger<OLDController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult ApplicationList()
        {
            return View();
        }

        public IActionResult ApplicationCreate()
        {
            return View();
        }

        public IActionResult ApplicationDetails()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
