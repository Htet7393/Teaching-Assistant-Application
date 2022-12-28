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
 *    This file contains code for the Modificataion model
 *         
 */

using System.ComponentModel.DataAnnotations;

namespace TAapplication.Models
{
    public class ModificationTracking
    {
        [ScaffoldColumn(false)]
        public DateTime CreationDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime ModificationDate { get; set; }
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; } = String.Empty;
        [ScaffoldColumn(false)]
        public string ModifiedBy { get; set; } = String.Empty;

    }
}
