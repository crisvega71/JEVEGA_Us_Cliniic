$(document).ready(function () {

    $("#buttOBGyne").click(function () {
        $("#serviceInfo").load("/HtmlContent/OBGyne.html");
    });

    $("#buttPelvis").click(function () {
        $("#serviceInfo").load("/HtmlContent/Pelvis.html");
    });

    $("#buttAbdomen").click(function () {
        $("#serviceInfo").load("/HtmlContent/Abdomen.html");
    });

    $("#buttVascular").click(function () {
        $("#serviceInfo").load("/HtmlContent/Vascular.html");
    });

    $("#buttSmallparts").click(function () {
        $("#serviceInfo").load("/HtmlContent/Smallparts.html");
    });

});