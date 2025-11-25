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
        errors.push('Imię powinno zawierać co najmniej 2 znaki!');
    }
    if(fname.length > 60) {
        document.getElementById('fnameError').innerText = 'Imię nie może być dłuższe niż 60 znaków';
        errors.push('Imię nie może być dłuższe niż 60 znaków!');
    }


    // Walidacja pola Nazwisko
    if(lname.length < 2) {
        document.getElementById('lnameError').innerText = 'Nazwisko powinno zawierać co najmniej 2 znaki';
        errors.push('Nazwisko powinno zawierać co najmniej 2 znaki!');
    }
    if(lname.length > 60) {
        document.getElementById('lnameError').innerText = 'Nazwisko nie może być dłuższe niż 60 znaków';
        errors.push('Nazwisko nie może być dłuższe niż 60 znaków!');
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
            document.getElementById('bdateError').innerText = 'Data urodzenia nie może być późniejsza niż dzisiaj!';
            document.getElementById('bdate').classList.add('error');
            errors.push('Data urodzenia nie może być późniejsza niż dzisiaj!');
        } else if(realAge < 16){
            document.getElementById('bdateError').innerText = 'Musisz mieć co najmniej 16 lat!';
            document.getElementById('bdate').classList.add('error');
            errors.push('Jesteś za młody, nie możesz się zarejestrować!');
        }
    }


    // Walidacja pola Email
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if(!email){
        document.getElementById('emailError').innerText = 'Pole email jest wymagane!';
        document.getElementById('email').classList.add('error');
        errors.push('Pole email jest wymagane!');
    } else if(!emailPattern.test(email)){
        document.getElementById('emailError').innerText = 'Niepoprawny email!';
        document.getElementById('email').classList.add('error');
        errors.push('Podaj prawidłowy email!');
    }


    // Walidacja pola Phone
    const phonePattern = /^\+[0-9]{9,15}$/;
    if(!phone){
        document.getElementById('phoneError').innerText = 'Pole number jest wymagane!';
        document.getElementById('phone').classList.add('error');
        errors.push('Pole number jest wymagane!');
    } else if(!phonePattern.test(phone)) {
        document.getElementById('phoneError').innerText = 'Niepoprawny numer! Musi zaczynać się od + i mieć 9-15 cyfr';
        document.getElementById('phone').classList.add('error');
        errors.push('Niepoprawny numer!');
    }


    // Walidacja pola Źródło
    if(source===''){
        document.getElementById('sourceError').innerText = 'Wybierz jedną z opcji';
        document.getElementById('source').classList.add('error');
        errors.push('Pole źródło jest wymagane!');
    }


    if(errors.length > 0){
        document.getElementById('sumErr').innerText = 'Do poprawy następujące błędy:\n* ' + errors.join('\n * ');
        return false;
    }
    
    alert("Dane zapisane. Dziękujemy za rejestrację!")
    return true;
}