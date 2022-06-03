function Toast(succeeded, message) {
    var x = document.getElementById("snackbar");
    x.className = "show";
    x.innerText = message;
    if(succeeded){
        x.className +=" bg-success"
    }
    else{
        x.className +=" bg-danger"
    }
    
    setTimeout(function(){ 
        x.className = x.className.replace("show", "");
     }, 1500);
  }