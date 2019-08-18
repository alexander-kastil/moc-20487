import { Component, OnInit } from "@angular/core";
import { RecentFile } from "./model";
import { O365Service } from "./o365.service";
import { environment } from "src/environments/environment";

@Component({
  selector: "app-adal",
  templateUrl: "./adal.component.html",
  styleUrls: ["./adal.component.scss"]
})
export class AdalComponent implements OnInit {
  constructor(private service: O365Service) {}

  ngOnInit() {}

  evt = {
    Subject: "A Graph Event",
    Body: {
      ContentType: "HTML",
      Content: "The Super Fancy MS Graph Event"
    },
    Start: {
      DateTime: "2019-04-02T00:00:00",
      TimeZone: "UTC"
    },
    End: {
      DateTime: "2019-04-02T23:00:00",
      TimeZone: "UTC"
    }
  };

  recentFiles: RecentFile[];

  logIn() {
    this.service.logIn();
  }

  logOff() {
    this.service.logOut();
    environment.token = null;
  }

  getRecentFiles() {
    this.service.query(
      environment.o365Config.endpoints.graphApiUri,
      "/v1.0/me/drive/recent",
      response => {
        this.recentFiles = response.value.slice(0, 9);
        console.log(
          "Successfully fetched Recent Top Ten Documents:",
          this.recentFiles
        );
      }
    );
  }

  createEvent() {
    this.service.createEvent(this.evt, "/v1.0/me/calendar/events");
  }
}
