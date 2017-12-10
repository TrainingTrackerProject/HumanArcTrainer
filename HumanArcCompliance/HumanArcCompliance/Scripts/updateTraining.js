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
//end of datepicker

var app = angular.module('updateQuizApp', ['ngRoute']);

app.controller('addQuestionController', function ($scope, $http) {

    $scope.questionData = {}
    $scope.status = {
        isEditing: false
    }
    $scope.mcAnswers = [
        answer1 = {
            answerText: '',
            isCorrect: false
        },
        answer2 = {
            answerText: '',
            isCorrect: false
        },
        answer3 = {
            answerText: '',
            isCorrect: false
        },
        answer4 = {
            answerText: '',
            isCorrect: false
        }
    ];

    $scope.tfAnswers = [
        answer1 = {
            answerText: 'True',
            isCorrect: false
        },
        answer2 = {
            answerText: 'False',
            isCorrect: false
        }
    ]

    var sentJson = {
        questionId: 0,
        questionText: '',
        questionType: '',
        answers: [],
        answerIds: []
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
            var row = [JSON.stringify(res.data), type, $scope.questionData.questionText, "<button class='btn btn-default edit' ng-click='edit()'>Edit</button>", "<button class='btn btn-default remove'>remove</button>"];
            var table = $('#questionTable').DataTable();
            table.row.add(row).draw();
        });
    }

    $scope.edit = function () {
        var table = $('#questionTable').DataTable();
        //update datatable here
        sentJson.answerIds = JSON.parse(table.row($(this).parent()).data()[0]);
        $scope.setCorrectAnswer();
        $http.post('/Training/AddQuizQuestionAnswer', { questionData: JSON.stringify(sentJson) }, config).then(function (res) {

        });
    }
});

app.controller('updateQuizController', function ($scope, $http, $timeout) {
    $scope.savedForm;
    $scope.inactive = true;
    $scope.quizData = {}
    $.ajax({
        url: '/Training/GetUserQuizes',
        type: 'GET',
        contentType: "application/json",
        success: function (data, status) {
            $.each(data, function (index, value) {

                for (value.id) {
                    completed.push([value.title, value.description, value.groups, value.media]);
                }
            });
            $('#notCompleted tbody tr').on('click', function () {
                var data = $('#notCompleted').DataTable().row(this).data();
                console.log(data[1]);

                window.location.href = "/Training/Quiz/?id=" + data[1];
            });
        }
    });

    $http.get('/Training/GetAllQuizes').then(function (data) {
        $.each(data.data, function (index, value) {
            groups.push(value);
        });
    });
    $scope.groups = groups;


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
            if (saved != current) {
                $scope.$apply(function () {
                    $scope.inactive = true;
                });
            }
            else if (saved == current) {
                $scope.$apply(function () {
                    $scope.inactive = false;
                });
            }
        }, 0

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

    $scope.updateQuiz = function () {
        $scope.savedForm = angular.copy($scope.quizData);
        console.log($scope.savedForm)
        enableAddQuestion();
        $("#confirm-submit").modal('hide');
        $('#trainingTitle').attr('disabled', 'disabled');
            $http.post('/Training/UpdateQuiz', { quizData: JSON.stringify($scope.quizData) }, config).then(function (res) {

            });
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
        preferredDateCheck(p, s);
        expirationDateCheck(e, p);
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
});

$(document).ready(function () {

    $('#saveQuizInfo, #addQuestionBtn, #backToQuizPage').attr('disabled', 'disabled');

    var userData = {}

    $('#questionTable').DataTable({
        data: userData,
        columns:
        [
            { title: "id", visible: true },
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
});
$('#submitBtnMod').click(function () {
    /* when the submit button in the modal is clicked, submit the form */
    $('#formField').submit();
});