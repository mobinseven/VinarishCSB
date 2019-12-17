
window.SaveScrollPosition = function () {
    var LastPage = localStorage.getItem("LastPage");
    var scrollPosition = document.getElementsByClassName("mat-drawer-app-content")[0].scrollTop;
    localStorage.setItem("scrollPosition_" + LastPage, scrollPosition.toString());
}
window.SaveCurrentPagePath = function () {
    var pathName = document.location.pathname;
    localStorage.setItem("LastPage", pathName);
}
window.LoadScrollPosition = function () {
    var pathName = document.location.pathname;
    var scroll = parseInt(localStorage.getItem("scrollPosition_" + pathName));
    if (!isNaN(scroll))
        document.getElementsByClassName("mat-drawer-app-content")[0].scrollTop = scroll;

}