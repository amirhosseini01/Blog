$(document).ready(function () {
    var columns = [
        { data: 'Id' },
        { data: 'Title' },
        { data: 'CategoryTitle' },
        { data: 'CategoryId' },
        { data: 'IsHidden' },
        { data: 'CreateDate' },
        { data: 'PersianCreateDate' },
        { data: 'UpdateDate' },
        { data: 'PersianUpdateDate' }
    ]
    initDataTbl(tblId = "#tbl", serverUrl = "/Admin/Blogs?handler=List", columns = columns)
});