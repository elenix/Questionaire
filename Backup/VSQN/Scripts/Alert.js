function ShowMessage(message, messagetype) {
    var cssclass;
    var word;
    switch (messagetype) {
        case 'Success':
            cssclass = 'alert-success'
            word = 'Done'
            break;
        case 'Error':
            cssclass = 'alert-danger'
            word = 'Warning'
            break;
        case 'Warning':
            cssclass = 'alert-warning'
            word = 'Info'
            break;
        default:
            cssclass = 'alert-info'
            word = 'Info'
    }

    $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + word + '!</strong> <span>' + message + '</span></div>');
  
    $("#alert_div").fadeTo(3000, 500).slideUp(500, function () {
        $("#alert_div").alert('close');
    });

}

