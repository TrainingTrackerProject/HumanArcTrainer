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

function getAllGroups() {
    $.ajax({
        type: 'GET',
        url: '/Training/GetAllGroups',
        contentType: 'application/json; charset=utf-8',
        success: function (data, status) {
            $.each(data, function (index, value) {
                //alert(value.name);
            });
        },
    })
}

$(document).ready(function () {
    getAllGroups();
});