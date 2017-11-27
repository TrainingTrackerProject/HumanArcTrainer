/*
 * AngularJS code for the Quiz application, called on Quiz.cshtml.
 */

//Dataset for To Do list for My Training

//Need: title, description, allQuestions {text, type, answers[text, isCorrect]}
var id = '@ViewBag.id'
alert(id);

var app = angular.module('QuizApp', []);
app.controller('QuizCtrl', function ($scope, $http) {
    $http.get('/Training/GetQuizById/'+('#quizId').val()).then(function (response) {
        $scope.quiz = response.data;
        alert($scope.quiz);
    });
});

/*
app = angular.module("QuizApp", []);

app.controller("QuizCtrl", function ($scope) {
    $scope.isArray = angular.isArray;
    $scope.json = json;
})

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
                    { title: "Edit or Remove Training" }
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
*/

/*
app = angular.module("QuizApp", []);
var json = {
    title: 'my title',
    description: 'myDesc',
    allQuestions: [{
        text: 'This is question 1',
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
        text: 'This is question 2',
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
        text: 'This is question 3',
        type: 'shortAnswer'
    }
    ]
}
app.controller("QuizCtrl", function ($scope) {
    $scope.isArray = angular.isArray;
    $scope.json = json;
})*/