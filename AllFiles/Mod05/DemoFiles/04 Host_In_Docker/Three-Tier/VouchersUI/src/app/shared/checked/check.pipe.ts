import { Pipe, PipeTransform, ElementRef } from '@angular/core';

@Pipe({
  name: 'checked'
})
export class CheckPipe implements PipeTransform {

  transform(checked: boolean): any {
    let result : string = !checked ? '<i class="material-icons">check</i>': ""
    return result;
  }

}
