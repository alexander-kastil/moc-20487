import { Voucher } from '../shared/model/model';

export class VoucherValidator{

    static validate(voucher : Voucher)
    {
        var detailSumOk: boolean;        
        if (voucher.Details!=null) {
            var sumD = 0;
            for(let vd of voucher.Details){
                sumD += vd.Amount;
            }
            detailSumOk = sumD == voucher.Amount;
        }
        return detailSumOk;
    }
}