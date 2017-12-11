$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

//Dataset for To Do list for My Training

//Counts number of quizzes user has to still complete
$.ajax({
    url: '/Training/GetUserQuizes',
    type: 'GET',
    contentType: "application/json",
    success: function (data, status) {
        var counter = 0;
        $.each(data, function (index, value) {
            if (!value.isGraded && !value.isCompleted) {
                counter++;  
            }
        });  

        $('#quizNum').html(counter);


    }
});

