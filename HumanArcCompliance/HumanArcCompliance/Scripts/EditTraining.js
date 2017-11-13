
$(document).ready(function () {
    var quizes = [];
    var remove = "<input type= 'button' value= 'Remove' class='btn btn-primary' ng- click='removeRow(company.name)'/>";
    $.ajax({
        url: '/Training/GetAllQuizes',
        type: 'GET',
        success: function (data, status) {
            $.each(data, function (index, value) {
                quizes.push([value.title, value.description, remove])
            });
            $('#trainingTable').DataTable({
                data: quizes,
                columns: [
                    { title: "Title" },
                    { title: "Description" },
                    {title: "Remove Quiz" }
                ]
            });
        }
    });
});


