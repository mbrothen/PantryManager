$(document).ready(function () {
    $.ajax({
        url: '/Items/BuildItemTable',
        success: function (result) {
            $('#tableDiv').html(result);
        }
    })
})