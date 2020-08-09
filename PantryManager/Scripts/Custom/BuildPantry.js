$(document).ready(function () {
    $.ajax({
        url: '/Items/BuildPantryTable',
        success: function (result) {
            $('#tableDiv').html(result);
        }
    })
})