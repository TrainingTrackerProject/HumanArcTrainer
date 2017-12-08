﻿/*
 * AngularJS code for the Quiz application, called on Quiz.cshtml.
 */
$(document).ready(function () {

});

var app = angular.module('QuizApp', ['ngRoute']);
app.controller('QuizCtrl', function ($scope, $http) {
    var data;
    var submittedAnswers = [];
    $scope.status = {
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
    $http.post('/Training/TakeQuiz',
        { id: quizId }, config)
        .then(function (res) {
            data = res.data;
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
        $http.post('/Training/SubmitQuiz', { answers: JSON.stringify(submittedAnswers) }, config)           
            .then(function (res) {
                console.log(res.data);
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
        $scope.quiz.question.questionId = data.questions[$scope.quiz.currentQuestion].id;
        $scope.quiz.question.questionText = data.questions[$scope.quiz.currentQuestion].text;
        $scope.quiz.question.type = data.questions[$scope.quiz.currentQuestion].type;
        $scope.quiz.question.answerText = '';
        $scope.quiz.question.selectedAnswer = 0;
        $scope.quiz.question.answers = [];
    }

   
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

