/* This file gets the data from the Training controller. It fills the table
   created in the 'EditTraining.cshtml' page with data from the Quiz table. */
$(document).ready(function () {
    var quizes = [];
    
    $.ajax({
        url: '/Training/GetAllQuizes',
        type: 'GET',
        success: function (data, status) {
            $.each(data, function (index, value) {
                quizes.push([value.title, value.description, "<input type='button' value='Edit' class='btn btn-primary edit'/>" + " || " + "<input type='button' value='Remove' class='btn btn-primary remove' id='" + value.id + "'/>"])
            });
            $('#trainingTable').DataTable({
                data: quizes,
                columns: [
                    { title: "Title" },
                    { title: "Description" },
                    {title: "Edit or Remove Training" }
                ]
            });
        }
    }).then(function () {
        
        });


    var id;
    var row;
    $("body").on("click", ".remove", function () {
        $("#confirmRemove").modal('show');
        id = $(this).attr("id");
        row = $(this);
    });

    $('#removeQuizBtn').on('click', function () {
        $("#confirmRemove").modal('hide');
        $('#trainingTable').DataTable()
            .row(row.parents('tr'))
            .remove()
            .draw();

        console.log($(this).attr("id"));
        // Remove record
        $.ajax({
            method: 'post',
            url: '/Training/RemoveQuiz',
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify({ id: id }),
            success: function (data, status) {
            }
        }).then(function (response) {

        });
    });
});


