document.addEventListener("scroll", function() {
    const scrollTopBtn = document.querySelector(".scrollTopBtn");
    if (window.scrollY > 300) {
        scrollTopBtn.style.display = "block";
    } else {
        scrollTopBtn.style.display = "none";
    }
});

document.querySelector(".scrollTopBtn").addEventListener("click", function() {
    window.scrollTo({ top: 0, behavior: "smooth" });
});
