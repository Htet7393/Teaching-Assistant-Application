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
 *    This file contains code for the Course model
 *         
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TAapplication.Areas.Data;


namespace TAapplication.Models
{
    public enum Semester
    {
        Spring, Fall, Summer
    }

    public enum Year
    {
        [Display(Name ="2022")]
        twotwo,
        [Display(Name = "2023")]
        twothree,
        [Display(Name = "2024")]
        twofour
    }

    public enum TimeAndDay
    {
        [Display(Name = "M/W 3:30-5:00")]
        A,
        [Display(Name = "T/F 9:30-11:00")]
        B,
        [Display(Name = "T/Th 1:30-3:00")]
        C,
        [Display(Name = "W/F 4:30-6:00")]
        D,
        [Display(Name = "Th/F 8:30-10:00")]
        E
    }

    public class Course
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Semester")]
        public Semester Semester{ get; set; }

        [Required]
        [Display(Name = "Year")]
        public Year Year{ get; set; }

        [Display(Name = "Title")]
        [StringLength(100, ErrorMessage = "Enter a valid course title!")]
        public String Title { get; set; }

        [Required]
        [Display(Name = "Number")]
        [RegularExpression("[0-9]{4}")]
        public string CourseNumber { get; set; }

        [Required]
        [Display(Name = "Section")]
        [RegularExpression("^0[0][0-9]{1}")]
        public string Section { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(5000, ErrorMessage = "Description too long!")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Professor's UID")]
        [RegularExpression("^u[0-9]{7}")]
        public string ProfessorUnid { get; set; }

        [Required]
        [Display(Name = "Professor's Name")]
        [StringLength(100, ErrorMessage = "Name is too long!")]
        public string ProfessorName { get; set; }

        [Required]
        [Display(Name = "Offered on")]
        public TimeAndDay TimeAndDay { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Credit")]
        [Range(1, 5)]
        public int Credit { get; set; }

        [Required]
        [Display(Name = "Enrollment")]
        [Range(0, 200)]
        public int Enrollment { get; set; }

        [Display(Name = "Note")]
        public string? Note { get; set; }
    }
}
