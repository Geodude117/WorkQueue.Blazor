    var onBegin = function onBegin() {
        $("#GroupClick").prop('disabled', true);
        $("#Queueitems").prop('align', "center");
        $("#Queueitems").html($("#loader").html());
    };


    var ActionBegin = function ActionBegin() {
        $("#mainoverlay").addClass("overlay");
        $("#mainoverlay").addClass("active");
        $("#mainoverlay").html("<img id=\"secondImage\" src =\"images/LoadingGif3.gif\" alt =\"Loading\" />");
    };

    var EndRequest = function EndRequest() {
        $("#mainoverlay").removeClass("active");
        $("#mainoverlay").html("");
    };

    var NavBarEndRequest = function NavBarEndRequest() {
        $("#mainoverlay").html("");
        $('#mainoverlay').click(CloseOpen);
    };

    var onSuccess = function onSuccess(data) {
        $("#GroupClick").prop('disabled', false);
        $("#Queueitems").prop('align', "left");
        datatable('#queueItemTable');
    };

var datatable = function datatable(data) {
        $(document).ready(function() {
                $(data).DataTable({
                    "dom": "<'row'<'col-sm-12 pull-right'f>>" +
                        "<'row'<'col-sm-12'tr>>" +
                        "<'row'<'col-sm-5'B><'col-sm-7'p>>",
                    "buttons": [
                        {
                            extend: 'copyHtml5',
                            exportOptions: {
                                "columns": [2, 3, 4, 5],
                            }
                        }, {
                            extend: 'excelHtml5',
                            exportOptions: {
                                "columns": [2, 3, 4, 5],
                            }
                        }
                    ],
                    columns: [
                        null,
                        null,
                        null,
                        {
                            "render": $.fn.dataTable.render.ellipsis(35, "customer-text")
                        },
                        null,
                        {
                            "render": $.fn.dataTable.render.ellipsis(120, "customer-summary")
                        },
                        null
                ],
                "order": [[4, "asc"]],
                "columnDefs": [
                    { "orderable": false, "targets": 0 },
                    { "width": "30px", "orderable": false, "targets": 1 },
                    { "width": "64px", "orderable": false, "targets": 2},
                    { "width": "150px","orderable": false, "targets": 3},
                    { "targets": 4 },
                    { "max-width": "350px", "orderable": false, "targets": 5 },
                    { "orderable": false, "targets": 6 }
                ],
                "lengthChange": false,
                "lengthMenu": [
                    [7, 14, 21, -1],
                    ['7 rows', '14 rows', '21 rows', 'Show all']
                ]
            });
        });
    };

    var CloseOpen = function CloseOpen() {
        $('#mainoverlay').removeClass("active");
        $("#CsuItems").html("");
    };

    var ShowActionResults = function ShowActionResults(data) {
        var x = document.getElementById("QueueActionResults");
        if (x.style.display === "none") {
            x.style.display = "block";
            $('#ProcessCallback').text('Cancel')
            $('#EditButton').hide();
        } else {
            x.style.display = "none";
            $('#ProcessCallback').text('Process Callback')
            $('#EditButton').show();
        }
    };

     function SaveButton() {
        location.href = '@Url.Action("Index", "WorkQueue",new { SelectedGroupId = 1 } )';
    };

    $.fn.dataTable.render.ellipsis = function (cutoff, classes ,wordbreak, escapeHtml) {
        var esc = function (t) {
            return t
                .replace(/&/g, '&amp;')
                .replace(/</g, '&lt;')
                .replace(/>/g, '&gt;')
                .replace(/"/g, '&quot;');
        };

        return function (d, type, row) {
            // Order, search and type get the original data

            if (type !== 'display') {
                return d;
            }

            if (typeof d !== 'number' && typeof d !== 'string') {
                return d;
            }

            d = d.toString(); // cast numbers

            if (d.length < cutoff) {
                return d;
            }

            var shortened = d.substr(0, cutoff - 1);

            // Find the last white space character in the string
            if (wordbreak) {
                shortened = shortened.replace(/\s([^\s]*)$/, '');
            }

            // Protect against uncontrolled HTML input
            if (escapeHtml) {
                shortened = esc(shortened);
            }

            return '<div class="ellipsis '+classes+'" title="' + esc(d) + '">' + shortened + '&#8230;</div>';
        };
    };