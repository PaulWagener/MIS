var table;

$(function () {

    /* Disable closing toggle-columns on click */
    $("body").on("click", ".dropdown-menu", function (e) {
        e.stopPropagation();
    });

    initSelect2();
    initDatatable();
    initToggleColumns();
    initSelectAll();

    //$("#exportModules").on("click", function(e) {
    //    e.preventDefault();
    //    exportModules();
    //});


    $("#modules").on("click", "tbody tr", function () {

        var year = $(this).find(".cursusCode").data("year");
        var cursusCode = $(this).find(".cursusCode").data("code")
        
        if(year !== undefined || cursusCode !== undefined){
            window.location = "/Module/Details/" + year + "/" + cursusCode;
        }
    });


});

function initDatatable() {



    table = $("#modules").dataTable({
        dom:
            "<'row'<'col-sm-6'l><'col-sm-6 toolbar text-right'>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-6'p><'col-sm-6'i>>",
        processing: true,
        serverSide: true,
        language: {
            url: "/Scripts/dataTables.dutch.js"
        },
        ajax: {
            url: "/api/Module/GetOverview",
            type: "POST",
            data: function (d) {
                d.filter = getFilters();
                d.orderBy = getOrder();
            }
        },
        columns: [
            { "data": "Icon" },
            { "data": "Naam" },
            { "data": "CursusCode" },
            { "data": "Schooljaar" },
            { "data": "Blokken" },
            { "data": "TotalEc" },
            { "data": "FaseNamen" },
            { "data": "Verantwoordelijke" },
            { "data": "Docenten" }
        ],
        aoColumnDefs: [
            {
                sClass: "text-center",
                bSortable: false,
                mRender: function (data, type, full) {
                    return '<span class="fa fa-3x fa-' + data + '"></span>';
                },
                aTargets: [0]
            },
            {
                mRender: function (data, type, full) {
                    return data + "<br /><span class=\"text-muted\">" + full['Verantwoordelijke'] + ", " + full['Docenten'] + "</span>";
                },
                aTargets: [1]
            },
            {
                mRender: function (data, type, full) {
                    return '<span class="cursusCode" data-year="' + full['Schooljaar'] + '" data-code="' + data + '">' + data + '</span>';
                },
                aTargets: [2]
            },
            {
                mRender: function (data, type, full) {

                    var original = data.toString();
                    var years = "20" + original.substring(0, 2) + " - 20" + original.substring(2);

                    return years;
                },
                aTargets: [3]
            },
            {
                bSortable: false,
                mRender: function (data, type, full) {
                    return data + " EC";
                },
                aTargets: [5]
            },
            {
                bSortable: false,
                aTargets: [4,6,8]
            },
        ],
        order: [[1, "asc"]],
        initComplete: function () {

            $(".toolbar").append($("#toggle-columns"));
            $("#toggle-columns").show();

            initFilters();

        }
    });

}

function initFilters() {

    $("#FilterTable").on("click", function (e) {
        e.preventDefault();
        $("#modules").dataTable().fnDraw();
    });

    $("#search").on("click", ".input-group-btn", function(e) {
        e.preventDefault();
        $("#modules").dataTable().fnDraw();
    });

    $("#ResetFilters").on("click", function(e) {
        e.preventDefault();
        resetSelect2();
    });

}

function initToggleColumns() {
    /* Toggle columns */
    $(".toggle").on("click", function (e) {

        // Get the column API object
        var column = $("#modules").dataTable().api().column($(this).data("column"));

        // Toggle the visibility
        column.visible(!column.visible());
    });
}

function initSelectAll() {
    /* Select all */
    $("#modules").on("click", "#checkbox-select-all", function () {

        if ($(this).is(":checked")) {
            $('.checkbox-module').prop("checked", true);
        } else {
            $('.checkbox-module').prop("checked", false);
        }

    });
}

