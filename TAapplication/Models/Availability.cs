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
 *    This file contains code for the Aavailability model
 *         
 */

using TAapplication.Areas.Data;

namespace TAapplication.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public TAUser User { get; set; } = null!;

        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }




    }

   
}
