import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AuthService } from "src/app/shared/auth/auth.service";

@Component({
  selector: "app-firebase",
  templateUrl: "./firebase.component.html",
  styleUrls: ["./firebase.component.scss"]
})
export class FirebaseComponent implements OnInit {
  constructor(private httpClient: HttpClient, public as: AuthService) {}

  currentUser: firebase.User;

  ngOnInit() {
    this.as.User.subscribe(user => {
      this.currentUser = user;
    });
  }

  resp: any;

  callCoreApi() {
    this.resp = null;
    this.httpClient
      .get("http://localhost:5000/api/secure")
      .subscribe(
        data => (this.resp = data),
        () => (this.resp = "401 - You are not authenticated")
      );
  }

  logOut() {
    this.as.logOff();
  }
}
