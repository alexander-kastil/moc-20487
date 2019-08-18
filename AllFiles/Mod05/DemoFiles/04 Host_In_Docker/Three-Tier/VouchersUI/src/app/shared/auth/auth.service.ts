import { Injectable } from "@angular/core";
import { AngularFireAuth } from "@angular/fire/auth";
import { BehaviorSubject, Observable, of } from "rxjs";
import { environment } from "../../../environments/environment";
import { SnackbarService } from "../snackbar/snackbar.service";

@Injectable()
export class AuthService {
  constructor(private fireAuth: AngularFireAuth, private sns: SnackbarService) {
    this.fireAuth.auth.onAuthStateChanged(user => {
      this.setUserToken(user);
    });
  }

  private usrToken: string;
  public Token: BehaviorSubject<string> = new BehaviorSubject<string>("");

  private fbUser: firebase.User = null;
  public User: BehaviorSubject<firebase.User> = new BehaviorSubject(
    this.fbUser
  );

  private setUserToken(user) {
    this.fbUser = user;
    this.User.next(this.fbUser);

    if (user != null) {
      this.fbUser.getIdToken().then(token => {
        environment.token = token;
        this.setToken(token);
      });
    } else {
      this.setToken(null);
    }
  }

  private setToken(token) {
    this.usrToken = token;
    this.Token.next(this.usrToken);
  }

  isAuthenticated(): Observable<boolean> {
    this.User.subscribe(user => {
      let auth: boolean = user == null ? false : true;
      return of(auth);
    });
    return of(false);
  }

  createUser(email: string, password: string): Promise<any> {
    return this.fireAuth.auth.createUserWithEmailAndPassword(email, password);
  }

  logOn(user, password, onSuccess?) {
    return this.fireAuth.auth
      .signInWithEmailAndPassword(user, password)
      .then(onSuccess)
      .catch(err => {
        console.log("Error logging in", err);
        return err;
      });
  }

  logOff() {
    this.fireAuth.auth
      .signOut()
      .then(() => {
        this.setUserToken(null);
        this.sns.displayAlert("Logged out", "Come back and visit soon");
      })
      .catch(err => console.log("Error logging out", err));
  }
}
