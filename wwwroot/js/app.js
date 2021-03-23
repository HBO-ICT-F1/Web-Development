$(document).ready(function() {
    onWindowResize();
    $(window).resize(onWindowResize);
    $('.navbar-toggle').click(onNavbarToggle);
});

function onWindowResize() {
    var navbar = $("nav.navbar");
    if ($(this).width() < 768) {
        navbar.addClass('mobile');
    }
    if ($(this).width() >= 768 && navbar.hasClass('mobile')) {
        navbar.removeClass('mobile');
        navbar.removeClass('active');
    }
}

function onNavbarToggle() {
    var navbar = $("nav.navbar");
    if (navbar.hasClass('active')) {
        navbar.removeClass('active');
        return;
    }
    navbar.addClass('active');
}