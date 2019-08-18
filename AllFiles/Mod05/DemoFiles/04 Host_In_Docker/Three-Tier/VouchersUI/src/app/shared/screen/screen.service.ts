import { Injectable } from "@angular/core";
import { Route, Router, RouterEvent } from "@angular/router";
import { BehaviorSubject, Observable, Subscription } from "rxjs";
import { ObservableMedia, MediaChange } from "@angular/flex-layout";

@Injectable()
export class ScreenService {
  //isDemo
  private demo: BehaviorSubject<boolean> = new BehaviorSubject(false);
  isDemo: Observable<boolean> = this.demo.asObservable();

  //isPhone
  watcher: Subscription;

  isPhone: boolean;
  isTablet: boolean;

  constructor(private router: Router, private obsMedia: ObservableMedia) {
    // this.subscribeDemo();
    this.subscribeIsPhone();
  }

  // subscribeDemo() {
  //   let children = this.router.config[0].children.map((item: Route) => {
  //     return item.path;
  //   });

  //   this.router.events.subscribe((evt: RouterEvent) => {
  //     if (evt.url != undefined) {
  //       let isChildRoute =
  //         children.find(item => evt.url.includes(item)) != undefined;

  //       evt.url == "/" || isChildRoute
  //         ? this.demo.next(true)
  //         : this.demo.next(false);
  //     }
  //   });
  // }

  subscribeIsPhone() {
    this.watcher = this.obsMedia.subscribe((change: MediaChange) => {
      switch (change.mqAlias) {
        case "xs":
          this.isPhone = true;
          this.isTablet = false;
          break;
        case "sm":
          this.isPhone = false;
          this.isTablet = true;
          break;
        default:
          this.isPhone = false;
          this.isTablet = false;
          break;
      }

      this.isPhone = change.mqAlias === "xs";
    });
  }
}
