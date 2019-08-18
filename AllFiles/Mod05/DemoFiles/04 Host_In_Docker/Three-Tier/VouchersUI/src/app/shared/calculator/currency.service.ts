import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { RatesResponse } from "./rates";
import { Observable } from "rxjs";

@Injectable()
export class CurrencyService {
  url: string = "https://api.fixer.io/latest";
  rates: Map<string, number>;

  constructor(private httpClient: HttpClient) {}

  getRates(): Observable<RatesResponse> {
    return this.httpClient.get<RatesResponse>(this.url);
  }
}
