//Dataset for To Do list for My Training

$.ajax({
    url: '/Training/GetUserQuizes',
    type: 'GET',
    contentType: "application/json",
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
                { title: "quizId", visible: false },
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
        $('#needsGraded tbody tr').on('click', function () {
            var data = $('#needsGraded').DataTable().row(this).data();
            window.location.href = "/Training/EmployeeQuizes/?id=" + data[0];
        });
        $('#completed tbody tr').on('click', function () {
            var data = $('#needsGraded').DataTable().row(this).data();
            window.location.href = "/Training/EmployeeQuizes/?id=" + data[0];
        });
    }
});

