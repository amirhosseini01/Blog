$(document).ready(function () {
    resizeImgSetup(file_input = $('#imgInput'), result_input = $('#VmInput_ImgBase64'));

    initTinymce()
});

function SubmitForm(url) {
    tinyMCE.triggerSave();
    $("#frm").validate();

    if (!$("#frm").valid()) {
        return
    }
    AjaxCaller(type = "post", url = url, data = $('#frm').serialize(), callbackFunction = function (res) {
        Toast(res.Succeeded, res.Message)
        if (res.Succeeded) {
            setTimeout(() => {
                window.location.replace("/Admin/Blogs");
            }, 2000);
        }
    })
}