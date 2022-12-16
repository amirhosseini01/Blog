function DeleteAlert(callbackFunction) {
    Swal.fire({
        title: 'آیا مطمئن هستید؟',
        text: "در صورت حذف، قادر به بازیابی نمی باشید",
        icon: 'warning',
        showCancelButton: true,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر'
    }).then((result) => {
        if (result.isConfirmed) {
            if (typeof callbackFunction == "function") {
                callbackFunction.call(this, result.isConfirmed);
            }
        }
    })
}