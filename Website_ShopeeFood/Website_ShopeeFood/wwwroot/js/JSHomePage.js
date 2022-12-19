

window.onscroll = function () { scrolling() };

function scrolling() {
    var elementId = document.getElementById('scroll');
    var element = document.getElementsByClassName('clearfix');
    var height = element.offsetHeight;

    if (document.documentElement.scrollTop >= 1452) {
        elementId.style.position = "absolute";
        elementId.style.top = "1600px";
        elementId.style.marginTop = "-100px";
    }
    else {
        elementId.style.position = "fixed";
        elementId.style.top = "90px";
    }
}

function getItem(item) {

    const areaID = document.getElementsByClassName('dropdown-menu');

    const nameArea = document.getElementsByClassName('odropdown');

    const item_Area = document.getElementById('item-area');

    for (var i = 1; i <=areaID.childElementCount; i++)
    {
        if (i == item) {
            nameArea.innerHTML = item_Area;
        }
    }
}

