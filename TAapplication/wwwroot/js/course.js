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
 *    This file contains Javascript code for the CoursesController
 *         
 */


function change_note(i_d) {
    swal({
        text: 'Type Note',
        content: "input",
        button: {text:"Submit",closeModal:true}
    }).then((note) =>{ 
        $.post(
        {
           url: "/Courses/change_note",
           data: {
              new_note: note,
              id: i_d
           }
        })
    })
}
