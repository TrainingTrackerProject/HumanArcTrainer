/*
 * AngularJS code for the Quiz application, called on Quiz.cshtml.
 */

//Stores the JSON data in quiz

$(document).ready(function () {

    //var quizId = $('#quizId').val();

    //$.ajax({
    //    url: '/Training/TakeQuiz',
    //    type: "GET",
    //    data: {  id: quizId},
    //    contextType: "application/json",
    //    success: function (res) {
    //        console.log(res);
    //    }
    //});
});

var app = angular.module('QuizApp', ['ngRoute']);
app.controller('QuizCtrl', function ($scope, $http) {
    ///Complete JSON to be sent to server
    var currentQuestion = 0;
    var data;

    var submittedAnswers = [];
 

    $scope.quiz = {
        answer: 0,
        quizId: 0,
        question: {
            questionId: 0,
            questionText: '',
            type: '',
            answers: []
        }       
    }

    var quizId = $('#quizId').val();

    var config = {
        headers: {
            'Content-Type': 'application/json;'
        }
    }
    console.log(quizId);
    $http.post('/Training/TakeQuiz',
        { id: quizId }, config)
        .then(function (res) {
            data = res.data;
            console.log(data);
        });

    $scope.start = function () {
            currentQuestion = 0;
            $scope.quiz.question.text = data.questions[currentQuestion].text;
            $scope.quiz.question.type = data.questions[currentQuestion].type;
            $scope.quiz.question.answers = [];

            $.each(data.questions[currentQuestion].answers, function (index, value) {
                var answer = {
                    id: value.id,
                    text: value.text,
                }

                $scope.quiz.question.answers.push(answer);
            });
            console.log($scope.quiz.question.answers);
        }      

    $scope.next = function () {
        if (typeof submittedAnswers[currentQuestion] === 'undefined') {
            var uqqa = {
                quizId: quizId,
                questionId: angular.copy($scope.quiz.question.questionId),
                answerId: angular.copy($scope.quiz.answer)
            }
            submittedAnswers.push(uqqa);
        } else {
            submittedAnswers[currentQuestion].answerId = angular.copy($scope.quiz.answer);
        }

        currentQuestion++;
        $scope.quiz.question.text = data.questions[currentQuestion].text;
        $scope.quiz.question.type = data.questions[currentQuestion].type;
        $scope.quiz.question.answers = [];
        $.each(data.questions[currentQuestion].answers, function (index, value) {
            var answer = {
                id: value.id,
                text: value.text,
            }
            $scope.quiz.question.answers.push(answer);
        });
        console.log($scope.quiz.question.answers);

    }

    $scope.previous = function () {
        if (typeof submittedAnswers[currentQuestion] === 'undefined') {
            var uqqa = {
                quizId: quizId,
                questionId: angular.copy($scope.quiz.question.questionId),
                answerId: angular.copy($scope.quiz.answer)
            }
            submittedAnswers.push(uqqa);
        } else {
            submittedAnswers[currentQuestion].answerId = angular.copy($scope.quiz.answer);
        }
        if (currentQuestion > 0) {
            currentQuestion--;
        }

        $scope.quiz.question.text = data.questions[currentQuestion].text;
        $scope.quiz.question.type = data.questions[currentQuestion].type;
        $scope.quiz.question.answers = [];
        $.each(data.questions[currentQuestion].answers, function (index, value) {
            var answer = {
                id: value.id,
                text: value.text,
            }
            $scope.quiz.question.answers.push(answer);
        });
        console.log($scope.quiz.question.answers);
    }

    $scope.submit = function () {
        console.log(submittedAnswers);
        //$http.push('Training/SubmitQuiz', { answers: submittedAnswers }, config)
        //    .then(function (res) {
        //        console.log(res);
        //    });
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////


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