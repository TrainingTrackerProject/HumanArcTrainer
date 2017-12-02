﻿/*
 * AngularJS code for the Quiz application, called on Quiz.cshtml.
 */

//Stores the JSON data in quiz
var app = angular.module('QuizApp', ['ngRoute']);
app.controller('QuizCtrl', function ($scope, $http) {
    //Gets Quiz information
    ///currently broken

    //now obsolete
    //$http.get('/Training/GetQuizById/' + ('#quizId').val()).then(function (response) {
    //    $scope.quiz = response.data;
    //    alert($scope.quiz);
    //});

    //Submits user quiz information
    ///Complete JSON to be sent to server
    var sentJson = {
        questionText: '',
        questionType: '',
        answers: []
    };
    $scope.questionData = {}

    ///User Answers
    $scope.userMcAnswers = [
        answer1 = {
            text: ''
        },
        answer2 = {
            text: ''
        },
        answer3 = {
            text: ''
        },
        answer4 = {
            text: ''
        }
    ];

    $scope.userTfAnswers = [
        answer1 = {
            text: ''
        }
    ];
    $scope.userSaAnswers = [
        answer1 = {
            text: ''
        }
    ]

    ///////////YO LOOK HERE THIS IS TODO. COPY-PASTED OVER BS.
    //////////FOR USERQUIZQUESTIONANSWERS YOU NEED: userId, quizId, questionId, answerId, text, isChecked, isApproved
    //////////JUST USE $scope.quiz.id AND STUFF
    $scope.submitQuiz = function () {
        $http.post('/Training/AddUserQuizQuestionAnswers',
            { title: document.getElementById("trainingTitle").value, questionData: JSON.stringify(sentJson) }, config)
            .then(
                function (response) {
                    // success callback
                },
                function (response) {
                    // failure callback
                }
            );
    }
});


/*
 * TODO: use some kind of $http.post to submit to the database. Looks like this:
 * var app = angular.module('myApp',[]);
    app.controller('bookController',function($scope,$http){
    $scope.insertData=function(){
    $http.post("insert.php", {
		'bname':$scope.bname,
		'bauthor':$scope.bauthor,
		'bprice':$scope.bprice,
		'blanguage':$scope.blanguage})

    .success(function(data,status,headers,config){
    console.log("Data Inserted Successfully");
    });
    }
   });
 */


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