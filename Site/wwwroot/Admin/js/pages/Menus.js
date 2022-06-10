$(document).ready(function () {
    CreateTbl()
});

function CreateTbl() {
    var columns = [
        { data: 'Id' },
        {
            data: 'OrderView',
            "width": "5%",
            render: function (data, type, row) {
                return `<input type="number" class="form-control" value="${data}" onchange="ChangeOrder(${row.Id},this.value, 'Menus')">`
            }
        },
        { data: 'Title' },
        { data: 'Url' },
        {
            data: 'IsHidden',
            render: function (data, type, row, meta) {
                return `
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" ${data ? 'checked' : ''} onchange="ToggleStatus(${row.Id},'/Admin/Menus?handler=ToggleStatus')">
                            </label>
                        </div>
                    `;
            }
        },
        { data: 'CreateDate', visible: false },
        { data: 'PersianCreateDate' },
        { data: 'UpdateDate', visible: false },
        { data: 'PersianUpdateDate' },
        {
            data: 'Id',
            render: function (data, type, row, meta) {
                var html = `<a href="/Admin/Menus?pid=${data}" class="btn btn-primary btn-sm margin">
                            <i class="fa fa-list"></i>
                        </a>`;
                html += `<button type="button" onclick="FillModal('${data}')" class="btn btn-warning btn-sm">
                            <i class="fa fa-edit"></i>
                        </button>`;
                html += `<button type="button" onclick="DeleteItem('${data}')" class="btn btn-danger btn-sm margin">
                                <i class="fa fa-trash"></i>
                            </button>`;
                return html;
            }
        }
    ]
    initDataTbl(tblId = "#tbl", serverUrl = "/Admin/Menus?handler=List", columns = columns, columnDefs = [
        {
            targets: [6],
            orderData: [5]
        },
        {
            targets: [8],
            orderData: [7]
        },
    ], ajaxDataFuncName = ajaxAddData)
}


function DeleteItem(id) {
    DeleteAlert(callbackFunction = function () {
        AjaxCaller(type = "get", url = '/Admin/Menus?handler=Delete', data = { itemId: id }, callbackFunction = function (res) {
            Toast(res.Succeeded, res.Message)
            if (res.Succeeded) {
                CreateTbl()
            }
        })

    })
}


function ajaxAddData(d) {
    d.pid = pid
}

function OpenModal_Custom(modalId = '#mainModal', frmId = '#frm') {
    OpenModal(modalId = modalId, frmId = frmId)
    $('#VmInput_PId').val(pid)
}

function FillModal(itemId) {
    AjaxCaller(type = "get", url = '/Admin/Menus?handler=GetById', data = { itemId: itemId }, callbackFunction = function (res) {
        if (res == null) {
            Toast(false, 'خطا در دریافت اطلاعات')
            return
        }

        OpenModal(modalId = '#mainModal', frmId = '#frm', isEdit = true)
        FillFormWithData(res)
        $('#VmInput_PId').val(pid)
        $('#VmInput_Id').val(itemId)
    })
}

function SubmitForm(url) {
    $("#frm").validate();

    if (!$("#frm").valid()) {
        return
    }
    AjaxCaller(type = "post", url = url, data = $('#frm').serialize(), callbackFunction = function (res) {
        Toast(res.Succeeded, res.Message)
        if(res.Succeeded){
            $('#mainModal').modal('hide')
        }
        table.draw()
    })
}