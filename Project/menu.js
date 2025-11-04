document.addEventListener("DOMContentLoaded", () => {
    fetch("nav.html")
        .then(response => response.text())
        .then(data => {
            document.getElementById("placeForNavbar").innerHTML = data;

            // gdybym chciał podświetlić w menu na której stronie jestem
            // const links = document.querySelectorAll("nav a");
            // links.forEach(link => {
            //     if (link.href === window.location.href) {
            //         link.classList.add("active");
            //     }
            // });

        })
        .catch(error => console.error("Nie udało się załadować menu:", error));



    fetch("footer.html")
        .then(response => response.text())
        .then(data => {
            document.getElementById("placeForFooter").innerHTML = data;


        })
        .catch(error => console.error("Nie udało się załadować footera:", error));
});