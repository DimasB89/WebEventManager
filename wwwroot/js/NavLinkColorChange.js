window.onload = function () {
    var currentPage = window.location.pathname;
    var links = [
        { id: "navLink1", href: "/" },
        { id: "navLink2", href: "/Events/Create" }
    ];

    links.forEach(function (link) {
        var linkElement = document.getElementById(link.id);
        if (currentPage === link.href) {
            linkElement.style.backgroundColor = "#1f59a0";
            linkElement.style.color = "white";
        }
    });
}