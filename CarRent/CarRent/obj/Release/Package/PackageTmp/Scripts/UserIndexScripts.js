$(function () {
    $(".grades > option").each(function () {
        $(this).addClass("list-group-item");
    });
    $(".engines > option").each(function () {
        $(this).addClass("list-group-item");
    });
});