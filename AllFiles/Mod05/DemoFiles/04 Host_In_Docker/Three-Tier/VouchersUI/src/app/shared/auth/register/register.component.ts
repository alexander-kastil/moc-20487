import { Component, OnInit } from "@angular/core";
import { AngularFireAuth } from "@angular/fire/auth";
import { AuthService } from "../auth.service";
import { SnackbarService } from "../../snackbar/snackbar.service";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.scss"]
})
export class RegisterComponent implements OnInit {
  constructor(
    public fbAuth: AngularFireAuth,
    public as: AuthService,
    private sns: SnackbarService
  ) {}

  ngOnInit() {}

  login = {
    displayName: "",
    email: "",
    pwd: "",
    pwd2: ""
  };

  registerUser() {
    if (this.login.pwd != this.login.pwd2) {
      this.sns.displayAlert("Passwords", "Your passwords dont match");
      this.login.pwd = "";
      this.login.pwd2 = "";
    } else {
      if (this.login.displayName == "") {
        this.login.displayName = this.login.email;
      }

      this.as
        .createUser(this.login.email, this.login.pwd)
        .then((user: firebase.User) => {
          this.sns.displayAlert(user.email, "Acct created - Please Login");

          let p = { displayName: this.login.displayName, photoURL: "" };
          user.updateProfile(p);
        });
    }
  }
}
