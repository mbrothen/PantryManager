$(document).ready(function () {
    //Handle clicking the checkbox for inCart
    $('.ActiveCheck').change(function () {
        var self = $(this);
        var id = $(this).parent().parent().attr('id');
        console.log(id);
        var value = self.prop('checked');
        $.ajax({
            url: '/Items/AJAXEdit',
            data: {
                id: id,
                value: value
            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        })
    })
    //Handle Clicking the plus button for the pantryQty cell
    $('.plusPantry').click(function () {
        var self = $(this);
        var id = $(this).parent().parent().attr('id');
        $.ajax({
            url: '/Items/ChangePantryPantry',
            data: {
                id: id,
                value: 1
            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        })
    })

    //Handle Clicking the minus button for the pantryQty cell
    $('.minusPantry').click(function () {
        var self = $(this);
        var id = $(this).parent().parent().attr('id');
        $.ajax({
            url: '/Items/ChangePantryPantry',
            data: {
                id: id,
                value: -1
            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        })
    })

    //Handle Clicking the plus button for the listQty cell
    $('.plusList').click(function () {
        var self = $(this);
        var id = $(this).parent().parent().attr('id');
        $.ajax({
            url: '/Items/ChangeListPantry',
            data: {
                id: id,
                value: 1
            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        })
    })

    //Handle Clicking the minus button for the listQty cell
    $('.minusList').click(function () {
        var self = $(this);
        var id = $(this).parent().parent().attr('id');
        $.ajax({
            url: '/Items/ChangeListPantry',
            data: {
                id: id,
                value: -1
            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        })
    })

    //Handle the checkout process
    $('#completeCheckout').click(function () {
        $.ajax({
            url: '/Items/Checkout',
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        })
        $('.modal-backdrop').css({ "z-index": "0", "display": "none" });
    })

    $('.edit-btn').click(function () {
        itemId = $(this).parent().parent().attr('id');  // Get the id of the item that's set to the row property.

    })
    $('#addItem').click(function () {  //display add item form on add item button click
        $('#itemCreateForm').css({ "display": "block" })
        //Make it toggle????
    })
    $('#createItemButton').click(function () {   //hide the form on submit
        $('#itemCreateForm').css({ "display": "none"})
    })
});
