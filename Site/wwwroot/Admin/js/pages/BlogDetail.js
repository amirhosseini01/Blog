$(document).ready(function () {
    resizeImgSetup(file_input = '#imgInput', result_input = '#VmInput_ImgUrl');
});

function SubmitForm(url) {
    AjaxCaller(type = "post", url = url, data = $('#frm').serialize(), callbackFunction = function (res) {

    })
}