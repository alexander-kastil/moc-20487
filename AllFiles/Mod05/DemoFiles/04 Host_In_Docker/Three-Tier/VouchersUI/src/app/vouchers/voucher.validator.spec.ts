import { VoucherValidator } from './voucher.validator';

describe("Voucher Validater works", function () {
    
        var voucherSum;
        var gooddetails;
        var baddetails;
        var voucherhead;
        var goodvoucher;
        var badvoucher;
        var nullVoucher;
    
        beforeEach(function() {

            goodvoucher = {
                "ID": 2,
                "Text": "BP Tankstelle",
                "Date": "2016-11-15T00:00:00",
                "Amount": 40,
                "Paid": false,
                "Expense": false,
                "Remark": true,
                "Details": [
                    {
                        "ID": 2, 
                        "VoucherID": 3, 
                        "AccountID": 1, 
                        "Text": "USB Stick",
                        "Amount": 11, 
                        "Comment": null
                    },
                    {
                        "ID": 7,
                        "VoucherID": 3,
                        "AccountID": 6,
                        "Text": "Game of Thrones, Season 6",
                        "Amount": 29,
                        "Comment": null
                    }
                ]
            };

            badvoucher = {
                "ID": 2,
                "Text": "BP Tankstelle",
                "Date": "2016-11-15T00:00:00",
                "Amount": 650,
                "Paid": false,
                "Expense": false,
                "Remark": true,
                "Details": [
                    {
                        "ID": 2,
                        "VoucherID": 3,
                        "AccountID": 1,
                        "Text": "USB Stick",
                        "Amount": 11,
                        "Comment": null
                    },
                    {
                        "ID": 7,
                        "VoucherID": 3,
                        "AccountID": 6,
                        "Text": "Game of Thrones, Season 6",
                        "Amount": 55,
                        "Comment": null
                    }
                ]
            }

            nullVoucher = {
                "ID": 2,
                "Text": "BP Tankstelle",
                "Date": "2016-11-15T00:00:00",
                "Amount": 650,
                "Paid": false,
                "Expense": false,
                "Remark": true,
                "Details": null
            };
        });    
            
        it("returns true when correct data is passed", function () {
            expect(VoucherValidator.validate(goodvoucher)).toEqual(true);
        });
    
        it("returns false when bad data is passed", function () {
            expect(VoucherValidator.validate(badvoucher)).toEqual(false);
        });
    
        it("returns false when null is passed as Details", function () {       
            debugger;    
            expect(VoucherValidator.validate(nullVoucher)).toEqual(false);
        });
    
    });