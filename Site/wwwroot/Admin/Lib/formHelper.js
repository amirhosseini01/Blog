function FillFormWithData(obj,frmId = '#frm'){
    $(frmId + " :input").each(function() {
        var keyName = $(this).attr('keyName')
        $(this).val(obj[keyName])
    })
}

function OpenModal(modalId = '#mainModal', frmId = '#frm', isEdit = false) {
    $(frmId).trigger('reset');

    if(isEdit){
        $('#modalTitle').text('افزودن')
        $('#btnEdit').show()
        $('#btnAdd').hide()
    }
    else{
        $('#modalTitle').text('ویرایش')
        $('#btnEdit').hide()
        $('#btnAdd').show()
    }

    $(modalId).modal('show');
}