function onErr(msg) {
    console.log(msg.responseText);
    $("#divOutput").html(msg);
}

function output(msg) {
    console.log(msg);
    $("#divOutput").html(msg);
}

function getVouchers() {
    debugger;
    var url = "/api/vouchers";
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            output("query successful, data received: " + JSON.stringify(msg));
        },
        error: onErr
    });
}

function getVoucher() {
    debugger;
    var url = "/api/vouchers/2";
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            output("query successful, data received: " + JSON.stringify(msg));
        },
        error: onErr
    });
}

function insertVoucher() {
    debugger;
    var url = "/api/vouchers";
    var data = JSON.stringify({ Text: "Inserted by WebApi", Date: new Date() });
    $.ajax({
        type: "POST",
        data: data,
        url: url,
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            output("query successful, data received: " + JSON.stringify(msg));
        },
        error: onErr
    });
}

function updateVoucher() {
    debugger;
    var id = 1003;
    var url = "/api/vouchers/" + id;
    var vtu = JSON.stringify({ "ID": id, "Text": "Updated by WebApi", "Date": "2016-04-22T16:59:32.086", "Amount": 99, "Paid": true, "Expense": false, "VATRate" : 20 });
    $.ajax({
        type: "PUT",
        data: vtu,
        url: url,
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            output("query successful, voucher updated - id:" + id);
        },
        error: onErr
    });
}

function deleteVoucher() {
    debugger;
    var id = 3003;
    var url = "/api/vouchers/" + id;
    $.ajax({
        type: "DELETE",
        url: url,
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            output("query successful, voucher deleted");
        },
        error: onErr
    });
}

function getVoucherDetails() {
    debugger;
    var url = "/api/voucherDetails";
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            output("query successful, data received: " + JSON.stringify(msg));
        },
        error: onErr
    });
}

function getVoucherDetail() {
    debugger;
    var url = "/api/voucherDetails/1";
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            output("query successful, data received: " + JSON.stringify(msg));
        },
        error: onErr
    });
}

function insertVoucherDetail() {
    debugger;
    var url = "/api/voucherDetails";
    var data = JSON.stringify({VoucherID: 1, AccountID: 1,  Text: "Detail Inserted by WebApi", Amount: 22 });
    $.ajax({
        type: "POST",
        data: data,
        url: url,
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            output("query successful, data received: " + JSON.stringify(msg));
        },
        error: onErr
    });
}

function updateVoucherDetail() {
    debugger;
    var id = 1;
    var url = "/api/voucherDetails/" + id;
    var vtu = JSON.stringify({ VoucherID: 1, AccountID: 1, Text: "Detail Updated by WebApi", Amount: 22 });
    $.ajax({
        type: "PUT",
        data: vtu,
        url: url,
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            output("query successful, voucherdetail updated - id:" + id);
        },
        error: onErr
    });
}

function deleteVoucherDetail() {
    debugger;
    var id = 1;
    var url = "/api/voucherDetails/" + id;
    $.ajax({
        type: "DELETE",
        url: url,
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            output("query successful, voucherdetail deleted - id:" + id);
        },
        error: onErr
    });
}

function getSum() {
    debugger;
    var url = "/api/vouchers/getsum/true";
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            output("query successful, data received: " + JSON.stringify(msg));
        },
        error: onErr
    });
}

function getVM() {
    debugger;
    var url = "/api/vouchers/getvm/1";
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            output("query successful, data received: " + JSON.stringify(msg));
        },
        error: onErr
    });
}

//Execute once as it is and once with an id of 0
function doSave() {
    debugger;
    var voucher = {
        "ID": 2,
        "Text": "BP Tankstelle",
        "Date": "2017-06-27T14:30:04.8849651",
        "Amount": 65,
        "Paid": false,
        "Expense": true,
        "Remark": true
    };
    var url = "/api/vouchers/save/";
    $.ajax({
        type: "POST",
        data: JSON.stringify(voucher),
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            output("query successful, data received: " + JSON.stringify(msg));
        },
        error: onErr
    });
}

