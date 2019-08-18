import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { RouterModule } from "@angular/router";
import { MaterialModule } from "../material.module";
import { SharedModule } from "../shared/shared.module";
import { DemoService } from "./demo.service";
import { DemosComponent } from "./demos.component";
import { WinAuthComponent } from "./winauth/winauth.component";
import { FirebaseComponent } from "./firebase/firebase.component";
import { AdalComponent } from './adal/adal.component';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    SharedModule,
    MaterialModule
  ],
  declarations: [DemosComponent, WinAuthComponent, FirebaseComponent, AdalComponent],
  providers: [DemoService]
})
export class DemosModule {}
