import { Component, OnInit } from "@angular/core";
import {
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators
} from "@angular/forms";
import { MatSnackBar, MatSnackBarConfig } from "@angular/material";
import { ActivatedRoute, Router } from "@angular/router";
import { BalanceAccount, Voucher, VoucherDetail } from "../../shared";
import { DataStoreService } from "../../shared/data-store/data-store-service";
import {
  VOUCHER_ADD_DETAIL,
  VOUCHER_SAVE,
  VOUCHER_SAVE_DETAIL,
  VOUCHER_SHOW_LIST
} from "../../shared/event-bus/action.types";
import { EventBusService } from "../../shared/event-bus/event-bus.service";
import { IconAdd, IconCancel, IconSave } from "../../shared/table/cmd.type";
import { VoucherValidator } from "../voucher.validator";

@Component({
  selector: "app-voucher",
  templateUrl: "./voucher.component.html",
  styleUrls: ["./voucher.component.scss"]
})
export class VoucherComponent implements OnInit {
  constructor(
    private ds: DataStoreService,
    private route: ActivatedRoute,
    private ebus: EventBusService,
    private router: Router,
    private fb: FormBuilder,
    private snackBar: MatSnackBar
  ) {}

  voucher: Voucher = Voucher.init();
  accounts: BalanceAccount[];
  currentDetail: VoucherDetail;

  voucherForm: FormGroup;

  ngOnInit() {
    this.initData();
    this.initForm();
    this.router.navigate(["", { outlets: { sidebarOutlet: "upload" } }]);

    this.ebus.Panel.subscribe((action: string) => {
      this.evalAction("Voucher Action received", action);
    });
    this.setPanelCmds();
  }

  initData() {
    let id = this.route.snapshot.params["id"];
    this.ds.getAllAccounts().subscribe(data => (this.accounts = data));

    this.ds.getVoucherById(id).subscribe(data => {
      if (data != undefined) {
        this.voucher = data;
      }
    });
    this.setDetails(this.voucher);
  }

  initForm(): any {
    this.voucherForm = this.fb.group({
      Text: [this.voucher.Text, [Validators.required, Validators.minLength(2)]],
      Expense: [this.voucher.Expense],
      Date: [this.voucher.Date],
      Paid: [this.voucher.Paid],
      Amount: [this.voucher.Amount, [Validators.min(0)]]
    });
  }

  setPanelCmds() {
    this.ebus.setCmds([
      { title: "Save Voucher", action: VOUCHER_SAVE, icon: IconSave },
      { title: "New Detail", action: VOUCHER_ADD_DETAIL, icon: IconAdd },
      { title: "Save Detail", action: VOUCHER_SAVE_DETAIL, icon: IconSave },
      { title: "Cancel", action: VOUCHER_SHOW_LIST, icon: IconCancel }
    ]);
  }

  evalAction(msg: string, action: string) {
    switch (action) {
      case VOUCHER_SAVE:
        this.saveVoucher();
        break;
      case VOUCHER_ADD_DETAIL:
        this.addDetail();
        break;
      case VOUCHER_SHOW_LIST:
        this.showVouchers();
        break;
      case VOUCHER_SAVE_DETAIL:
        // this.saveDetail();
        break;
      default:
        console.error("Invalid cmd in account details");
        break;
    }

    this.snackBar.open(msg, action, {
      duration: 2000
    });
  }

  //Voucher

  showVouchers() {
    this.router.navigate(["/vouchers/"]);
  }

  saveVoucher() {
    let vvs = this.voucherForm.value;
    let vts = Object.assign({}, this.voucher, vvs);

    if (VoucherValidator.validate(vts)) {
      if (this.voucher.ID == 0) {
        this.ds.insertVoucher(vts);
      } else {
        this.ds.updateVoucher(vts);
      }
      this.snackBar.open("Voucher Saved", "Save", {
        duration: 2000
      });
    } else {
      let config = new MatSnackBarConfig();
      config.duration = 2000;
      this.snackBar.open("Validation Error", "Save", config);
    }
  }

  violatesMinLenght() {
    let result = false;
    let errs: ValidationErrors = this.voucherForm.controls.Text.errors;

    if (errs != null) {
      if (errs["minlength"]) {
        result = true;
      }
    }
    return result;
  }

  //Detail

  setDetails(v: Voucher) {
    if (v.Details != null) {
      v.Details.forEach((d: VoucherDetail) => {
        d.Account = this.accounts.find(a => a.ID == d.AccountID);
      });
      this.currentDetail = v.Details[0];
    } else {
      this.voucher.Details = new Array<VoucherDetail>();
    }
  }

  selectDetail(detail) {
    this.currentDetail = detail;
  }

  saveDetail(vd: VoucherDetail) {
    vd.Account = this.accounts.find(a => a.ID == vd.AccountID);
    let modified = Object.assign({}, this.currentDetail, vd);

    if (this.currentDetail.ID != 0) {
      this.voucher.Details.splice(
        this.voucher.Details.indexOf(this.currentDetail),
        1,
        modified
      );
    } else {
      this.voucher.Details.push(modified);
    }
    this.selectDetail(modified);
  }

  addDetail() {
    this.currentDetail = <VoucherDetail>{
      ID: 0,
      VoucherID: this.voucher.ID,
      Account: null,
      Text: "",
      Comment: ""
    };
  }

  deleteDetail(vd: VoucherDetail) {
    this.voucher.Details.splice(this.voucher.Details.indexOf(vd), 1);
  }
}
