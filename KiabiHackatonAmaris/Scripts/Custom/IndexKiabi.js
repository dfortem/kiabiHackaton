

$(document).ready(function () {
    $(document).off("click", "#AdvancedSearchAnchor").on("click", "#AdvancedSearchAnchor", function () {
        var advancedSearchMenu = $('#advancedSearchDiv');
        if (advancedSearchMenu.hasClass('visible')) {
            $('#AdvancedSearchAnchor i').attr('class', 'fa fa-caret-down');
            advancedSearchMenu.removeClass('visible');
            advancedSearchMenu.slideUp('slow');
        } else {
            $('#AdvancedSearchAnchor i').attr('class', 'fa fa-caret-up');
            advancedSearchMenu.addClass('visible');
            advancedSearchMenu.slideDown('slow');
        }
    });

    $(".open-modal").on("click", function() {
        var group = $(this).data("group");
        var theme = $(this).data("theme");
        $("#GroupText").text(group);
        $("#ThemeText").text(theme);
        $("#ModalImage").attr("src", "/Content/images/KIABI_MAP_7.jpg");
        if (group === "GRMEN") {
            switch (theme) {
            case "MURAL MODE":
            case "MURALVILLE":
                $("#ModalImage").attr("src", "/Content/images/KIABI_MAP_1.jpg");
                break;
            case "GTSPECI":
                $("#ModalImage").attr("src", "/Content/images/KIABI_MAP_4.jpg");
                break;
            case "GT_LENY":
                $("#ModalImage").attr("src", "/Content/images/KIABI_MAP_3.jpg");
                break;
            }
        }
        else if (group === "GRACCESS" && theme === "VIACCESS") {
            $("#ModalImage").attr("src", "/Content/images/KIABI_MAP_2.jpg");
        }
        else if (group === "GRKIDS" && theme === "FPPX") {
            $("#ModalImage").attr("src", "/Content/images/KIABI_MAP_5.jpg");
        }
        else if (group === "GRLINGERIE" && theme === "ESSCOTNU") {
            $("#ModalImage").attr("src", "/Content/images/KIABI_MAP_6.jpg");
        }
        else if (group === "GRBABY" && theme === "L0341") {
            $("#ModalImage").attr("src", "/Content/images/KIABI_MAP_7.jpg");
        }
    });
});
