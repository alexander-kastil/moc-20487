import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { VoucherDetail, BalanceAccount } from '../../../shared/index';

@Component({
  selector: 'app-voucher-detail',
  templateUrl: './voucher-detail.component.html',
  styleUrls: ['./voucher-detail.component.scss']
})
export class VoucherDetailComponent implements OnInit {
  @Input() detail : VoucherDetail;
  @Input() accounts: BalanceAccount[];
  @Output() detailSaved : EventEmitter<VoucherDetail> = new EventEmitter();
  
  constructor() { }

  ngOnInit() {
    console.log("received accounts: ", this.accounts)
  }

  saveDetail(){
    this.detailSaved.emit(this.detail);
  }

}
