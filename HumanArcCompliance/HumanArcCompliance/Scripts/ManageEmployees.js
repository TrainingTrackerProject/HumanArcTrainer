
var userData = [];
$(document).ready(function () {
    getAllUsers();
    $('#employeeTable tbody').on('click', 'tr', function () {

        var data = $('#employeeTable').DataTable().row(this).data()

        alert('You clicked on ' + data[0] + '\'s row');

    });
});

getAllUsers = function () {
    if (document.getElementById("hrCheck").value == "True") {
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
    } else {
        $.ajax({
            url: '/Training/GetAllManagersUsers',
            type: 'GET',
            cache: false,
            success: function (data, status) {
                fillDataTable(data);
                
            },
            error: function () {
            }
        });
    }
}

fillDataTable = function (users) {
    if (document.getElementById("hrCheck").value == "true") {
        $.each(users, function (index, value) {
            if (value.hasUngradedQuiz) {
                userData.push([value.id, value.firstName + " " + value.lastName, value.SAMAccountName, value.manager, '<span class="glyphicon glyphicon-time"></span>']);
            } else {
                userData.push([value.id, value.firstName + " " + value.lastName, value.SAMAccountName, value.manager, '<span class="glyphicon glyphicon-ok"></span>']);
            }
        });
        $('#employeeTable').DataTable({
            data: userData,
            columns: 
            [
                { title: "id", visible: false },
                { title: "Name" },
                { title: "Username" },
                { title: "manager" },
                { title: "Graded" }
            ]
        });
        
    } else {
        $.each(users, function (index, value) {
            userData.push([value.id, value.firstName + " " + value.lastName, value.SAMAccountName, value.manager]);
        });
        $('#employeeTable').DataTable({
            data: userData,
            columns: [
                { title: "id", visible: false},
                { title: "Name" },
                { title: "Username" },
                { title: "manager" }
            ]
        });
    }
    $('#employeeTable tbody tr').on('click', function () {
        var data = $('#employeeTable').DataTable().row(this).data();
        window.location.href = "/Training/EmployeeQuizes/?id=" + data[0];
    });
}