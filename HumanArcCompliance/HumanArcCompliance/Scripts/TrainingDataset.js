//Dataset for To Do list

var toDoDataSet = [
    ["Learn Assembly", "<a href='download.html'>Download Training File</a>", "<a href='Quiz'>Take Quiz</a>", "24%"],
    ["Learn Whitespace....why", "<a href='download.html'>Download Training File</a>", "<a href='Quiz'>Take Quiz</a>", "53%"]
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
});

//Dataset for Completed list
var completedDataSet = [
    ["How to wash your hands", "<a href='download.html'>Download Training File</a>"],
    ["How to drink water", "<a href='download.html'>Download Training File</a>"],
    ["How to breathe air", "<a href='download.html'>Download Training File</a>"]
];

$(document).ready(function () {
    $('#completedTable').DataTable({
        data: completedDataSet,
        columns: [
            { title: "Training Title" },
            { title: "Training File" },
        ]
    });
});

