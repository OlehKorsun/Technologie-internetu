function validateForm(){
    console.log("Jestem tu!")
    const errors = [];
    document.querySelectorAll('.error-msg').forEach(el => {el.innerText = '';});
    document.querySelectorAll('.error').forEach(el => {el.classList.remove("error");});
    document.getElementById('sumErr').innerText = '';

    const fname = document.getElementById('fname').value.trim();
    const lname = document.getElementById('lname').value.trim();
    const bdate = document.getElementById('bdate').value.trim();
    const email = document.getElementById('email').value.trim();
    const phone = document.getElementById('phone').value.trim();
    const source = document.getElementById('source').value;

    // Walidacja pola Imię
    if(fname.length < 2) {
        document.getElementById('fnameError').innerText = 'Imię powinno zawierać co najmniej 2 znaki';
        errors.push('Pole Imię!');
    }
    if(fname.length > 60) {
        document.getElementById('fnameError').innerText = 'Imię nie może być dłuższe niż 60 znaków';
        errors.push('Pole Imię!');
    }


    // Walidacja pola Nazwisko
    if(lname.length < 2) {
        document.getElementById('lnameError').innerText = 'Nazwisko powinno zawierać co najmniej 2 znaki';
        errors.push('Pole Nazwisko!');
    }
    if(lname.length > 60) {
        document.getElementById('lnameError').innerText = 'Nazwisko nie może być dłuższe niż 60 znaków';
        errors.push('Pole Nazwisko!');
    }


    // Walidacja pola birthdate
    if(!bdate){
        document.getElementById('bdateError').innerText = "Podaj datę urodzenia";
        document.getElementById('bdate').classList.add('error');
        errors.push('Podaj datę urodzenia!')
    } else {
        const birthDate = new Date(bdate);
        const today = new Date();
        const age = today.getFullYear() - birthDate.getFullYear();
        const month = today.getMonth() - birthDate.getMonth();
        const dayAdjust = month<0 || (month === 0 && today.getDate < birthDate.getDate()) ? 1 : 0;
        const realAge = age - dayAdjust;

        if(birthDate > today){
            document.getElementById('bdateError').innerText = 'Wprowadź poprawną datę urodzenia!';
            document.getElementById('bdate').classList.add('error');
            errors.push('Pole Birthdate');
        } else if(realAge < 16){
            document.getElementById('bdateError').innerText = 'Musisz mieć co najmniej 16 lat!';
            document.getElementById('bdate').classList.add('error');
            errors.push('Za młody!');
        }
    }


    // Walidacja pola Email
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if(!emailPattern.test(email)){
        document.getElementById('emailError').innerText = 'Podaj swój email';
        document.getElementById('email').classList.add('error');
        errors.push('Pole Email!');
    }


    // Walidacja pola Phone
    const phonePattern = /^\+[0-9]{9,15}$/;
    if(!phonePattern.test(phone)) {
        document.getElementById('phoneError').innerText = 'Podaj swój numer telefonu';
        document.getElementById('phone').classList.add('error');
        errors.push('Pole Phone!');
    }


    // Walidacja pola Źródło
    if(source===''){
        document.getElementById('sourceError').innerText = 'Wybierz jedną z opcji';
        document.getElementById('source').classList.add('error');
        errors.push('Pole Źródło');
    }


    if(errors.length > 0){
        console.log("Są jakieś błędy!" + errors.join("\n"));
        document.getElementById('sumErr').innerText = 'Do poprawy następujące błędy: ' + errors.join(' ');
        return false;
    }

    console.log('Wszysko dobrze!')
    alert("Dane zapisane. Dziękujemy za rejestrację!")
    return true;


}