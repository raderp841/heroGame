/// <reference path="../Scripts/jquery-1.10.2.js" />
/// <reference path="../Scripts/jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    var heroForm = $('#heroForm');
    var newHeroButton = $('#newHero');
    var heroList = $('#heroList');
    var heroes = $('.heroClass');
    var inventories = $('.inventoryClass');
    var viewInventory = $('.viewInventoryButton');
    var viewHeroButton = $('.viewHeroButton');


    heroForm.hide();
    inventories.hide();

    newHeroButton.click(function () {
        heroForm.show();
        newHeroButton.hide();
        heroList.hide();
    });

    viewInventory.click(function () {
        var button = $(this);
        var parentElement = button.parent();
        var inventoryElement = parentElement.next();

        parentElement.hide();
        inventoryElement.show();
    });

    viewHeroButton.click(function () {
        var button = $(this);
        var parentElement = button.parent();
        var heroElement = parentElement.prev();

        parentElement.hide();
        heroElement.show();
    });

    

    








});