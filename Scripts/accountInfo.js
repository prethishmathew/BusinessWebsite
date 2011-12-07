function checkCurrentPageSetting() {

    return;

}

function displayBizWarning() {

    $('#businessAccountSignUp').css('display', 'block');
    $('body').append('<div id="fade"></div>'); 
    $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); 


    return false;
}

function displayConsumerWarning() { 
    $('#businessAccountDisable').css('display', 'block');
    $('body').append('<div id="fade"></div>'); 
    $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); 


    return false;
}

