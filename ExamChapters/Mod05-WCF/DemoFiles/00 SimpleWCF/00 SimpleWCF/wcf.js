var url = 'http://localhost:55110/demo.svc/';

function callWCF() {

    var response;

    $.ajax({
        type: 'POST',
        url: url + 'DoWork',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function(msg) {
            console.log("Response from WCF", msg);
        },

        error: function(e) {
            alert("Error  : " + e.statusText);
        }
    });
}