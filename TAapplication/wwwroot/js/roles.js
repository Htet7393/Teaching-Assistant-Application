/**
 * Author:    Htet Naing
 * Partner:   None
 * Date:      10 OCT 2022
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
 *    This file contains Javascript code for the AdminController
 *         
 */

$(document).ready(function () {
    $('#table').DataTable();
});
 
function change_role(id, name) {
    $.post(
        {
            url: "/Admin/addrolecs",
            data: {
                user_s: id,
                role: name
            }
        })
        .done(function (data) {
            console.log("Sample of data:", data);
        })
}




