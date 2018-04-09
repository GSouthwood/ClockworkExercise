
function FirstAction() {

    
    var xhttp = new XMLHttpRequest();
    var params = "?timeZone=" + $("#selection").html();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            window.location.replace("http://localhost:50594/Home/GetTime");
            $(".loader").css("visibility", "visible");
        }
    };

    xhttp.open("GET", "http://localhost:50599/api/currenttime" + params, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.send();
    

}

function UserAction() {
    
    var xhttp = new XMLHttpRequest();
    var params = "?timeZone=" + $("#selection").html();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            location.reload();
            $(".loader").css("visibility", "visible");
        }
    };

    xhttp.open("GET", "http://localhost:50599/api/currenttime" + params, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.send();


}

$(document).ready(function () {

    var selection = $("#selection");
    var listItem = $("#zone");
    listItem.click(function (e) {
        if (listItem.val() != "--") {
            selection.html(listItem.val());
            
        }
    });
    
    
    
});