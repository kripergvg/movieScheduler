$(function() {
    $("[data-shedule-record-delete]").click(function() {
        var cancelBtn = {
            Value: "Отмета",
            Css: "btn-success",
            Callback: function () { BstrapModal.Close(); }
        };
        var deleteBtn = {
            Value: "Удалить",
            Css: "btn-danger",
            Callback: function() {
                $("[data-shedule-record-delete-form]").submit();
            }
        };
        new BstrapModal("Удаление расписания", "Вы точно хотите удалить раписание?", [cancelBtn, deleteBtn]).Show();
    });
});