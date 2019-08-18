import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material";

@Injectable({
  providedIn: "root"
})
export class SnackbarService {
  constructor(private snackBar: MatSnackBar) {}

  displayAlert(title: string, msg: string) {
    this.snackBar.open(title, msg, {
      duration: 1000
    });
  }
}
