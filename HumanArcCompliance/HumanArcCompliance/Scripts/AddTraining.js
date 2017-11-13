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
            groups.push(value);
        });
    });
    $scope.groups = groups;
});

$(document).ready(function () {
    
    $('#submit').click(function () {
        var json;
        var groups = [];
        var quiz = {
            title: '',
            description: '',
            media: ''
        }
        var questions = "";
        var answers = [];
        var q = [];
        event.preventDefault();
        quiz.title = $('#trainingTitle').val();
        quiz.description = $('#trainingDesc').val();
        //var media = ;
        $('#groupsApplied > option:selected').each(function (index, value) {
            groups.push(value.id);
        });
        var questionForm = $('#questions > fieldset').each(function (index, value) {
            var question = {
                type: '',
                text: document.getElementById('questionText' + index).value,
                answers: []
            }
            $('input:radio').each(function (i, value) {                
                if (value.name == "content" + index) {                  
                    if (value.checked) {
                        question.type = value.value;
                    }
                }
            });
            
            if (question.type == "multipleChoice") {
                for (var i = 1; i < 5; i++) {
                    var answerid = 'choice' + i + index;
                    var answer = {
                        //id: index,
                        answerText: document.getElementById(answerid).value,
                        isCorrect: 'false'
                    }
                    if (document.getElementById(('is' + i) + index).checked){
                        answer.isCorrect = 'true';
                    }
                    question.answers.push(answer);
                }
            } else if (question.type == "trueFalse") {
                var answer1 = {
                    //id: index,
                    answerText: 'true',
                    isCorrect: 'false'
                }
                if (document.getElementById('trueAnswer' + index).checked) {
                    answer1.isCorrect = 'true';
                }
                question.answers.push(answer1);
                var answer2 = {
                    //id: index,
                    answerText: 'false',
                    isCorrect: 'false'
                }
                if (document.getElementById('falseAnswer' + index).checked) {
                   answer2.isCorrect = 'true';
                }
                question.answers.push(answer2);
            }
            if (questions == "") {
                questions += JSON.stringify(question);
            } else {
                questions += "|" + JSON.stringify(question);
            }
            q.push(question);
            json = {
                title: quiz.title,
                description: quiz.description,
                questions: q
            }
        });
        $.ajax({
            type: 'POST',
            url: '/Training/AddQuiz',
            data: {group:groups.toString(), j:JSON.stringify(json) }, //{group: groups.toString(), quiz: JSON.stringify(quiz), question: questions },
            success: function(data, status) {
                alert("success");
            }
        });
    });

});