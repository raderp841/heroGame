/// <reference path="../Scripts/jquery-1.10.2.js" />
/// <reference path="../Scripts/jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    var heroForm = $('#heroForm');
    var newHeroButton = $('#newHero');
    var heroList = $('#heroList');

    heroForm.hide();

    newHeroButton.click(function () {
        heroForm.show();
        newHeroButton.hide();
        heroList.hide();
    });




});