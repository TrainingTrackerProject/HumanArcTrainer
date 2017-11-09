// This lets the HR member look for media files when "Browse" is clicked.
$(document).on('click', '.browse', function () {
    var file = $(this).parent().parent().parent().find('.hide-file');
    file.trigger('click');
});
$(document).on('change', '.hide-file', function () {
    $(this).parent().find('.form-control').val($(this).val().replace(/C:\\fakepath\\/i, ''));
});

// Allows HR to add multiple quiz questions

var app = angular.module('addQuestionApp', ['ngRoute']);

app.controller('addQuestionController', function ($scope, $http) {
    $scope.choiceSet = { choices: [] };
    $scope.quest = {};

    $scope.choiceSet.choices = [];
    $scope.addNewChoice = function () {
        $scope.choiceSet.choices.push('');
    };

    $scope.removeChoice = function (z) {
        //var lastItem = $scope.choiceSet.choices.length - 1;
        $scope.choiceSet.choices.splice(z, 1);
    };
});

app.controller('addQuizController', function ($scope, $http) {
    $scope.name = "quiz"
    var groups = [];
    $http.get('/Training/GetAllGroups').then(function (data) {
        $.each(data.data, function (index, value) {
            groups.push(value.name);
        });
    });
    $scope.groups = groups;
});