function initSelect2() {
    $("#FilterCompetenties").select2({
        placeholder: "Competenties"
    });
    $("#FilterLeerlijnen").select2({
        placeholder: "Leerlijnen"
    });
    $("#FilterFases").select2({
        placeholder: "Fases"
    });
    $("#FilterLeerjaar").select2({
        placeholder: "Leerjaar"
    });
    $("#FilterEc").select2({
        placeholder: "EC"
    });
    $("#FilterBlokken").select2({
        placeholder: "Blokken"
    });
    $("#FilterTags").select2({
        placeholder: "Tags"
    });
}

function resetSelect2() {

    $('#FilterCompetenties').select2('data', null);
    $('#FilterLeerlijnen').select2('data', null);
    $('#FilterFases').select2('data', null);
    $('#FilterLeerjaar').select2('data', null);
    $('#FilterEc').select2('data', null);
    $('#FilterBlokken').select2('data', null);
    $('#FilterTags').select2('data', null);

    $("#modules").dataTable().fnDraw();
}

function getFilters() {
    
    data = {
        Zoekterm: $("#Zoekterm").val(),
        Competenties: $("#FilterCompetenties").val(),
        Fases: $("#FilterFases").val(),
        Leerjaar: $("#FilterLeerjaar").val(),
        Blokken: $("#FilterBlokken").val(),
        Tags: $("#FilterTags").val(),
        Ec: $("#FilterEc").val(),
        Leerlijnen: $("#FilterLeerlijnen").val()
    }

    return data;

}

function getExports() {

    data = {
        CursusCode: $("input[name='CursusCode']").is(":checked"),
        Naam: $("input[name='Naam']").is(":checked"),
        Beschrijving: $("input[name='Beschrijving']").is(":checked"),
        AlgemeneInformatie: $("input[name='AlgemeneInformatie']").is(":checked"),
        Studiebelasting: $("input[name='Studiebelasting']").is(":checked"),
        Organisatie: $("input[name='Organisatie']").is(":checked"),
        Weekplanning: $("input[name='Weekplanning']").is(":checked"),
        Beoordeling: $("input[name='Beoordeling']").is(":checked"),
        Leermiddelen: $("input[name='Leermiddelen']").is(":checked"),
        Leerdoelen: $("input[name='Leerdoelen']").is(":checked"),
        Competenties: $("input[name='Competenties']").is(":checked"),
        Leerlijnen: $("input[name='Leerlijnen']").is(":checked"),
        Tags: $("input[name='Tags']").is(":checked")
    }

    return data;

}

function getOrder() {
    var column = $("#modules").dataTable().fnSettings().aaSorting[0][0];
    var dir = $("#modules").dataTable().fnSettings().aaSorting[0][1];

    data = {
        column: column,
        dir: dir
    }

    return data;

}

function exportModules() {

    /* Ajax request */
    $.ajax({
        type: 'POST',
        url: '/Module/ExportAll',
        data: {
            Filters: getFilters(),
            Export: getExports()
        },
        beforeSend: function () {
            $("#exportAlert").html("<div class=\"alert alert-info\" role=\"alert\"><strong>Een ogenblik geduld a.u.b.</strong> Uw PDF wordt samengesteld.</div>");
            $("#exportModules").attr("disabled", "disabled");
        },
        success: function (data) {
            $("#exportAlert").html("");
            $("#exportModules").removeAttr("disabled", "disabled");
            // TODO, pdf meegeven aan gebruiker, of gebruiker doorlinken naar .pdf (new tab?)
            top.location.href = 'Export/All/' + data;
        },
        error: function () {
            $("#exportAlert").html("<div class=\"alert alert-danger\" role=\"alert\"><strong>Oh snap!</strong> Er ging iets mis, probeer het opnieuw.</div>");
            $("#exportModules").removeAttr("disabled", "disabled");
        }
    });

}

function exportOnSubmit() {
  
    $('#filterForm :input').not(':submit').clone().hide().appendTo('#exportForm');

    return true;
};