/*
 * AngularJS code for the Quiz application, called on Quiz.cshtml.
 */
var app = angular.module('addQuestionApp', ['ngRoute']);
var jsonData;

//////////////////////////////////////////////////////////////
//Change this json to real data later
var json = {
    title: 'my title',
    description: 'myDesc',
    allQuestions: [{
        text: 'question1',
        type: 'multipleChoice',
        answers: [{
            text: 'a1',
            isCorrect: true
        },
        {
            text: 'a2',
            isCorrect: false
        }
        ]
    },
    {
        text: 'question2',
        type: 'multipleChoice',
        answers: [{
            text: 'a3',
            isCorrect: true
        },
        {
            text: 'a4',
            isCorrect: false
        }
        ]
    }
    ]
}
/////////////////////////////////////////////////////////////

app.controller('addQuizController', function ($scope, $http) {
    $scope.json = json;
    $scope.name = "quiz"
    var groups = [];
    $http.get('/Training/GetAllGroups').then(function (data) {
        $.each(data.data, function (index, value) {
            groups.push(value);
        });
    });
    $scope.groups = groups;
    var id = document.getElementById('quizId').innerHTML;
    $.ajax({
        url: '/Training/GetQuizById',
        type: 'POST',
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ id: id }),
        success: function (data, status) {
            jsonData = data;
            document.getElementById("trainingTitle").value = jsonData.title;
            document.getElementById("trainingDesc").value = jsonData.description;
            //for (var i = 0; i < data.questions.length; i++) {
            //    $scope.choiceSet.choices.push('');
            //}
            var fullDiv = $('<div>');
            var titleRow = $('<div>', {id: "titleRow", "class":"row"});
            var descRow = $('<div>', { id: "descRow", "class": "row" });
            var title = $('<div>', { id: "quizTitle", "class": "col-sm-8" }).text(data.title);
            titleRow.append(title);
            var desc = $('<div>', { id: "quizDesc", "class": "col-sm-8" }).text(data.description);
            descRow.append(desc);
            fullDiv.append(titleRow);
            fullDiv.append(descRow);
            $.each(data.questions, function (index, value) {
                var questionRow = $('<div>', { id: "questionRowindex" + index, "class": "row" });
                var question = $('<div>', { id: "question||" + index, "class": "col-sm-8" }).text(value.text);
                if (value.type == "multipleChoice" || value.type == "trueFalse") {
                    var ansRow1 = $('<div>', { id: "ansRow1index" + index, "class": "row" });
                    $.each(value.answers, function (innerIndex, innerValue) {
                        var colDiv = $('<div>', { id: "colDiv", "class": "col-sm-3" });
                        var answer = $('<div>', { id: "answerindex" + index + "innerindex" + innerIndex, "class": "answer" + innerValue.id }).text(innerValue.answerText);
                        var inputId = "inputindex" + index + "innerindex" + innerIndex;

                        var input = $('<input id="'+inputId+'" type="radio" name="question'+index+'">', { "class": "form-control" });
                        colDiv.append(answer);
                        colDiv.append(input);
                        ansRow1.append(colDiv);
                    });
                    question.append(ansRow1);
                } else {
                   var ansRow1 = $('<div>', { id: "ansRow1index" + index, "class": "row" });
                    var answer = $('<input>', { id: "answerindex" + index+"shortanswer", type: "text", "class": "col-sm-8" })
                    ansRow1.append(answer);
                    question.append(ansRow1);
                }
                questionRow.append(question);
                fullDiv.append(questionRow);
            });
            $('#testDiv').append(fullDiv);
        }
    });
});

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
    //$scope.$watch(function () {

    //    for (var i = 0; i < jsonData.questions.length; i++) {
    //        document.getElementById("questionText" + i).value = jsonData.questions[i].text;
    //        if (jsonData.questions[i].type == "multipleChoice") {
    //            //document.getElementById("multipleChoice" + i).checked = true;
    //            for (var j = 1; j <= jsonData.questions[i].answers.length; j++) {
    //                document.getElementById("choice" + j + i).value = jsonData.questions[i].answers[j-1].answerText;
    //            }
    //        }
    //    }

    //});

   
});

$(document).ready(function () {
    
    $('#submitQuiz').on('click', function () {
        event.preventDefault();
        var quiz = {
            id: jsonData.id,
            questions: []
        }
        for (var i = 0; i < jsonData.questions.length; i++) {
            var question = {
                id: '',
                answers: []
            }
            if (jsonData.questions[i].type == "multipleChoice" || jsonData.questions[i].type == "trueFalse") {
                for (var j = 0; j < jsonData.questions[i].answers.length; j++) {
                    var id = "#inputindex" + i + "innerindex" + j;
                    if ($('#inputindex' + i + 'innerindex' + j + '').is(":checked")) {
                        var answer = {
                            id: ''
                        }
                        answer.id = jsonData.questions[i].answers[j].id;
                        question.answers.push(answer);
                        question.id = jsonData.questions[i].id;
                    }
                } 
            } else {
                var answer = {
                    id: '',
                    text: ''
                }
                answer.id = jsonData.questions[i].id;
                answer.text = $('#answerindex' + i + 'shortanswer').val();
                question.answers.push(answer);
                question.id = jsonData.questions[i].id;
            }
            quiz.questions.push(question);
        }
        $.ajax({
            url: '/Training/SubmitQuiz',
            type: 'POST',            
            data: { data: JSON.stringify(quiz)},
            success: function () {
                alert("test submitted view the training page to see that is has moved to taken");
            }
        });
    });
});
