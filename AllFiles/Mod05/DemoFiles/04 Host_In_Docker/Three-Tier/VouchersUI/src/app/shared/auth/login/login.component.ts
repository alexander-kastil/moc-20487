import { Component, OnInit } from "@angular/core";
import { SnackbarService } from "../../snackbar/snackbar.service";
import { AuthService } from "../auth.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {
  constructor(public sns: SnackbarService, public auth: AuthService) {}

  ngOnInit() {}

  login = {
    email: "",
    pwd: ""
  };

  logIn() {
    if (!this.login.email || !this.login.pwd) {
      this.sns.displayAlert("Error !", "You must enter email and password");
    } else {
      this.auth.logOn(this.login.email, this.login.pwd).then(returned => {
        this.sns.displayAlert("Logon", "you are now logged on");
      });
    }
  }
}
