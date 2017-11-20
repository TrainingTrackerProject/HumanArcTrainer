/*
 * AngularJS code for the Quiz application, called on gradeQuiz.cshtml.
 */

app = angular.module("QuizApp", []);
var json = {
    title: 'my title',
    description: 'myDesc',
    allQuestions: [{
        text: 'HERE IS qUeSTION 1',
        type: 'multipleChoice',
        answers: [{
            text: 'a1',
            isCorrect: true
        },
        {
            text: 'a2',
            isCorrect: false
        },
        {
            text: 'a3',
            isCorrect: false
        },
        {
            text: 'a4',
            isCorrect: false
        }]
    },
    {
        text: 'HERE IS qUeSTION 2',
        type: 'trueFalse',
        answers: [{
            text: 'True',
            isCorrect: true
        },
        {
            text: 'False',
            isCorrect: false
        }]
    },
    {
        text: 'HERE IS qUeSTION 3',
        type: 'shortAnswer'
    }
    ]
}
app.controller("QuizCtrl", function ($scope) {
    $scope.isArray = angular.isArray;
    $scope.json = json;
})

/*var userData = [];
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
}*/