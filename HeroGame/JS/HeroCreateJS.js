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
    var hamburgerMenu = $('.hamburger-menu');
    var deleteHero = $('#deleteHero');
    var updateHero = $('#updateHero');


    heroForm.hide();
    inventories.hide();


    newHeroButton.click(function () {
        heroForm.show();
        newHeroButton.hide();
        heroList.hide();
    });

    viewInventory.click(function () {
        var button = $(this);
        var parentElement = button.parent().parent().parent();
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

    hamburgerMenu.click(function () {
        var thisMenu = $(this);
        var children = thisMenu.children();

        if (thisMenu.hasClass('holder')) {
            children.eq(0).removeClass('changeBar1')
            children.eq(1).removeClass('changeBar2');
            children.eq(2).removeClass('changeBar3');
            thisMenu.removeClass('holder');
        }
        else {
            children.eq(0).addClass('changeBar1');
            children.eq(1).addClass('changeBar2');
            children.eq(2).addClass('changeBar3');
            thisMenu.addClass('holder');
        }
    });














});