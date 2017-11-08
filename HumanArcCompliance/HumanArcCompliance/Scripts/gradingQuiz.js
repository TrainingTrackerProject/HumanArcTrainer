var userData = [];
$(document).ready(function () {
    getAllUsers();

    $('#gradeEmpTable tbody').on('click', 'tr', function () {

        var data = $('#gradeEmpTable').DataTable().row(this).data()

        alert('You clicked on ' + data[0] + '\'s row');
        //display user quiz table
            //if user quiz is click, display results 

    });
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
        userData.push([value.givenName + " " + value.sn, value.sAMAccountName, value.manager]);
    });
    $('#gradeEmpTable').DataTable({
        data: userData,
        columns: [
            { title: "Name" },
            { title: "Username" },
            { title: "manager" },
        ]
    });
}

