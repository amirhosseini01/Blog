function initDataTbl(tblId, serverUrl, columns) {
    table.destroy();
    table = $(tblId).DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        ajax: {
            url: serverUrl,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            type: "POST",
            datatype: "json"
        },
        language: {
            url: "/Persian.json"
        },
        responsive: true,
        select: true,
        columns: columns,
    });
}