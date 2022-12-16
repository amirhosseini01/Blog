function AjaxCaller(type, url, data, callbackFunction) {
    $.ajax({
        type: type,
        url: url,
        data: data,
        success: function (res) {
            if (typeof callbackFunction == "function") {
                callbackFunction.call(this, res);
            }
        },
        error: function (jqXHR, exception) {
            LogError(jqXHR, exception);
        },
    });
}

function LogError(jqXHR, exception) {
    let msg = "";
    if (jqXHR.status === 0) {
      msg = "Not connect.\n Verify Network.";
    } else if (jqXHR.status == 404) {
      msg = "Requested page not found. [404]";
    } else if (jqXHR.status == 500) {
      msg = "Internal Server Error [500].";
    } else if (exception === "parsererror") {
      msg = "Requested JSON parse failed.";
    } else if (exception === "timeout") {
      msg = "Time out error.";
    } else if (exception === "abort") {
      msg = "Ajax request aborted.";
    } else {
      msg = "Uncaught Error.\n" + jqXHR.responseText;
    }
    console.log(msg);
  }