function Toast(succeeded, message) {
    var x = document.getElementById("snackbar");
    x.className = "show";
    x.innerText = message;
    if(succeeded){
        x.className +=" bg-green"
    }
    else{
        x.className +=" bg-red"
    }
    
    setTimeout(function(){ 
        x.className = x.className.replace("show", "");
     }, 2000);
  }