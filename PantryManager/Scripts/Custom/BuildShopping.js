$(document).ready(function () {
    $.ajax({
        url: '/Items/BuildShoppingTable',
        success: function (result) {
            $('#tableDiv').html(result);
        }
    })
})