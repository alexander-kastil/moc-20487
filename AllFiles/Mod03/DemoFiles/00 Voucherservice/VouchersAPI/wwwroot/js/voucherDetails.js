getFullVoucher();

function getFullVoucher() {
    var url = "/api/vouchers/1";
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (voucher) {
            if (voucher != null) {
                setVoucherData(voucher);
                setDetailsTable(voucher);
            }
        },
        error: function (data) {
            console.log(data.responseText);
        }
    });
}

function setVoucherData(voucher) {
    document.getElementById("txtVoucherText").value = voucher.Text;
    var strDtGerman = new Date(voucher.Date).toLocaleDateString();
    document.getElementById("txtDate").value = strDtGerman;
    document.getElementById("chkPaid").checked = voucher.Paid;
    if (voucher.Expense) {
        document.getElementById("rbExpense").checked = true;
    } else {
        document.getElementById("rbIncome").checked = true;
    }
}

function setDetailsTable(voucher) {

    for (var i = 0; i < voucher.Details.length; i += 1) {
        var item = voucher.Details[i];
        $('#tblVoucherDetails').append(
          '<tr id="' + item.ID + '" style="cursor:pointer">' +
            '<td>' + item.Text + '</td>' +
            '<td>' + item.Amount + '</td>' +
            '<td>' + item.AccountID + '</td>' +
            '<td>&nbsp;</td>' +
          '</tr>');
    }
}
