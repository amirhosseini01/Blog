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
                return `<input type="number" class="form-control" value="${data}" onchange="ChangeOrder(${row.Id},this.value, 'Blogs')">`
            }
        },
        { data: 'Title' },
        { data: 'CategoryTitle' },
        { data: 'CategoryId', visible: false },
        {
            data: 'IsHidden',
            render: function (data, type, row, meta) {
                return `
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" ${data ? 'checked' : ''} onchange="ToggleStatus(${row.Id},'/Admin/Blogs?handler=ToggleStatus')">
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
                var html = `<a href="/Admin/BlogDetail/${data}" class="btn btn-warning btn-sm">
                            <i class="fa fa-edit"></i>
                        </a>`;
                html += `<button type="button" onclick="DeleteItem('${data}')" class="btn btn-danger btn-sm margin">
                                <i class="fa fa-trash"></i>
                            </button>`;
                return html;
            }
        }
    ]
    initDataTbl(tblId = "#tbl", serverUrl = "/Admin/Blogs?handler=List", columns = columns, columnDefs = [
        {
            targets: [6],
            orderData: [5]
        },
        {
            targets: [8],
            orderData: [7]
        },
    ])
}


function DeleteItem(id) {
    DeleteAlert(callbackFunction = function () {
        AjaxCaller(type = "get", url = '/Admin/Blogs?handler=Delete', data = { itemId: id }, callbackFunction = function (res) {
            Toast(res.Succeeded, res.Message)
            if (res.Succeeded) {
                CreateTbl()
            }
        })

    })
}