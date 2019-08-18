import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";

export class AuthInterceptor implements HttpInterceptor {
  public intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let cloned = req.clone({
      setHeaders: { Authorization: `Bearer ${environment.token}` }
    });
    console.log(`Interceptor added Token to request ${req.url}`, cloned);
    return next.handle(cloned);
  }
}
