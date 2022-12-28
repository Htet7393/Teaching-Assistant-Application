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
 *    This file contains code for the Application model
 *         
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TAapplication.Areas.Data;

namespace TAapplication.Models
{
    public enum Pursuing
    {
        BS, MS, PhD
    }
    public class Application : ModificationTracking
    {
        [Required]
        public int Id { get; set; }

        public TAUser User { get; set; } = null!;

        [Required]
        [Display(Name = "Pursuing")]
        public Pursuing Pursuing {get; set; }

        [Required]
        [Display(Name = "Major")]
        public string Department { get; set; }
        [Required]
        public float GPA { get; set; }

        [Required]
        [Range(5,20)]
        public int Hours { get; set; }

        [Required]
        [Display(Name = "Availability")]
        public bool Availability { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0!")]
        public int CompletedSemester { get; set; }

        [StringLength(50000, ErrorMessage = "Exceeded allowed length!")]
        public string? PersonalStatement { get; set; }

        [StringLength(100, ErrorMessage = "Enter a valid school!")]
        public string? TransferSchool { get; set; }

        [StringLength(100, ErrorMessage = "Enter a valid LinkedIn link!")]
        [Url]
        public string? LinkedIn { get; set; }

        [StringLength(100, ErrorMessage = "File not found")]
        public string? Resume { get; set; }
       

    }
}
