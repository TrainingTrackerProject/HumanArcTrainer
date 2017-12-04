/*
 * AngularJS code for the Quiz application, called on Quiz.cshtml.
 */

//Stores the JSON data in quiz
var app = angular.module('QuizApp', ['ngRoute']);
app.controller('QuizCtrl', function ($scope, $http) {
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