//This displays the datepicker
$('#sandbox-container input').datepicker({
    autoclose: true
});

$('#sandbox-container input').on('show', function (e) {

    if (e.date) {
        $(this).data('stickyDate', e.date);
    }
    else {
        $(this).data('stickyDate', null);
    }
});

$('#sandbox-container input').on('hide', function (e) {
    var stickyDate = $(this).data('stickyDate');

    if (!e.date && stickyDate) {
        console.debug('restore stickyDate', stickyDate);
        $(this).datepicker('setDate', stickyDate);
        $(this).data('stickyDate', null);
    }
});


// This lets the HR member look for media files when "Browse" is clicked.
$(document).on('click', '.browse', function () {
    var file = $(this).parent().parent().parent().find('.hide-file');
    file.trigger('click');
});
$(document).on('change', '.hide-file', function () {
    $(this).parent().find('.form-control').val($(this).val().replace(/C:\\fakepath\\/i, ''));
});


// Allows HR to add multiple quiz questions

var app = angular.module('addQuizApp', ['ngRoute']);

app.controller('addQuestionController', function ($scope, $http) {

    $scope.questionData = {}

    $scope.jsonQuestion = {
        questionText: $scope.questionData.title,
        questionType: $scope.questionData.questionType,
        answers : []
    }

    for (var key in $scope.questionData.answers) {
        jsonQuestion.push($scope.questionData.answers[key]);
    }

    var config = {
        headers: {
            'Content-Type': 'application/json;'
        }
    }

    $scope.addQuestion = function () {
        $http.post('/Training/AddQuizQuestionAnswers', { quizData: JSON.stringify(sampleJSON) }, config).then(function (success) {
            alert(success);
        });
    }
});

app.controller('addQuizController', function ($scope, $http) {
    function enableAddQuestion() {
        $scope.inactive = false;
    } 

    $scope.name = "quiz"

    var groups = [];
    $http.get('/Training/GetAllGroups').then(function (data) {
        $.each(data.data, function (index, value) {
            groups.push(value);
        });
    });
    $scope.groups = groups;

    var config = {
        headers: {
            'Content-Type': 'application/json;'
        }
    }

    //$scope.quizData = {
    //    title: '',
    //    groups: [],
    //    description: '',
    //    media: ''
    //}
    $scope.quizData = {}

    $scope.jsonData = {
        title: 'this is the test title3',
        description: 'this is the test description',
        media: 'testing the media',
        groups: [1, 2]
    }

    $scope.addQuiz = function () {
        console.log($scope.quizData);
        enableAddQuestion();
        $("#confirm-submit").modal('hide');
        $http.post('/Training/AddQuiz', { quizData: JSON.stringify($scope.quizData) }, config).then(function (success) {
            alert(success);
        }); 
    }
});

var sampleJSON = {
    title: 'this is the test title'
}

$(document).ready(function () {
    $("#startDatePicker, #preferredDatePicker, #expiredDatePicker").datepicker();

    $('#saveQuizInfo, #addQuestionBtn').attr('disabled', 'disabled');

    var userData = {}

    $('#questionTable').DataTable({
        data: userData,
        columns:
        [
            { title: "id", visible: false },
            { title: "Question Type" },
            { title: "Question Text" },
            { title: "" },
            { title: "" }
        ]
    });

    
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
            } else {
                var answer = {
                    isCorrect: 'false'
                }
                question.answers.push(answer);
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
        });
    });
});
$('#submitBtnMod').click(function () {
    /* when the submit button in the modal is clicked, submit the form */
    $('#formField').submit();
});