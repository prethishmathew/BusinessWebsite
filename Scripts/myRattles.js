var lagtime = 5000;
var max = 15;
var display = 9;
var inRotation = true;
var rotator;
/*var n = max;
var j = display;

*/
var ua = $.browser;

var n = display + 1;
var j = 1;
function removeNaddLi(cnter) {
    return function () {
        if (ua.msie) {
            $('#rattle' + cnter).remove().css('display', 'none').css('opacity', 'none').prependTo('#myRattles');
        }
        else
        {
            $('#rattle' + cnter).remove().css('display', 'none').css('opacity', '1').prependTo('#myRattles');
        }
    }

}
function slider() {
    //$('#rattle' + n).css("opacity", "0.7").slideDown(1000).fadeTo('3000', 'none');
    $('#rattle' + n).slideDown(1000);
    //$('#rattle' + j).css("opacity", "0.7").slideUp(1000, removeNaddLi(j));
    $('#rattle' + j).slideUp(1000, removeNaddLi(j));
    n++; j++;
    
    if (n > max) {
        n = 1;
    }

    if (j > max) {
        j = 1;
    }

    //WriteToFile(n, j);
    rotator = setTimeout('slider()', lagtime);
}

$(document).ready(function () {

    displayRecs();
    rotator = setTimeout('slider()', lagtime);
    $(".myRattles").hover(function () {
        clearTimeout(rotator);
        inRotation = false;
    },
    function () {
        if (inRotation == false) {
            rotator = setTimeout('slider()', lagtime);
            inRotation = true;
        }
    });
});

function displayRecs() {

    //$(".myRattles>ul>li").css({ 'opacity': 100 });
    for (cnt = 1; cnt <= display; cnt++) {
        $("#rattle" + cnt).css('display', 'block');
    }
    

}

function createSlider(lgTime,  totalRecs,disRecs) {

    
    lagtime = lgTime;
    display = disRecs;
    max = totalRecs;
    displayRecs();

}

function WriteToFile(n,j) {

    $("#pageLeft").html('<br/> n is ' + n + '  j is ' + j);
}


function getLikes() {
    return Math.random;  
}