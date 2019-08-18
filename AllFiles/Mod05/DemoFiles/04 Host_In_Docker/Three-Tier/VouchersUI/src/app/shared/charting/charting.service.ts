import { Injectable } from '@angular/core';

@Injectable()
export class ChartingService {

  constructor() { }

  getData(): DiagramData[]{
    return [
      {
        "name": "Jan 2018",
        "value": 40632
      },
      {
        "name": "Feb 2018",
        "value": 49737
      },
      {
        "name": "March 2018",
        "value": 36745
      }
    ];
  }

}
