function validateForm(){
    let x = document.forms['myForm']['fname'].value;
    if(x == null || x === ""){
        alert("Please enter a valid name");
        return false;
    }
    return true;
}