$(document).ready(function () {
    var activeSystemClass = $('.list-group-item.active');

    //something is entered in search form
    $('#system-search').keyup(function () {
        var that = this;
        // affect all table rows on in systems table
        var tableBody = $('.table-list-search tbody');
        var tableRowsClass = $('.table-list-search tbody tr');
        $('.search-sf').remove();
        tableRowsClass.each(function (i, val) {

            //Lower text for case insensitive
            var rowText = $(val).text().toLowerCase();
            var inputText = $(that).val().toLowerCase();
            if (inputText != '') {
                $('.search-query-sf').remove();
                tableBody.prepend('<tr class="search-query-sf"><td colspan="6"><strong>Searching for: "'
                    + $(that).val()
                    + '"</strong></td></tr>');
            }
            else {
                $('.search-query-sf').remove();
            }

            if (rowText.indexOf(inputText) == -1) {
                //hide rows
                tableRowsClass.eq(i).hide();

            }
            else {
                $('.search-sf').remove();
                tableRowsClass.eq(i).show();
            }
        });
        //category --------------------search
        $('#categoryName option:selected').text(function () {
            var that = this;
            // affect all table rows on in systems table
            var tableBody = $('.table-list-search tbody');
            var tableRowsClass = $('.table-list-search tbody tr');
            $('.search-sf').remove();
            tableRowsClass.each(function (i, val) {

                //Lower text for case insensitive
                var rowText = $(val).text().toLowerCase();
                var inputText = $(that).val().toLowerCase();
                if (inputText != '') {
                    $('.search-query-sf').remove();
                    tableBody.prepend('<tr class="search-query-sf"><td colspan="6"><strong>Searching for: "'
                        + $(that).val()
                        + '"</strong></td></tr>');
                }
                else {
                    $('.search-query-sf').remove();
                }

                if (rowText.indexOf(inputText) == -1) {
                    //hide rows
                    tableRowsClass.eq(i).hide();

                }
                else {
                    $('.search-sf').remove();
                    tableRowsClass.eq(i).show();
                }
            });
            //all tr elements are hidden
            if (tableRowsClass.children(':visible').length == 0) {
                tableBody.append('<tr class="search-sf"><td class="text-muted" colspan="6">No entries found.</td></tr>');
            }
        });
        //delete product alert --------------------search
        /*  Commented by Tim H
        $(function(){
            $("#deleteproduct").click(function (event) {
                event.preventDefault();
                $('<div title="Confirm Box"></div>').dialog({
                    open: function (event, ui) {
                        $(this).html("Yes or No question?");
                    },
                    close: function () {
                        $(this).remove();
                    },
                    resizable: false,
                    height: 140,
                    modal: true,
                    buttons: {
                        'Yes': function () {
                            var url = '@Url.Action(""Delete", new { id = item.product_id, @class = "btn btn-link" })';
                            $(this).dialog('close');
                            $.post(url, { categoryId: SelCat });

                        },
                        'No': function () {
                            $(this).dialog('close');
                           // $.post('url/theOtherValueYouWantToPAss');
                        }
                    }
                });
            });
            //all tr elements are hidden
            if (tableRowsClass.children(':visible').length == 0) {
                tableBody.append('<tr class="search-sf"><td class="text-muted" colspan="6">No entries found.</td></tr>');
            }
        });*/
        //delete category alert --------------------search Added by Tim H , then commented by Tim H
        /*
        $(function () {
            $("#deletecategory").click(function (event) {
                event.preventDefault();
                $('<div title="Confirm Box"></div>').dialog({
                    open: function (event, ui) {
                        $(this).html("Yes or No question?");
                    },
                    close: function () {
                        $(this).remove();
                    },
                    resizable: false,
                    height: 140,
                    modal: true,
                    buttons: {
                        'Yes': function () {
                            var url = '@Url.Action(""Delete", new { id = item.category_id, @class = "btn btn-link" })';
                            $(this).dialog('close');
                            $.post(url, { categoryId: SelCat });

                        },
                        'No': function () {
                            $(this).dialog('close');
                            // $.post('url/theOtherValueYouWantToPAss');
                        }
                    }
                });
            });
            //all tr elements are hidden
            if (tableRowsClass.children(':visible').length == 0) {
                tableBody.append('<tr class="search-sf"><td class="text-muted" colspan="6">No entries found.</td></tr>');
            }
        });
        */
    })
});