
$(document).ready(function () {
    //changes the 'selected' tab
    var url = window.location;
    $('.navbar .nav').find('.active').removeClass('active');
    $('.navbar .nav li a').each(function () {
        if (this.href === url) {
            $(this).parent().addClass('active');
        }
    });
    //allows user to see or not see certain material
    if ($('#hrCheck').val() === "false") {
            var hrContent = $('.humanResources');
            hrContent.each(function (key, value) {
                value.style.display = 'none';
            });
            if ($('#managerCheck').val() === "false") {
                var managerContent = $('.managers');
                $.each(managerContent, function (key, value) {
                    value.style.display = 'none';
                });
            }   
        }
});