document.addEventListener('DOMContentLoaded', function () {
    var currentPath = window.location.pathname;
    var menuItems = document.querySelectorAll('#menu li a');

    menuItems.forEach(function (menuItem) {
        if (menuItem.getAttribute('href') === currentPath) {
            menuItem.parentElement.classList.add('active');
        }
    });
});