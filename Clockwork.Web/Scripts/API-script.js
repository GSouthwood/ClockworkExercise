
function UserAction() {
    var xhttp = new XMLHttpRequest();
    var params = "?timeZone=" + $("p#selection").html();
xhttp.onreadystatechange = function() {
    if (this.readyState == 4 && this.status == 200) {
     location.reload();
    }
};

xhttp.open("GET", "http://localhost:50599/api/currenttime" + params, true);
xhttp.setRequestHeader("Content-type", "application/json");
xhttp.send();


}

$(document).ready(function () {

    var listItem = $("#zone");
    var selection = $("p#selection");
    listItem.click( function (e) {
        selection.html(listItem.val());
    });
});