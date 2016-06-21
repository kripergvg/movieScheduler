$(function () {
    var validator = $("[data-validate-hidden]").data('validator');
    validator.settings.ignore = "";

    //$.validator.setDefaults({ ignore: [] });

    $("[data-datepicker]").datepicker({ language: "ru",startDate:new Date()});
    $("[data-datepicker]").on("changeDate", function () {
        var hiddenFiledId = $(this).attr("data-hidden-field");
        $("#" + hiddenFiledId).val(
            $(this).datepicker("getFormattedDate")
        );
    });
})