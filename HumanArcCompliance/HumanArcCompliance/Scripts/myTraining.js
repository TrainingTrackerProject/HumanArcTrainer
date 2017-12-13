$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    showLoadingScreen("Loading Quizzes Please Wait");
    var timeout = setTimeout(function () {
        hideLoadingScreen();
    }, 10000);
        $.ajax({
            url: '/Training/GetUserQuizes',
            type: 'GET',
            contentType: "application/json",
            success: function (data, status) {
                var needsGraded = [];
                var notCompleted = [];
                var completed = [];

                $.each(data, function (index, value) {
                    var preferShow = value.preferredDate;
                    var preferResult = preferShow.slice(6, 19);
                    var preferNum = Number(preferResult);
                    var newPreferDate = new Date(parseInt(preferResult, 10));
                    var preferDateDisplay = newPreferDate.toDateString();

                    var expireShow = value.expirationDate;
                    var expireResult = expireShow.slice(6, 19);
                    var expireNum = Number(expireResult);
                    var newExpireDate = new Date(parseInt(expireResult, 10));
                    var expireDateDisplay = newExpireDate.toDateString();

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

                    var pDate = "<span>" + preferDateDisplay + "</span>";

                    if (t < preferNum) {
                        pDate = "<span class='greenFont'>" + preferDateDisplay + "</span>";
                    }
                    else if (t >= preferNum && t <= expireNum) {
                        pDate = "<span class='orangeFont'>" + preferDateDisplay + "</span>";
                    }

                    var eDate = "<span>" + expireDateDisplay + "</span>";

                    if (t >= expireNum) {
                        pDate = "<span class='redFont'>" + preferDateDisplay + " QUIZ CLOSED</span>"
                        eDate = "<span class='redFont'>" + expireDateDisplay + " QUIZ CLOSED</span>";
                    }

                    $('#notCompleted').on("mouseover", "tbody tr", function () {
                        if (t >= expireNum) {
                            var row = $('#notCompleted').DataTable().row(this).node();
                            $(row).addClass("cursorChange");
                        }
                    });
                    if (value.isGraded) {
                        completed.push([value.userId, value.quizId, value.quizTitle, (value.percentCorrect*100) + '%']);
                    } else if (value.isCompleted) {
                        needsGraded.push([value.userId, value.quizId, value.quizTitle, (value.percentCorrect * 100) + '%']);
                    } else {
                        notCompleted.push([value.userId, value.quizId, value.quizTitle, pDate, eDate]);
                    }
                });
                $('#needsGraded').DataTable({
                    data: needsGraded,
                    columns:
                    [
                        { title: "userId", visible: false },
                        { title: "quizId", visible: false },
                        { title: "Title" },
                        { title: "Percent Correct"}
                    ]
                });
                $('#notCompleted').DataTable({
                    data: notCompleted,
                    columns:
                    [
                        { title: "userId", visible: false },
                        { title: "quizId", visible: false },
                        { title: "Title" },
                        { title: "Preferred Date" },
                        { title: "Expiration Date" }
                    ]
                });
                $('#completed').DataTable({
                    data: completed,
                    columns:
                    [
                        { title: "userId", visible: false },
                        { title: "quizId", visible: false },
                        { title: "Title" },
                        { title: "Percent Correct" }
                    ]
                });
                $('#needsGraded tbody tr').on('click', function () {
                    var data = $('#needsGraded').DataTable().row(this).data();
                    window.location.href = "/Training/Quiz/?id=" + data[1];
                });
                $('#completed tbody tr').on('click', function () {
                    var data = $('#needsGraded').DataTable().row(this).data();
                    window.location.href = "/Training/Quiz/?id=" + data[1];
                });
                $('#notCompleted').on('click', 'tbody tr', function () {
                    var data = $('#notCompleted').DataTable().row(this).data();
                    window.location.href = "/Training/Quiz/?id=" + data[1];
                });
                $('#needsGraded').on('click', 'tbody tr', function () {
                    var data = $('#needsGraded').DataTable().row(this).data();
                });
                var notCompletedTable = $('#notCompleted').dataTable();
            }
        }).then(function () {
            hideLoadingScreen();
            clearTimeout(timeout);
    }); 
}

function showLoadingScreen(message) {
    document.getElementById('spinnerText').innerHTML = message;
    $('#spinner').fadeIn("fast");
    document.getElementById('main-container').style.display = "none";
    document.getElementById('spinner').style.display = "block";
}

function hideLoadingScreen() {
    $("#spinner").fadeOut("fast");
    document.getElementById('spinner').style.display = "none";
    document.getElementById('main-container').style.display = "block";
    $('#spinner').stop();
}