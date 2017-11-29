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
        $http.post('/Training/AddQuizQuestionAnswers', { title: document.getElementById("trainingTitle").value,  questionData: JSON.stringify(sentJson) }, config).then(function (res) {
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

    $scope.quizData = {}

    $scope.addQuiz = function () {
        console.log($scope.quizData);
        enableAddQuestion();
        $("#confirm-submit").modal('hide');
        $('#trainingTitle').attr('disabled', 'disabled');
        $http.post('/Training/AddQuiz', { quizData: JSON.stringify($scope.quizData) }, config).then(function (success) {
            //alert(success);
        });
    };


    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = mm + '/' + dd + '/' + yyyy;
    //console.log(today)

    //compare dates

    var startDate = $("#startDate").val();
    var preferredDate = $("#preferredDate").val();
    var expirationDate = $("#expirationDate").val();

    var s = new Date(startDate);
    var t = new Date(today);
    var p = new Date(preferredDate);
    var e = new Date(expirationDate);

    if (s < t) {
        document.getElementById('startWarning').innerHTML = "Start date must be on or after today's date"
    }

    if (p < s) {
        document.getElementById('preferredWarning').innerHTML = "Preferred date must be after start date"
    }
    if (e < p) {
        document.getElementById('expirationWarning').innerHTML = "Expiration date must be after preferred date"
    }

    //$scope.quizData.startDate = startDate;
    //$scope.quizData.preferredDate = preferredDate;
    //$scope.quizData.expirationDate = expirationDate;

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
        id = table.row($(this).parent()).data()[0];
        row = $(this).parent();
        $("#confirmQuestionRemove").modal('show');
    });

    $('#removeQuestionBtn').on('click', function () {
        $("#confirmRemove").modal('hide');
        $('#trainingTable').DataTable()
            .row(row)
            .remove()
            .draw();

        // Remove record
        $.ajax({
            method: 'post',
            url: '/Training/RemoveQuestion',
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify({ id: JSON.stringify(id) }),
            success: function (data, status) {
                alert("success");
            }
        }).then(function (response) {

        });
    });

    
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