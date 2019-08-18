import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { SnackbarService } from "src/app/shared/snackbar/snackbar.service";
import { BalanceAccount } from "../../shared";
import { DataStoreService } from "../../shared/data-store/data-store-service";
import {
  ACCOUNT_CANCEL,
  ACCOUNT_SAVE
} from "../../shared/event-bus/action.types";
import { EventBusService } from "../../shared/event-bus/event-bus.service";
import { IconCancel, IconSave } from "../../shared/table/cmd.type";

@Component({
  selector: "app-account-detail",
  templateUrl: "./account-detail.component.html",
  styleUrls: ["./account-detail.component.scss"]
})
export class AccountDetailComponent implements OnInit {
  constructor(
    private store: DataStoreService,
    private eb: EventBusService,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private sns: SnackbarService
  ) {}

  acctForm: FormGroup;

  account: BalanceAccount = {
    ID: 0,
    Name: "",
    Expense: false,
    ActivatedOn: new Date(),
    Deprecated: false
  };

  ngOnInit() {
    this.getAcct();
    this.initForm();
    this.initEventBus();
    this.router.navigate(["", { outlets: { sidebarOutlet: null } }]);
  }

  initEventBus() {
    this.eb.setCmds([
      { title: "Save Account", action: ACCOUNT_SAVE, icon: IconSave },
      { title: "Cancle", action: ACCOUNT_CANCEL, icon: IconCancel }
    ]);

    this.eb.Panel.subscribe((action: string) => {
      switch (action) {
        case ACCOUNT_CANCEL:
          this.router.navigate(["/accounts/"]);
          break;
        case ACCOUNT_SAVE:
          this.store.saveAccount(<BalanceAccount>this.acctForm.value);
          break;
        default:
          console.log("invalid cmd in account details");
          break;
      }
    });
  }

  getAcct() {
    let id = this.route.snapshot.params["id"];
    this.store.getAccountById(id).subscribe(data => (this.account = data));
  }

  initForm() {
    this.acctForm = this.fb.group({
      ID: [this.account.ID],
      Name: [this.account.Name],
      Expense: [this.account.Expense]
    });
  }

  saveAccount() {
    this.store
      .saveAccount(<BalanceAccount>this.acctForm.value)
      .then(() => this.sns.displayAlert("Accout saved", "Vouchers"));
  }
}
