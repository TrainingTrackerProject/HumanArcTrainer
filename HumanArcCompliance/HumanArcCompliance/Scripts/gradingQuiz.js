var userData = [];
$(document).ready(function () {
    $('#employeGradeTable tbody').on('click', 'tr', function () { 
        var data = $('#employeGradeTable').DataTable().row(this).data()
        alert('You clicked on ' + data[0] + '\'s row');
    });
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
            },
            error: function () {
            }
        });
}

fillDataTable = function (users) {
    $.each(users, function (index, value) {
        userData.push([value.firstName + " " + value.lastName, value.SAMAccountName, value.manager]);
    });
    $('#employeGradeTable').DataTable({
        data: userData,
        columns: [
            { title: "Name" },
            { title: "Username" },
            { title: "manager" },
        ]
    });
}