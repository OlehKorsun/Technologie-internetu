document.addEventListener("DOMContentLoaded", () => {
    fetch("nav.html")
        .then(response => response.text())
        .then(data => {
            document.getElementById("placeForNavbar").innerHTML = data;

            // подсвечиваем активный пункт меню
            const links = document.querySelectorAll("nav a");
            links.forEach(link => {
                if (link.href === window.location.href) {
                    link.classList.add("active");
                }
            });
        })
        .catch(error => console.error("Nie udało się załadować menu:", error));
});