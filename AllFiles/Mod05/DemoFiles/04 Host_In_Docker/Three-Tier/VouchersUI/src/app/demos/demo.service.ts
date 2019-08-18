import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DemoItem } from "./demoItem";

@Injectable()
export class DemoService {
  constructor(private http: HttpClient) {}

  getItems(): Observable<DemoItem[]> {
    return this.http.get<DemoItem[]>("/assets/demos.json");
  }
}
