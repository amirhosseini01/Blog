$(document).ready(function () {
    resizeImgSetup(file_input = $('#imgInput'), result_input = $('#VmInput_ImgBase64'));
});

function SubmitForm(url) {
    AjaxCaller(type = "post", url = url, data = $('#frm').serialize(), callbackFunction = function (res) {
        Toast(res.succeeded,res.message)
        // if(res.succeeded){
        //     setTimeout(() => {
        //         window.location.replace("/Admin/Blogs");
        //     }, 1500);
        // }
    })
}