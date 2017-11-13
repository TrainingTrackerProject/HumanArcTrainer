var app = angular.module('updateQuestionApp', ['ngRoute']);

app.controller('updateGroupController', function ($scope, $http) {
    $scope.name = "quiz"
    var groups = [];
    $http.get('/Training/GetAllGroups').then(function (data) {
        $.each(data.data, function (index, value) {
            groups.push(value);
        });
    });
    $scope.groups = groups;
});

app.controller('updateQuestionController', function ($scope, $http) {
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

    $http.push
});

$(document).ready(function () {
    $.ajax({
        url: ''
    });
});

