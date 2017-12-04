﻿var quizId = 0;

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


var app = angular.module('addQuizApp', ['ngRoute']);

app.controller('addQuestionController', function ($scope, $http) {

    $scope.questionData = {}

    $scope.mcAnswers = [
        answer1 = {
            text: '',
            isCorrect: false
        },
        answer2 = {
            text: '',
            isCorrect: false
        },
        answer3 = {
            text: '',
            isCorrect: false
        },
        answer4 = {
            text: '',
            isCorrect: false
        }
    ];

    $scope.tfAnswers = [
        answer1 = {
            text: 'True',
            isCorrect: false
        },
        answer2 = {
            text: 'False',
            isCorrect: false
        }
    ]

    var sentJson = {
        questionText: '',
        questionType: '',
        answers: []
    };

    $scope.isCorrect = '';

    $scope.setCorrectAnswer = function () {
        sentJson.questionText = $scope.questionData.questionText;
        sentJson.questionType = $scope.questionData.questionType;
        if ($scope.questionData.questionType == 'multipleChoice') {
            if ($scope.isCorrect == 'answer1') {
                $scope.mcAnswers[0].isCorrect = true;
            }
            else if ($scope.isCorrect == 'answer2') {
                $scope.mcAnswers[1].isCorrect = true;
            }
            else if ($scope.isCorrect == 'answer3') {
                $scope.mcAnswers[2].isCorrect = true;
            }
            else {
                $scope.mcAnswers[3].isCorrect = true;
            }
            sentJson.answers = $scope.mcAnswers;
        }
        else if ($scope.questionData.questionType == 'trueFalse') {
            if ($scope.isCorrect == 'answer1') {
                $scope.tfAnswers[0].isCorrect = true;
            }
            else if ($scope.isCorrect == 'answer2') {
                $scope.tfAnswers[1].isCorrect = true;
            }
            sentJson.answers = $scope.tfAnswers;
        }
        $scope.addQuestion();
    }

    var config = {
        headers: {
            'Content-Type': 'application/json;'
        }
    }

    $scope.addQuestion = function () {
        $("#questionModal").modal('hide');
        $http.post('/Training/AddQuizQuestionAnswers', { title: document.getElementById("trainingTitle").value, questionData: JSON.stringify(sentJson) }, config).then(function (res) {
            console.log(res);
            var type;
            if ($scope.questionData.questionType == 'trueFalse') {
                type = "True/False";
            }
            else if ($scope.questionData.questionType == "multipleChoice") {
                type = "Multiple Choice";
            }
            else {
                type = "Short Answer"
            }
            var row = [JSON.stringify(res.data), type, $scope.questionData.questionText, 'edit button', "<button class='btn btn-default remove'>remove</button>"];
            var table = $('#questionTable').DataTable();
            table.row.add(row).draw();
        });
    }
});

app.controller('addQuizController', function ($scope, $http, $timeout) {
    $scope.savedForm;
    $scope.inactive = true;
    $scope.quizData = {}

    function enableAddQuestion() {
       $scope.inactive = false;
    }

    function disableAddQuestion() {
        $scope.inactive = true;
    }

    $scope.setGroups = function () {
        
            console.log($scope.quizData.groups);
    }

    $('.quizFormInfo').on('change keyup paste', function () {
        var saved = '';
        var current = '';
        $timeout(function () {
            saved = JSON.stringify($scope.savedForm);
            current = JSON.stringify($scope.quizData);
            if (quizId !== 0 && saved != current) {
                $scope.$apply(function () {
                    $scope.inactive = true;
                });
            }
            else if (quizId !== 0 && saved == current) {
                $scope.$apply(function () {
                    $scope.inactive = false;
                });
            }
        },0

        )

    });

   

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


    $scope.addQuiz = function () {
        $scope.savedForm = angular.copy($scope.quizData);
        console.log($scope.savedForm)
        enableAddQuestion();
        $("#confirm-submit").modal('hide');
        $('#trainingTitle').attr('disabled', 'disabled');
        //$('#saveQuizInfo').attr('disabled', 'disabled');
        if (quizId != 0) {
            $http.post('/Training/UpdateQuiz', { quizData: JSON.stringify($scope.quizData) }, config).then(function (res) {

            });
        }
        else {
            $http.post('/Training/AddQuiz', { quizData: JSON.stringify($scope.quizData) }, config).then(function (res) {
                quizId = res.data[0];
            });
        }
        

    };


    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = mm + '/' + dd + '/' + yyyy;
    
    var t = Date.parse(today);
    //compare dates

   

    $('.dateInfo').on('change keyup', function () {
        var startDate = $("#startDate").val();
        var preferredDate = $("#preferredDate").val();
        var expirationDate = $("#expirationDate").val();

        var s = Date.parse(startDate);
        
        var p = Date.parse(preferredDate);
        var e = Date.parse(expirationDate);
        startDateCheck(s);
        preferredDateCheck(p,s);
        expirationDateCheck(e,p);
    })


    function startDateCheck(s) {
        if (Number.isInteger(s) && s < t) {
            document.getElementById('startWarning').innerHTML = "Start date must be on or after today's date"
            $("#startDate").val("");
        }
        else if (Number.isInteger(s)) {
            document.getElementById('startWarning').innerHTML = ""
        }
    }

    function preferredDateCheck(p, s) {
        if (!Number.isInteger(s)) {
            $("#preferredDate").val("");
        }
        if (Number.isInteger(p) && p < s && Number.isInteger(s)) {
            document.getElementById('preferredWarning').innerHTML = "Preferred date must be after start date" 
            $("#preferredDate").val("");
        }
        else if (Number.isInteger(p)) {
            document.getElementById('preferredWarning').innerHTML = ""
        }
    }

    function expirationDateCheck(e, p) {
        if (!Number.isInteger(p)) {
            $("#expirationDate").val("");
        }
        if (Number.isInteger(e) && e < p && Number.isInteger(p)) {
            document.getElementById('expirationWarning').innerHTML = "Expiration date must be after preferred date"
            $("#expirationDate").val("");
        }
        else if (Number.isInteger(e)) {
            document.getElementById('expirationWarning').innerHTML = ""
        }
    }
   

    console.log(startDate)
    console.log(preferredDate)
    console.log(expirationDate)

});

