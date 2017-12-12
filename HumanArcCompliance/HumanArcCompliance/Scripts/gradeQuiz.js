/*
 * AngularJS code for the GradeQuiz application, called on GradeQuiz.cshtml.
 */
var app = angular.module('QuizApp', ['ngRoute']);
app.controller('QuizCtrl', function ($scope, $http) {
    var submittedAnswers = [];
    var questionIterator = 0;
    $scope.data;
    $scope.quiz = {
        iterator: 0,
        answer: 0,
        quizId: 0,
        questions: [{
            questionId: 0,
            questionText: '',
            type: '',
            answerText: '',
            isApproved: false
        }]
    }
    var quizId = $('#quizId').val();
    var config = {
        headers: {
            'Content-Type': 'application/json;'
        }
    }
    $http.post('/Training/ViewGradeQuiz', JSON.stringify({ id: quizId }), config).then(function (res) {
        $scope.quiz.questions.splice(0,1)
        $scope.data = res.data;
        for (var i = 0; i < $scope.data.questions.length; i++) {
            if ($scope.data.questions[i].type == 'shortAnswer') {
                for (var j = 0; j < $scope.data.juqqas.length; j++) {
                    if ($scope.data.questions[i].id === $scope.data.juqqas[j].questionId) {
                        var question = $scope.data.questions[i];
                        var juqqa = $scope.data.juqqas[j];
                        $scope.quiz.questions.push({
                            iterator: i,
                            questionId: question.id,
                            questionText: question.text,
                            type: 'shortAnswer',
                            answerText: juqqa.text
                        });
                        questionIterator++;
                    }
                }
            }
            /*var answer = {
                answerId: value.answerId,
                answerText: value.text
            }*/
        }
    });

    $scope.accept = function (i) {
        $scope.quiz.questions[i].isApproved = true;
    }
    $scope.reject = function (i) {
        $scope.quiz.questions[i].isApproved = false;
    }
    $scope.submit = function () {
        $('#confirm-submit').modal('hide');
        $http.post('/Training/SubmitGrade', { answers: JSON.stringify(/*TODO*/) }, config).then(function (res) {
        });
    }
    /*
    $scope.submit = function () {
        $('#confirm-submit').modal('hide');
        $scope.status.isSubmitted = true;
        $http.post('/Training/SubmitGrading', { answers: JSON.stringify(submittedAnswers) }, config).then(function (res) {
            $scope.status.text = res.data;
        });
    }

    function displayShortAnswerQuestions() {
        $scope.quiz.questions.forEach(function (question) {
            question.questionId = data.question.id;
            question.questionText = data.question.text;
            question.type = data.question.type;
            question.answerText.push(data.question.answerText);
        })
    }
    //function setpossibleanswers() {
    //    $.each(data.questions[$scope.quiz.currentquestion].answers, function (index, value) {
    //        var answer = {
    //            id: value.id,
    //            text: value.answertext,
    //        }
    //        $scope.quiz.question.answers.push(answer);
    //    });
    //}

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
        $scope.quiz.questions
        $scope.quiz.question.questionId = data.questions[$scope.quiz.currentQuestion].id;
        $scope.quiz.question.questionText = data.questions[$scope.quiz.currentQuestion].text;
        $scope.quiz.question.type = data.questions[$scope.quiz.currentQuestion].type;
        $scope.quiz.question.answerText = '';
        $scope.quiz.question.selectedAnswer = 0;
        $scope.quiz.question.answers = [];
    }*/
});

