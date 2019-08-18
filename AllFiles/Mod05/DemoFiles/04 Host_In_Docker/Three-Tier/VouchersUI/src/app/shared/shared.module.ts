import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { FileUploadModule } from "ng2-file-upload";
import { CKEditorModule } from "ngx-ckeditor";
import { MatchHeightDirective, NavbarComponent } from ".";
import { MaterialModule } from "../material.module";
import { AuthService } from "./auth/auth.service";
import { CalculatorComponent } from "./calculator/calculator.component";
import { CurrencyService } from "./calculator/currency.service";
import { CheckPipe } from "./checked/check.pipe";
import { DataStoreService } from "./data-store/data-store-service";
import { EditorComponent } from "./editor/editor.component";
import { EventBusService } from "./event-bus/event-bus.service";
import { KpiBarComponent } from "./kpi-bar/kpi-bar.component";
import { ScreenService } from "./screen/screen.service";
import { SidePanelComponent } from "./side-panel/side-panel.component";
import { UploadComponent } from "./upload/upload.component";
import { LoginComponent } from "./auth/login/login.component";
import { RegisterComponent } from "./auth/register/register.component";

const sharedComponents = [
  UploadComponent,
  NavbarComponent,
  MatchHeightDirective,
  CalculatorComponent,
  EditorComponent,
  CheckPipe,
  SidePanelComponent,
  KpiBarComponent,
  LoginComponent,
  RegisterComponent
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MaterialModule,
    FileUploadModule,
    CKEditorModule
  ],
  declarations: sharedComponents,
  exports: sharedComponents,
  providers: [
    CurrencyService,
    DataStoreService,
    EventBusService,
    ScreenService,
    AuthService
  ]
})
export class SharedModule {}
