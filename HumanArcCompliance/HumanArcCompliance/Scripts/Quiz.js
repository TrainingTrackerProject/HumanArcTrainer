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
    //    }
    //});
});

var app = angular.module('QuizApp', ['ngRoute']);
app.controller('QuizCtrl', function ($scope, $http) {
    ///Complete JSON to be sent to server
    //var currentQuestion = 0;
    var data;

    var submittedAnswers = [];

    $scope.status = {
        started: false,
        isFirstQuestion: true,
        isLastQuestion: false,
        isSubmitted: false
    }

    $scope.quiz = {     
        answer: 0,
        quizId: 0,
        question: {
            questionId: 0,
            questionText: '',
            type: '',
            answers: [],
            selectedAnswer: 0,
            answerText: ''
        },
        currentQuestion: 0
        
    }

    var quizId = $('#quizId').val();

    var config = {
        headers: {
            'Content-Type': 'application/json;'
        }
    }
    $http.post('/Training/TakeQuiz',
        { id: quizId }, config)
        .then(function (res) {
            data = res.data;
            console.log(data);
        });

    $scope.start = function () {
        $scope.status.started = true;
        if (typeof submittedAnswers[$scope.quiz.currentQuestion + 1] === 'undefined') {
            $scope.quiz.lastQuestion = true;
        }
        $scope.quiz.currentQuestion = 0;
        setScope();
        setPossibleAnswers();
    }

    $scope.next = function () {
        $scope.status.isFirstQuestion = false;

        setQuestionAnswer();

        $scope.quiz.currentQuestion++;

        if (data.questions.length - 1 == $scope.quiz.currentQuestion) {
            $scope.status.isLastQuestion = true;
        }
        setScope();
        setPossibleAnswers();

        if (typeof submittedAnswers[$scope.quiz.currentQuestion] != 'undefined') {
            console.log($scope.quiz.currentQuestion);
            console.log("going forward id: " + submittedAnswers[$scope.quiz.currentQuestion].answerId);
            $scope.quiz.question.selectedAnswer = submittedAnswers[$scope.quiz.currentQuestion].answerId
            $scope.quiz.question.answerText = submittedAnswers[$scope.quiz.currentQuestion].answerText
        }
    }

    $scope.previous = function () {
        $scope.status.isLastQuestion = false;

        setQuestionAnswer();

        $scope.quiz.currentQuestion--; 
        
        if ($scope.quiz.currentQuestion == 0) {
            $scope.status.isFirstQuestion = true;
        }

        setScope();      
        setPossibleAnswers();

        $scope.quiz.question.selectedAnswer = submittedAnswers[$scope.quiz.currentQuestion].answerId
        console.log("going previous id: " + submittedAnswers[$scope.quiz.currentQuestion].answerId);
        $scope.quiz.question.answerText = submittedAnswers[$scope.quiz.currentQuestion].answerText
       
    }

    $scope.setLastQuestion = function () {
        setQuestionAnswer();
    }

    $scope.submit = function () {
        $http.post('/Training/SubmitQuiz', { answers: JSON.stringify(submittedAnswers) }, config)           
            .then(function (res) {
                console.log(res);
            });
    }

    function setPossibleAnswers() {     
        $.each(data.questions[$scope.quiz.currentQuestion].answers, function (index, value) {
            var answer = {
                id: value.id,
                text: value.answerText,
            }
            $scope.quiz.question.answers.push(answer);
        }); 
        console.log("current question number from setPossibleAnswers: " + $scope.quiz.currentQuestion);

    }

    function setQuestionAnswer() {
        if ($scope.quiz.question.selectedAnswer != 0) {
            if (typeof submittedAnswers[$scope.quiz.currentQuestion] === 'undefined') {
                var uqqa = {
                    quizId: quizId,
                    questionId: angular.copy($scope.quiz.question.questionId),
                    answerId: angular.copy($scope.quiz.question.selectedAnswer),
                    answerText: ''
                }
                console.log("current question : " + $scope.quiz.currentQuestion + "-------- selected answer : " + $scope.quiz.question.selectedAnswer)
                if ($scope.quiz.question.type == 'shortAnswer') {
                    uqqa.answerId = angular.copy($scope.quiz.question.answers[0].id);
                    console.log("new set id: " + uqqa.answerId);
                    uqqa.answerText = angular.copy($scope.quiz.question.answerText);
                    console.log("new set text: " + uqqa.answerText);
                }
                submittedAnswers.push(uqqa);
            } else {
                console.log("the testing id " + submittedAnswers[$scope.quiz.currentQuestion].answerId)
                console.log("id being set it " + $scope.quiz.question.selectedAnswer);
                submittedAnswers[$scope.quiz.currentQuestion].answerId = angular.copy($scope.quiz.question.selectedAnswer);
                if ($scope.quiz.question.type == 'shortAnswer') {
                    submittedAnswers[$scope.quiz.currentQuestion].answerText = angular.copy($scope.quiz.question.answerText);
                }
            }
            console.log(submittedAnswers);
        }     
    }

    function setScope() {
        $scope.quiz.question.questionId = data.questions[$scope.quiz.currentQuestion].id;
        $scope.quiz.question.questionText = data.questions[$scope.quiz.currentQuestion].text;
        $scope.quiz.question.type = data.questions[$scope.quiz.currentQuestion].type;
        $scope.quiz.question.answerText = '';
        $scope.quiz.question.selectedAnswer = 0;
        $scope.quiz.question.answers = [];
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