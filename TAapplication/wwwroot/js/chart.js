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
 *    This file contains Javascript code for EnrollmentTrends
 *         
 */

function get_data() {

    $.post(
        {
            url: "/Admin/GetEnrollmentData",
            data: {
                c_name: document.getElementById("courseselect").value,
            }
        })
        .done(function (data) {
            $("#EnrollmentChart").highcharts().addSeries(
                {
                    name: document.getElementById("courseselect").value,
                    data: data
                });
        })   
}



$(document).ready(function () {

    Highcharts.chart('EnrollmentChart', {
        chart: {
            type: 'spline'
        },
        title: {
            text: 'Enrollments Over Time'
        },
        xAxis: {
            title: {
                text: 'Dates'
            },
            categories: ['Nov 1', 'Nov 2', 'Nov 3', 'Nov 4', 'Nov5', 'Nov 6', 'Nov 7', 'Nov 8', 'Nov 9', 'Nov 10',
                'Nov 11', 'Nov 12', 'Nov 13', 'Nov 14', 'Nov 15', 'Nov 16', 'Nov 17', 'Nov 18', 'Nov 19', 'Nov 20',
                'Nov 21', 'Nov 22', 'Nov 23', 'Nov 24', 'Nov 25', 'Nov 26', 'Nov 27', 'Nov 28', 'Nov 29', 'Nov 30'],
                        
            accessibility: {
                description: 'Months of the year'
            }
        },
        yAxis: {
            title: {
                text: 'Students'
            },
            labels: {
                formatter: function () {
                    return this.value;
                }
            }
        },
        tooltip: {
            crosshairs: true,
            shared: true
        },
        plotOptions: {
            spline: {
                marker: {
                    radius: 4,
                    lineColor: '#666666',
                    lineWidth: 1
                }
            }
        },
        series: [{
            
            
        }]
    });
});

$(function () {
    $("#datepicker1").datepicker();
});


$(function () {
    $("#datepicker2").datepicker();
});










