$(document).ready(function () {
    var columns = [
        { data: 'Id' },
        { data: 'Title' },
        { data: 'CategoryTitle' },
        { data: 'CategoryId', visible: false },
        {
            data: 'IsHidden',
            render: function (data, type, row, meta) {
                return `
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" ${data ? 'checked' : ''}>
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
                return `<a href="/Admin/BlogDetail/${data}" class="btn btn-warning">
                            <i class="fa fa-edit"></i>
                        </a>`;
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
});