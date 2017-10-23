$(document).ready(function () {
    $("#back-button").on('click', function () {
        window.location.href = window.document.referrer;
    })
});