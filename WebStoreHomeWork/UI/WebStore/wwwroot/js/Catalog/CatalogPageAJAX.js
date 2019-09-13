PartialCatalog = {
    _properties: {
        path: ""
    },

    init: function (properties) {
        $.extend(PartialCatalog._properties, properties);

        // первая загрузка
        $.get(PartialCatalog._properties.path)
            .done(function (result) {
                $("#page-catalog").LoadingOverlay("show");
                PartialCatalog.refreshPartialCatalog(result);
                PartialCatalog.initEvent(); // редактируем кнопки после их появления
            })
            .fail(function () { alert("Not Ok"); });

        //PartialCatalog.initEvent();
    },

    initEvent: function () {
        //$(".page-num").attr("onClick", "PartialCatalog.getPartialCatalog();");
        $(".page-num").click(PartialCatalog.getPartialCatalog);
    },


    /**
     * 
     * @param {Event} event
     */
    getPartialCatalog: function (event) {
        $("#page-catalog").LoadingOverlay("show");
        event.preventDefault();
        //var button = $(this);
        var page = $(this).data("page");

        $.get(PartialCatalog._properties.path + "?" + "page=" + page)
            .done(function (result) { PartialCatalog.refreshPartialCatalog(result); })
            .fail(function () { alert("Not Ok"); });
    },

    refreshPartialCatalog: function (result) {
        $("#page-catalog").html(result);
        PartialCatalog.initEvent();
        $("#page-catalog").LoadingOverlay("hide");
    }
}