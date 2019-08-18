import { BOOL_TYPE } from "@angular/compiler/src/output/output_ast";

export class VoucherDetail {
  ID: number;
  VoucherID: number;
  AccountID: number;
  Account: BalanceAccount;
  Text: string;
  Amount: number;
  Comment: string;
}

export class BalanceAccount {
  ID: number;
  Name: string;
  Expense: boolean;
  ActivatedOn: Date;
  Deprecated: boolean;
}

export class Voucher {
    ID: number;
    Text: string;
    Date: string;
    Amount: number;
    Paid: boolean;
    Expense: boolean;
    Remark?: boolean;
    Readonly?: boolean;
    Details?: VoucherDetail[];

    static init(): Voucher {
        return {
          ID: 0,
          Text: "",
          Date: new Date().toString(),
          Amount: 0,
          Paid: false,
          Expense: false,
          Remark: false,
          Details: new Array<VoucherDetail>()
        };
      }
}    