var sampleJSON = {
    title: 'this is the test title'
}

$(document).ready(function () {

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
    var id;
    var row;
    $("body").on("click", ".remove", function () {
        var table = $('#questionTable').DataTable();
        id = JSON.parse(table.row($(this).parent()).data()[0]);
        row = $(this).parent();
        $("#confirmQuestionRemove").modal('show');
    });

    $('#removeQuestionBtn').on('click', function () {
        var ids = {
            ids: id
        }
        $.ajax({
            method: 'post',
            url: '/Training/RemoveQuestion',
            dataType: "json",
            contentType: 'application/json',
            data: JSON.stringify({ ids: JSON.stringify(ids) }),
            success: function (res, status) {
                console.log(res);
                if (res == true) {
                    console.log("Successfully Deleted Question")
                    removeQuestionFromTable();
                }
                else {
                    console.log("There was an error deleting the question");
                }
            }
        }).then(function (response) {
            $("#confirmQuestionRemove").modal('hide');
        });
    });

    function removeQuestionFromTable() {
        $('#questionTable').DataTable()
            .row(row)
            .remove()
            .draw();
    }




    //$('#submit').click(function () {
    //    var json;
    //    var groups = [];
    //    var quiz = {
    //        title: '',
    //        description: '',
    //        media: '',
    //        startDate: '',
    //        preferDate: '',
    //        expiredDate: ''
    //    }
    //    var questions = "";
    //    var answers = [];
    //    var q = [];
    //    event.preventDefault();
    //    quiz.title = $('#trainingTitle').val();
    //    quiz.description = $('#trainingDesc').val();
    //    quiz.media = $('#mediaFile').val();
    //    quiz.startDate = $('#startDate').val();
    //    quiz.preferDate = $('#preferredDate').val();
    //    quiz.expiredDate = $('#expirationDate').val();
    //    $('#groupsApplied > option:selected').each(function (index, value) {
    //        groups.push(value.id);
    //    });
    //    var questionForm = $('#questions > fieldset').each(function (index, value) {
    //        var question = {
    //            type: '',
    //            text: document.getElementById('questionText' + index).value,
    //            answers: []
    //        }
    //        $('input:radio').each(function (i, value) {                
    //            if (value.name == "content" + index) {                  
    //                if (value.checked) {
    //                    question.type = value.value;
    //                }
    //            }
    //        });

    //        if (question.type == "multipleChoice") {
    //            for (var i = 1; i < 5; i++) {
    //                var answerid = 'choice' + i + index;
    //                var answer = {
    //                    //id: index,
    //                    answerText: document.getElementById(answerid).value,
    //                    isCorrect: 'false'
    //                }
    //                if (document.getElementById(('is' + i) + index).checked){
    //                    answer.isCorrect = 'true';
    //                }
    //                question.answers.push(answer);
    //            }
    //        } else if (question.type == "trueFalse") {
    //            var answer1 = {
    //                //id: index,
    //                answerText: 'true',
    //                isCorrect: 'false'
    //            }
    //            if (document.getElementById('trueAnswer' + index).checked) {
    //                answer1.isCorrect = 'true';
    //            }
    //            question.answers.push(answer1);
    //            var answer2 = {
    //                //id: index,
    //                answerText: 'false',
    //                isCorrect: 'false'
    //            }
    //            if (document.getElementById('falseAnswer' + index).checked) {
    //               answer2.isCorrect = 'true';
    //            }
    //            question.answers.push(answer2);
    //        } else {
    //            var answer = {
    //                isCorrect: 'false'
    //            }
    //            question.answers.push(answer);
    //        }
    //        if (questions == "") {
    //            questions += JSON.stringify(question);
    //        } else {
    //            questions += "|" + JSON.stringify(question);
    //        }
    //        q.push(question);
    //        json = {
    //            title: quiz.title,
    //            description: quiz.description,
    //            questions: q
    //        }
    //    });
    //    $.ajax({
    //        type: 'POST',
    //        url: '/Training/AddQuiz',
    //        data: {group:groups.toString(), j:JSON.stringify(json) }, //{group: groups.toString(), quiz: JSON.stringify(quiz), question: questions },
    //    });
    //});
});
$('#submitBtnMod').click(function () {
    /* when the submit button in the modal is clicked, submit the form */
    $('#formField').submit();
});