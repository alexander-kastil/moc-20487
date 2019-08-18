function loadPage(page, script) {
    var url = "/views/" + page;
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("GET", url, true);
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState === 4 && xmlhttp.status === 200) {
            document.getElementById("content").innerHTML = xmlhttp.responseText;
            if (script != null) {
                loadScript(script);
            }
        }
    }
    xmlhttp.send();
}

function loadScript(path) {
    var sc = "../js/" + path;
    var script = document.createElement("script");
    script.src = sc;
    script.type = "text/javascript";
    document.getElementsByTagName("head")[0].appendChild(script);
}