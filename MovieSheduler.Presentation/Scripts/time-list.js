$(function () {
    var timeItemTemplate = "<div data-time-list-item>\
                        <div class=\"input-group time-item\">\
                            <input type=\"text\" class=\"form-control\" name=\"TimeList\">\
                            <div class=\"input-group-btn\">\
                                <button type=\"button\" class=\"btn btn-info\" data-time-list-remove aria-label=\"Help\">\
                                    <span class=\"glyphicon glyphicon-trash\" aria-hidden=\"true\"></span>\
                                </button>\
                            </div>\
                        </div>\
                    </div>";

    var errorTimeTemplate = "<span class=\"text-danger field-validation-error\"><span>Нужно заполнить время</span></span>";

    $(".timeListContainer").on("click", "[data-time-list-remove]", function () {
        $(this).parents(".input-group").remove();
    });

    $("[data-time-list-add]").click(function() {
        $("[data-time-list-container]").append(timeItemTemplate);
    });
})