﻿
var edit = "<a ui-sref='.edit({trainingId : training.ID})'>Edit</a>";
var del = "<a href='#' ng-click='trainingCtrl.delete(training.ID)'>Delete</a";

var dataSet = [
    [edit + " | " + del, "Test", "djsalgj;s"],
    [edit + " | " + del, "Security", "This is test"],
    [edit + " | " + del, "Security For All", "This test is for everyone"],
    [edit + " | " + del, "Test", "Yah another test"],
    [edit + " | " + del, "Best Training Ever", "Some VERY GOOD training, highly recommend"],
    [edit + " | " + del, "Accountant", "A boring trainig test"]
];

$(document).ready(function () {
    $('#trainingTable').DataTable({
        data: dataSet,
        columns: [
            { title: "" },
            { title: "Name" },
            { title: "Description" }
        ]
    });

  /*  $('#trainingTable tbody').on('click', 'tr', function () {

        var data = $('#trainingTable').DataTable().row(this).data()

        alert('You clicked on ' + data[1] + '\'s row');

    }); */
});