import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { CmdItem } from "../side-panel/cmd-item";

@Injectable()
export class EventBusService {
  private commands: BehaviorSubject<CmdItem[] | null> = new BehaviorSubject(
    null
  );
  Commands: Observable<CmdItem[]> = this.commands.asObservable();

  private panel: Subject<string> = new Subject<string>();
  Panel = this.panel.asObservable();

  constructor() {}

  setCmds(items: CmdItem[]) {
    this.commands.next(items);
  }

  triggerCmd(action: string) {
    this.panel.next(action);
  }
}
