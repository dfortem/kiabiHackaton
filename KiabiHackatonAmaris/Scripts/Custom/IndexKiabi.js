

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
});
