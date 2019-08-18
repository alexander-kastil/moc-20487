import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EventBusService } from '../event-bus/event-bus.service';
import { CmdItem } from './cmd-item';

@Component({
  selector: 'app-side-panel',
  templateUrl: './side-panel.component.html',
  styleUrls: ['./side-panel.component.scss']
})
export class SidePanelComponent implements OnInit { 

  cmds : CmdItem[];
  editorDisplayed: boolean;

  constructor(private router: Router, private ebus : EventBusService) { }

  ngOnInit() {
    this.ebus.Commands.subscribe(items=>this.cmds = items);
    this.editorDisplayed = false;
  }

  dispatchAction(action : string){
    this.ebus.triggerCmd(action);
  }

  toggleEditor(){
    this.editorDisplayed = !this.editorDisplayed;
    if(this.editorDisplayed){
      this.router.navigate(['',{outlets: { sidebarOutlet : 'showeditor'}}])      
    }
    else{
      this.router.navigate(['',{outlets: { sidebarOutlet : null}}])
    }
  }

  showUpload(){
    this.router.navigate(['',{outlets: { sidebarOutlet : 'upload'}}])
  }


}
