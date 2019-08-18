import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { map } from "rxjs/operators";
import { BalanceAccount, Voucher } from "..";
import { AccountsService } from "../../accounts/account.service";
import { VouchersService } from "../../vouchers/voucher.service";
import { lateVoucher } from "./late-voucher";
import { environment } from "src/environments/environment";

@Injectable()
export class DataStoreService {
  constructor(private vs: VouchersService, private as: AccountsService) {
    if (environment.initDataservice) {
      this.initVouchers();
      this.initAccounts();
      this.addLateVoucher();
    }
  }

  //Vouchers

  private vouchersArray: Voucher[] = [];
  private vouchers: BehaviorSubject<Voucher[]> = new BehaviorSubject(
    this.vouchersArray
  );

  initVouchers() {
    this.vs.getVouchers().subscribe(data => {
      this.vouchersArray = data;
      this.vouchers.next(this.vouchersArray);
    });
  }

  addLateVoucher() {
    setTimeout(() => {
      this.vouchersArray.push(<Voucher>lateVoucher);
      this.vouchers.next(this.vouchersArray);
    }, 8000);
  }

  getAllVouchers(): Observable<Voucher[]> {
    return this.vouchers;
  }

  getVoucherById(id: number): Observable<Voucher> {
    return this.vouchers.pipe(map(m => m.find(mi => mi.ID == id)));
  }

  insertVoucher(v: Voucher): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      this.vs.insertVoucher(v).subscribe(
        () => {
          this.initVouchers();
          resolve();
        },
        err => reject(err)
      );
    });
  }

  updateVoucher(v: Voucher): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      this.vs.updateVoucher(v).subscribe(
        () => {
          this.initVouchers();
          resolve();
        },
        err => reject(err)
      );
    });
  }

  deleteVoucher(id: number): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      this.vs.deleteVoucher(id).subscribe(
        () => {
          this.initVouchers();
          resolve();
        },
        err => reject(err)
      );
    });
  }

  //Accounts
  private accountsArray: BalanceAccount[] = [];
  private accounts: BehaviorSubject<BalanceAccount[]> = new BehaviorSubject(
    this.accountsArray
  );

  initAccounts() {
    this.as.getAccounts().subscribe(data => {
      this.accountsArray = data;
      this.accounts.next(this.accountsArray);
    });
  }

  getAllAccounts(): Observable<BalanceAccount[]> {
    return this.accounts;
  }

  getAccountById(id: number): Observable<BalanceAccount> {
    return this.accounts.pipe(map(m => m.find(mi => mi.ID == id)));
  }

  deleteAccount(account: BalanceAccount): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      this.as.deleteAccount(account).subscribe(
        () => {
          this.initAccounts();
          resolve();
        },
        err => reject(err)
      );
    });
  }

  saveAccount(account: BalanceAccount): Promise<void> {
    if (account.ID == 0) {
      return new Promise<void>((resolve, reject) => {
        this.as.insertAccount(account).subscribe(
          () => {
            this.initAccounts();
            resolve();
          },
          err => reject(err)
        );
      });
    } else {
      return new Promise<void>((resolve, reject) => {
        this.as.updateAccount(account).subscribe(
          () => {
            this.initAccounts();
            resolve();
          },
          err => reject(err)
        );
      });
    }
  }
}
