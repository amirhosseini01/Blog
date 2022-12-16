function resizeImgSetup(file_input, result_input) {
    file_input.resizeImg(function () {
        let type, quality;
        type = "image/jpeg";
        quality = 0.8;
        return {
            use_reader: false,
            mode: 2,
            val: 400,
            type: type,
            quality: quality,

            callback: function (result) {
                result_input.val(result)
            }
        };
    });
}

function ReadFile(input, imgRes, imgParent) {
    var url = $(input).val();
    var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
    if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
        var reader = new FileReader();

        reader.onload = function (e) {
            ShowImageHolder(imgRes = imgRes, imgParent = imgParent, srcVal = e.target.result)  
        }
        reader.readAsDataURL(input.files[0]);
    }
    else {
        HideImageHolder(imgRes = imgRes, imgParent = imgParent)
    }
}

function ShowImageHolder(imgRes, imgParent, srcVal) {
    $(imgRes).attr('src', srcVal);
    $(imgParent).show();
}
function HideImageHolder(imgRes, imgParent) {
    $(imgRes).attr('src', "");
    $(imgParent).show();
}