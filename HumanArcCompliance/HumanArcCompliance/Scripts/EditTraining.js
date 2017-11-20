/* This file gets the data from the Training controller. It fills the table
   created in the 'EditTraining.cshtml' page with data from the Quiz table. */
$(document).ready(function () {
    var quizes = [];
    
    $.ajax({
        url: '/Training/GetAllQuizes',
        type: 'GET',
        success: function (data, status) {
            $.each(data, function (index, value) {
                quizes.push([value.title, value.description, "<input type='button' value='Edit' class='btn btn-primary edit' id='" + value.id + "'/>" + " || " + "<input type='button' value='Remove' class='btn btn-primary remove' id='" + value.id + "'/>"])
            });
            $('#trainingTable').DataTable({
                data: quizes,
                columns: [
                    { title: "Title" },
                    { title: "Description" },
                    { title: "Edit | Remove Training" }
                ]
            });
        }
    }).then(function () {
        $('.remove').on('click', function () {
            $('#trainingTable').DataTable()
                .row($(this).parents('tr'))
                .remove()
                .draw();

            console.log($(this).attr("id"));
            // Remove record
            $.ajax({
                method: 'post',
                url: '/Training/RemoveQuiz',
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ id: $(this).attr("id") }),
                success: function (data, status) {
                    alert("success");
                }
            }).then(function (response) {

            });
        });
    });
});


