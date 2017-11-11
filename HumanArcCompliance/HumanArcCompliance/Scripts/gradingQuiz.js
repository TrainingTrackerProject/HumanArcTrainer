var userData = [];
$(document).ready(function () {
    getAllUsers();

    
});

getAllUsers = function () {
        $.ajax({
            url: '/Training/GetAllUsers',
            type: 'GET',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            success: function (data, status) {
                fillDataTable(data);

                
                $('#employeeGradeTable tbody tr').on('click', function () {                  
                    var data = $('#employeeGradeTable').DataTable().row(this).data();
                    window.location.href = "/Training/EmployeeQuizes/?id=" + data[0];
                    //$.ajax({
                    //    url: '/Training/EmployeeQuizes',
                    //    Type: 'POST',
                    //    data: { id: data[0] }
                    //});
                });
            },
            error: function () {
            }
        });
}

fillDataTable = function (users) {
    $.each(users, function (index, value) {
        userData.push([value.id, value.firstName + " " + value.lastName, value.SAMAccountName, value.manager]);
    });
    $('#employeeGradeTable').DataTable({
        data: userData,

        columns: [
            { title: "id", visible: false },
            { title: "Name" },
            { title: "Username" },
            { title: "manager" }
        ]
    });
}