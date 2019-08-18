getVouchers();

function getVouchers() {
    var url = "/vouchers.json";
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log("query successful, data received: " + JSON.stringify(msg));
            buildTbl(msg);
        },
        error: function (msg) {
            console.log(msg.responseText);
        }
    });
}

function buildTbl(vouchers) {
    var strTbl = "<table style='width: 90%'><tbody>";
    var strTh = "<tr style='text-align: left'>";
    var strData = "";
    for (var i = 0; i < vouchers.length; ++i) {
        strData += "<tr>";
        var id = vouchers[i][Object.keys(vouchers[i])[0]];
        console.log(id);
        for (var prop in vouchers[i]) {
            console.log(prop + "=" + vouchers[i][prop]);
            if (i === 0) {
                if (prop === "ID") {
                    strTh += "<th>&nbsp;</th>";
                } else {
                    strTh += "<th>" + prop + "</th>";
                }
            }
            if (prop === "ID") {
                strData += "<td><a href=\"javascript:loadPage('voucherDetails.htm', 'voucherDetails.js', " + id + ")\" style=\'cursor: pointer;\'>Details</a>";
            } else {
                strData += "<td>" + vouchers[i][prop] + "</td>";
            }
        }
        strData += "</tr>";
    }
    strTbl += strTh + "<tr>" + strData + "</table>";
    document.getElementById("tblVouchers").innerHTML = strTbl;
}