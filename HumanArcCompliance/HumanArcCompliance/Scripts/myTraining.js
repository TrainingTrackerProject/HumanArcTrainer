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
                pDate = "<span style='color: green;font-weight:bold;'>" + preferDateDisplay + "</span>";
            }
            else if (t >= preferNum) {
                pDate = "<span style='color: orange;font-weight:bold;'>" + preferDateDisplay + "</span>";
            }

            var eDate = "<span>" + expireDateDisplay + "</span>";

            if (t >= expireNum) {
                eDate = "<span style='color: red;font-weight:bold;'>" + preferDateDisplay + " QUIZ CLOSED</span>";
            }

            if (value.isGraded) {
                completed.push([value.userId, value.quizId, value.quizTitle]);
            } else if (value.isCompleted) {
                needsGraded.push([value.userId, value.quizId, value.quizTitle]);
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
                { title: "Title" }
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
                { title: "Title" }
            ]
        });
        //$('#needsGraded tbody tr').on('click', function () {
        //    var data = $('#needsGraded').DataTable().row(this).data();
        //    window.location.href = "/Training/EmployeeQuizes/?id=" + data[0];
        //});
        //$('#completed tbody tr').on('click', function () {
        //    var data = $('#needsGraded').DataTable().row(this).data();
        //    window.location.href = "/Training/EmployeeQuizes/?id=" + data[0];
        //});
        $('#notCompleted tbody tr').on('click', function () {
            var data = $('#notCompleted').DataTable().row(this).data();
            console.log(data[1]);

            window.location.href = "/Training/Quiz/?id=" + data[1];
        });
    }
});

