/*
 * AngularJS code for the Quiz application, called on Quiz.cshtml.
 */

/*
 * TODO: You need to have the JSON for both the user answers and the accepted answers.
 * Also need the firstName and lastName of the user.
 * Add on to getQuizById with user answers in TrainingController
 */

var app = angular.module('QuizApp', ['ngRoute']);
app.controller('QuizCtrl', function ($scope, $http) {
    $http.get('/Training/GetQuizById/' + ('#quizId').val()).then(function (response) {
        $scope.userQuiz = response.data;
        alert($scope.userQuiz);
    });
});