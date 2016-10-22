$(function () {

    var notificationhub = $.connection.updateTableHub;

    notificationhub.client.displayMessage = function (message) {

        $('#notification').html(message);
    };

    notificationhub.client.addRow = function (url, responseTime) {
        $("#resultBody").append("<tr><td>" + url + "</td><td>" + responseTime + "</td></tr>")
    };

    var yourTableHTML = "";
    jQuery.each(name, function (i, data) {
        $("#resultBody").append("<tr><td>" + url + "</td><td>" + responseTime + "</td></tr>");
    });

    $.connection.hub.start();

    //$("#resultBody").append("<tr><td>" + url + "</td><td>" + responseTime + "</td></tr>")
});




$(function () {
    $("#startButton").click(function () {
        $("#resultTable").show();
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

