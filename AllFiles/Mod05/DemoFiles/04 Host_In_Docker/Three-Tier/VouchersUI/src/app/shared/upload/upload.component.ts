import { Component, OnInit } from "@angular/core";
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-upload",
  templateUrl: "./upload.component.html",
  styleUrls: ["./upload.component.scss"]
})
export class UploadComponent implements OnInit {

  public uploadProgress: number;

  constructor(private http: HttpClient) { }

  upload(files) {
    if (files.length === 0)
        return;

    const formData = new FormData();

    for (let file of files)
        formData.append(file.name, file);

    const req = new HttpRequest('POST','http://localhost:5000/api/files/', formData, {
        reportProgress: true,
    });

    this.http.request(req).subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
            this.uploadProgress = Math.round(100 * event.loaded / event.total);
        else if (event instanceof HttpResponse)
            console.log('Files uploaded!');
    });
}  

  ngOnInit() {}
}
