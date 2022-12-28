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
 *    This file contains Javascript code for the availability
 *         
 */


// Set up the canvas
let bg_color = 0x888888;
let app = new PIXI.Application({ backgroundColor: bg_color });
app.renderer.resize(850, 600);
$(document).ready(function () {
    $("#canvas").append(app.view);
    $("#save").hide();
});

class Slot extends PIXI.Graphics {
    on_color = 0xbba111;
    off_color = 0x321456;
    selected = false;
    width = 100;
    height = 10;
    text;
    day;
    color = 0xeeeeee;

    constructor(id, x, y, day) {
        super();
        this.x = x;
        this.y = y;
        this.id = id;
        this.day = day;
        this.selected = false;
        this.text = new PIXI.Text('Slot${id}');
        this.interactive = true;

        this.draw();

        app.stage.addChild(this);

        //this.on('mousedown', this.pointer_down);
        this.on('mousedown', this.pointer_down);
        this.addChild(this.text);
        this.text.visible = false;

    }

    draw() {
        if (this.selected) {
            this.clear();
            this.beginFill(this.on_color);
            this.drawRect(0, 0, this.width, this.height);
            //this.text.visible = true;
        }
        else {
            this.clear();
            this.beginFill(this.off_color);
            this.drawRect(0, 0, this.width, this.height);
            //this.text.visible = false;
        }
    }

    // get the assigned day
    get_day() {
        return this.day;
    }

    pointer_down() {
        this.selected = !this.selected;


        $("#save").show();
        this.draw();
    }

    make_true() {
        this.selected = true;
        this.draw();
    }

    get_selected() {
        return this.selected;
    }

}
 
let slots = [];
// 48 15 minuts slots fro 8 to 8
for (let i = 0; i < 240; i++) {
    if (i < 48) {      
        slots[i] = new Slot(i, 20, 50 + i * 10, 'Monday');
    }
    else if (i < 96) {
        slots[i] =  new Slot(i, 140, 50 + (i - 48) * 10, 'Tuesday');
    }
    else if (i < 144) {
        slots[i] =  new Slot(i, 260, 50 + (i - 96) * 10, 'Wednesday');
    }
    else if (i < 192) {
        slots[i] =  new Slot(i, 380, 50 + (i - 144) * 10, 'Thursday');
    }
    else {
        slots[i] =  new Slot(i, 500, 50 + (i - 192) * 10, 'Friday');
    }
}



function fill_data(id, avid) {
    $.post(
        {
            url: "/Availability/get_avail",
            data: {
                userid: id,
                avid: avid
            }
        })
        .done(function (data) {
            let mon = data; // monday availability
            console.log("Returned:", mon.length);
            for (let i = 0; i < 240; i++) {
                if (mon[i] == 1) {
                    slots[i].make_true();
                }
            }

        })
}

function save_data(id, avid) {
    let res = "";
    for (let i = 0; i < 240; i++) {
        if (slots[i].get_selected() == true) {
            res += '1';
        } else {
            res += '0';
        }
    }
    $.post(
        {
            url: "/Availability/save_res",
            data: {
                userid: id,
                avid: avid,
                ans: res
            }
        })
        .done(function (data) {
                    
        })  
}


const style = new PIXI.TextStyle({
    fontSize: 12,
    fontFamily: 'Arial',
    fontWeight: 'bold',
});

// Adding days names
for (let i = 0; i < 5; i++) {

    if (i == 0) {
        const monText = new PIXI.Text('MONDAY', style);
        monText.x = 40;
        monText.y = 20;
        app.stage.addChild(monText);
    }
    else if (i == 1) {
        const tueText = new PIXI.Text('TUESDAY', style);
        tueText.x = 160;
        tueText.y = 20;
        app.stage.addChild(tueText);
    }
    else if (i == 2) {
        const wedText = new PIXI.Text('WEDSDAY', style);
        wedText.x = 280;
        wedText.y = 20;
        app.stage.addChild(wedText);
    }
    else if (i == 3) {
        const thText = new PIXI.Text('WEDSDAY', style);
        thText.x = 400;
        thText.y = 20;
        app.stage.addChild(thText);
    }
    else {
        const friText = new PIXI.Text('FRIDAY', style);
        friText.x = 520;
        friText.y = 20;
        app.stage.addChild(friText);
    }
}

class Line extends PIXI.Graphics {
    color = 0xff0000;
    width = 700;
    height = 2;
    constructor(x, y) {
        super();
        this.x = x;
        this.y = y;
        this.draw_line();
        app.stage.addChild(this);
    }

    draw_line() {
        this.beginFill(this.color);
        this.drawRect(0, 0, this.width, this.height);
    }
}

for (let i = 0; i < 13; i++) {
    new Line(0, 50 + (i * 40));
}

for (let i = 0; i < 13; i++) {
    if (i < 4) {
        let j = i + 8;
        const timeText = new PIXI.Text(j +':00 am', style);
        timeText.x = 720;
        timeText.y = 43 + (i * 40);
        app.stage.addChild(timeText);
    } else { 
        let j = i - 4;
        const timeText = new PIXI.Text(j + ':00 pm', style);
        timeText.x = 720;
        timeText.y = 43 + (i * 40);
        app.stage.addChild(timeText);
    }
}

const desText = new PIXI.Text("Click and drag to set/un-set available times! ", style);
desText.x = 50;
desText.y = 550;
app.stage.addChild(desText);













