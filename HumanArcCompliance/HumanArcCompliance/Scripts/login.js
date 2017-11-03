$(document).ready(function () {
    //$('#LogOnButton').click(function () {
    //    event.preventDefault();
    //    login();
    //});
    $('#myLink').click(function () {
        var username = $('#username').val(); // get the textbox value
        var password = $('#password').val();
        //location.href = url; // redirect
        return true; // cancel default redirect
    });
});

function login() {
    var myUsername = document.getElementById("username").value;
    var myPassword = document.getElementById("password").value;
    $.ajax({
        url: '/Home/Login',
        type: "Post",
        data: { username: myUsername, password: myPassword },
        contextType: "application/json",
        success: function () {
        },
        error: function (e, status, then) {
           
        },
        complete: function () {
          
        }
    });
}