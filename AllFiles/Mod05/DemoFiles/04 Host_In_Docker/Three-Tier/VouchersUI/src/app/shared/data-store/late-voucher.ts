
export const lateVoucher = {
    ID: 99,
    Text: "Late Voucher",
    Date: new Date().toString(),
    Amount: 1000,
    Paid: false,
    Expense: false,
    Remark: true,
    Details: [
      {
        ID: 4,
        VoucherID: 2,
        AccountID: 2,
        Account: null,
        Text: "Diesel",
        Amount: 45,
        Comment: null
      },
      {
        ID: 6,
        VoucherID: 2,
        AccountID: 2,
        Account: null,
        Text: "Reifenwechsel",
        Amount: 20,
        Comment: null
      }
    ]
  };
