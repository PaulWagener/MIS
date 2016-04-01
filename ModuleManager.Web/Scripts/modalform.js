// modalform.js

$(function () {
    $.ajaxSetup({ cache: false });

    $("#content").on("click", "a[data-modal]", function (e) {

        e.preventDefault();
        // hide dropdown if any (this is used wehen invoking modal from link in bootstrap dropdown )
        //$(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');

        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');
            bindForm(this);
        });

    });
});

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        var data = $(this).serialize();
        $.ajax({
            url: this.action,
            type: this.method,
            data: data,
            success: function (result) {
                console.log(result);
                if (result.success) {
                    $('#myModal').modal('hide');
                    location.reload();
                    //$('#replacetarget').reload(result.url); //  Load data from the server and place the returned HTML into the matched element
                } else {
                    if (result.strError != null) {
                        alert(result.strError);
                    } else {
                        alert("Er is een fout opgetreden.");
                    }
                }
            }
        });
        return false;
    });
}