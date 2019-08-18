import { Component, OnInit } from "@angular/core";
import { Voucher } from "..";
import { DataStoreService } from "../data-store/data-store-service";

@Component({
  selector: "app-kpi-bar",
  templateUrl: "./kpi-bar.component.html",
  styleUrls: ["./kpi-bar.component.scss"]
})
export class KpiBarComponent implements OnInit {
  log: boolean = false;
  runningSum: number = 0;
  vouchers: Voucher[];

  constructor(private dataStore: DataStoreService) {}

  ngOnInit() {
    this.dataStore.getAllVouchers().subscribe((vouchers: Voucher[]) => {
      this.runningSum = vouchers.reduce((prev, item) => prev + item.Amount, 0);
    });
  }
}
