var table;
function initDataTbl(tblId, serverUrl, columns, columnDefs, ajaxDataFuncName = null) {
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
            data: function (d) {
                if (ajaxDataFuncName != null) {
                    ajaxDataFuncName(d)
                }
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

function ToggleStatus(id, url) {
    AjaxCaller(type = "get",
        url = url,
        data = { id: id },
        callbackFunction = function (res) {
            Toast(res.Succeeded, res.Message)
        }
    )
}