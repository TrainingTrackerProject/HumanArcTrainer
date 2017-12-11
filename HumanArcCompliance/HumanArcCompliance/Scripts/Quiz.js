/*
 * AngularJS code for the Quiz application, called on Quiz.cshtml.
 */
$(document).ready(function () {

});

var app = angular.module('QuizApp', ['ngRoute']);
app.controller('QuizCtrl', function ($scope, $http) {
    var data;
    var submittedAnswers = [];
    $scope.status = {
        isTaken: false,
        started: false,
        isFirstQuestion: true,
        isLastQuestion: false,
        isSubmitted: false,
        text: ''
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
    $http.post('/Training/ViewQuiz', JSON.stringify({ id: quizId }), config).then(function (res) {
        data = res.data;
        if (data.isTaken == true) {
            $scope.status.isTaken = true;
            $scope.status.started = true;
            if (typeof submittedAnswers[$scope.quiz.currentQuestion + 1] === 'undefined') {
                $scope.quiz.lastQuestion = true;
            }
            $scope.quiz.currentQuestion = 0;
            setScope();

            setPossibleAnswers();
            $.each(res.data.juqqas, function (index, value) {
                var answer = {
                    answerId: value.answerId,
                    answerText: value.text
                }
                submittedAnswers.push(answer)
            })
            if (typeof submittedAnswers[$scope.quiz.currentQuestion + 1] === 'undefined') {
                $scope.status.isLastQuestion = true;
            }
            if ($scope.quiz.question.type != 'shortAnswer') {
                console.log($scope.quiz.currentQuestion);
                console.log(submittedAnswers);
                $scope.quiz.question.selectedAnswer = submittedAnswers[$scope.quiz.currentQuestion].answerId
            }
            else {
                $scope.quiz.question.answerText = submittedAnswers[$scope.quiz.currentQuestion].answerText
            }
        }
    });

    $scope.start = function () {
        $scope.status.started = true;
        $scope.quiz.currentQuestion = 0;
        if (typeof data.questions[$scope.quiz.currentQuestion + 1] === 'undefined') {
            $scope.status.isLastQuestion = true;
        }
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
            if ($scope.quiz.question.type != 'shortAnswer') {
                $scope.quiz.question.selectedAnswer = submittedAnswers[$scope.quiz.currentQuestion].answerId
            }
            else {
                $scope.quiz.question.answerText = submittedAnswers[$scope.quiz.currentQuestion].answerText
            }
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
        if ($scope.quiz.question.type != 'shortAnswer') {
            $scope.quiz.question.selectedAnswer = submittedAnswers[$scope.quiz.currentQuestion].answerId
        }
        else {
            $scope.quiz.question.answerText = submittedAnswers[$scope.quiz.currentQuestion].answerText
        }   
    }

    $scope.setLastQuestion = function () {
        setQuestionAnswer();
    }

    $scope.submit = function () {
        $('#confirm-submit').modal('hide');
        $scope.status.isSubmitted = true;
        $http.post('/Training/SubmitQuiz', JSON.stringify({ answers:submittedAnswers }), config).then(function (res) {
            $scope.status.text = res.data;
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
                submittedAnswers.push(uqqa);
            } else {
                submittedAnswers[$scope.quiz.currentQuestion].answerId = angular.copy($scope.quiz.question.selectedAnswer);
            }
        }  
        else if ($scope.quiz.question.type == 'shortAnswer' && $scope.quiz.question.answerText != '') {
            if (typeof submittedAnswers[$scope.quiz.currentQuestion] === 'undefined') {
                var uqqa = {
                    quizId: quizId,
                    questionId: angular.copy($scope.quiz.question.questionId),
                    answerId: angular.copy($scope.quiz.question.answers[0].id),
                    answerText: angular.copy($scope.quiz.question.answerText)
                }
                submittedAnswers.push(uqqa);
            }
            else {
                submittedAnswers[$scope.quiz.currentQuestion].answerText = angular.copy($scope.quiz.question.answerText);
            }
        }
    }

    function setScope() {
        console.log(data);
        console.log(data.questions[$scope.quiz.currentQuestion]);
        $scope.quiz.question.questionId = data.questions[$scope.quiz.currentQuestion].id;
        $scope.quiz.question.questionText = data.questions[$scope.quiz.currentQuestion].text;
        $scope.quiz.question.type = data.questions[$scope.quiz.currentQuestion].type;
        $scope.quiz.question.answerText = '';
        $scope.quiz.question.selectedAnswer = 0;
        $scope.quiz.question.answers = [];
    }
    
});

