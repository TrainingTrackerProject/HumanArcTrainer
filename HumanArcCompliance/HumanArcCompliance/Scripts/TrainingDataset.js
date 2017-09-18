//Dataset for To Do list
var toDoDataSet = [
    ["Learn Assembly", "Assembly.ppt", "Take Quiz", "24%"],
    ["How to cook lasagna", "Lasagna.ppt", "Take Quiz", "53%"]
];

$(document).ready(function () {
    $('#toDoTable').DataTable({
        data: toDoDataSet,
        columns: [
            { title: "Training Title" },
            { title: "Training File" },
            { title: "Quiz" },
            { title: "Completion" },
        ]
    });

    $('#toDoTable tbody').on('click', 'tr', function () {

        var data = $('#toDoTable').DataTable().row(this).data()

        alert('Pretend you\'re downloading ' + data[0] + '\'s data.');

    });
});

//Dataset for Completed list
var completedDataSet = [
    ["How to wash your hands", "Hands.ppt"],
    ["How to cook lasagna", "Lasagna.ppt"],
    ["How to whatever", "Whatever.ppt"]
];

$(document).ready(function () {
    $('#completedTable').DataTable({
        data: completedDataSet,
        columns: [
            { title: "Training Title" },
            { title: "Training File" },
        ]
    });

    $('#completedTable tbody').on('click', 'tr', function () {

        var data = $('#completedTable').DataTable().row(this).data()

        alert('Pretend you\'re downloading ' + data[1]);

    });
});

