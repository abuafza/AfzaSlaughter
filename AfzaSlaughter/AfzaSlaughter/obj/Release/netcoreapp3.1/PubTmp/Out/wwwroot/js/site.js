// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

//for About slide
$('.carousel').carousel({
    interval: 3000
});

//for Initiate gallery lightbox 
const galleryLightbox = GLightbox({
    selector: '.gallery-lightbox'
});

//for chart
//pie
//let ctxP = document.getElementById("pieChart").getContext('2d');
//let myPieChart = new Chart(ctxP, {
//    type: 'pie',
//    data: {
//        labels: ["Red", "Green", "Yellow", "Grey", "Dark Grey"],
//        datasets: [{
//            data: [300, 50, 100, 40, 120],
//            backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"],
//            hoverBackgroundColor: ["#FF5A5E", "#5AD3D1", "#FFC870", "#A8B3C5", "#616774"]
//        }]
//    },
//    options: {
//        responsive: true
//    }
//});