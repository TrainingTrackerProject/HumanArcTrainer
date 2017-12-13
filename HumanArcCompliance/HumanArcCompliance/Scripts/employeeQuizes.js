$(document).ready(function () {
    var id = document.getElementById("employeeId").innerHTML;
    $.ajax({
        url: '/Training/GetUserQuizes',
        type: 'POST',
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ id: id }),
        success: function (data, status) {
            var needsGraded = [];
            var notCompleted = [];
            var completed = [];
            $.each(data, function (index, value) {
                if (value.isGraded) {
                    completed.push([value.userId, value.quizId, value.quizTitle]);
                } else if (value.isCompleted) {
                    needsGraded.push([value.userId, value.quizId, value.quizTitle]);
                } else {
                    notCompleted.push([value.userId, value.quizId, value.quizTitle]);
                }
            });
            $('#needsGraded').DataTable({
                data: needsGraded,
                columns:
                [
                    { title: "userId", visible: false },
                    { title: "quizId", visible: false},
                    { title: "Title" }
                ]
            });
            $('#notCompleted').DataTable({
                data: notCompleted,
                columns:
                [
                    { title: "userId", visible: false },
                    { title: "quizId", visible: false },
                    { title: "Title" }
                ]
            });
            $('#completed').DataTable({
                data: completed,
                columns:
                [
                    { title: "userId", visible: false },
                    { title: "quizId", visible: false },
                    { title: "Title" }
                ]
            });
            $('#needsGraded').on('click', 'tbody tr', function () {
                console.log($('#hrCheck').val());
                if($('#hrCheck').val() == 'True'){
                var data = $('#needsGraded').DataTable().row(this).data();
                window.location.href = "/Training/GradeQuiz/?id=" + data[1];
                }
            });          
        }
    });
});