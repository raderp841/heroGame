/// <reference path="../Scripts/jquery-1.10.2.js" />
/// <reference path="../Scripts/jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    var login = $("#login");
    var register = $("#register");
    var showRegButton = $(".showRegister");
    var showLogButton = $(".showLogin");

    login.hide();

    showLogButton.click(function () {
        register.hide();
        login.show();
    });

    showRegButton.click(function () {
        login.hide();
        register.show();
    });
});


