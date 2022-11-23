window.onscroll = function () { scrolling() };

function scrolling() {
    var elementId = document.getElementById('scroll');
    var element = document.getElementsByClassName('clearfix');
    var height = element.offsetHeight;

    var height1 = 1452;
    if (document.documentElement.scrollTop >= 1452) {
        elementId.style.position = "absolute";
        elementId.style.top = "1600px";
        elementId.style.marginTop = "-100px";
    }
    else {
        elementId.style.position = "fixed";
        elementId.style.top = "70px";
    }
}