import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BalanceAccount } from "../shared/index";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";

@Injectable()
export class AccountsService {
  constructor(private httpClient: HttpClient) {}

  accounts = null;

  getAccounts(): Observable<BalanceAccount[]> {
    return this.httpClient.get<BalanceAccount[]>(
      environment.apiUrl + "api/accounts"
    );
  }

  getAccount(id: number): Observable<BalanceAccount> {
    return this.httpClient.get<BalanceAccount>(
      environment.apiUrl + "api/accounts/" + id
    );
  }

  insertAccount(acct: BalanceAccount): Observable<any> {
    return this.httpClient.post(environment.apiUrl + "api/accounts", acct);
  }

  updateAccount(acct: BalanceAccount): Observable<any> {
    return this.httpClient.put(environment.apiUrl + "api/accounts", acct);
  }

  deleteAccount(acct: BalanceAccount): Observable<any> {
    return this.httpClient.delete(
      environment.apiUrl + "api/accounts/" + acct.ID
    );
  }
}
