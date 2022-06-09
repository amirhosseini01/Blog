var table;
function initDataTbl(tblId, serverUrl, columns, columnDefs) {
    if (table != undefined)
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
            url: "/Admin/Lib/datatables/Persian.json"
        },
        columnDefs: columnDefs,
        responsive: true,
        select: true,
        columns: columns,
    });
}


function ChangeOrder(id, order, pageName) {
    AjaxCaller(type = "get",
        url = '/Admin/' + pageName + '?handler=ChangeOrder',
        data = { id: id, order: order },
        callbackFunction = function (res) {
            Toast(res.Succeeded, res.Message)
        }
    )
}