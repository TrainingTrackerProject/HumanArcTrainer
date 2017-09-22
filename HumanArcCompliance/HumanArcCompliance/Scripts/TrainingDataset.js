//Dataset for To Do list
var downloadFile = "<a href='download.html'>Download Training File</a>";
var takeQuiz = "<a href='quiz.html'>Take Quiz</a";

var toDoDataSet = [
    ["Learn Assembly", downloadFile, takeQuiz, "24%"],
    ["How to cook lasagna", downloadFile, takeQuiz, "53%"]
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
var downloadFileCompleted = "<a href='download.html'>Download Training File</a>";

var completedDataSet = [
    ["How to wash your hands", downloadFileCompleted],
    ["How to cook lasagna", downloadFileCompleted],
    ["How to whatever", downloadFileCompleted]
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

