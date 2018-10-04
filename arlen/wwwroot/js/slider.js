$('.news-events-owl').owlCarousel({
    loop: true,
    nav: true,
    margin: 30,
    navText: ["<div class='prev'></div>", "<div class='next'></div>"],
    responsive: {
        0: {
            items: 1
        },
        768: {
            items: 2,
        },
        1024: {
            items: 3
        }
    }
});

$('.customers-owl').owlCarousel({
    loop: true,
    nav: true,
    margin: 30,
    navText: ["<div class='prev'></div>", "<div class='next'></div>"],
    autoplay: true,
    autoplayTimeout: 3000,
    autoplayHoverPause: true,
    responsive: {
        0: {
            items: 1
        },
        576: {
            items: 2
        },
        768: {
            items: 3
        },
        1200: {
            items: 4
        }
    }
})