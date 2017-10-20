$(document).ready(function () {
    if ($('#hrCheck').val() == "false") {
        var hrContent = $('.humanResources');
        hrContent.each(function (key, value) {
            value.style.display = 'none';
        });
        if ($('#managerCheck').val() == "false") {
            var managerContent = $('.managers');
            $.each(managerContent, function (key, value) {
                value.style.display = 'none';
            });
        }   
    }
    

});