var msalConfig = {
  auth: {
    clientId: "b4ec32c3-c35a-4d74-ac31-15ca02abfc99",
    authority:
      "https://login.microsoftonline.com/d92b247e-90e0-4469-a129-6a32866c0d0a"
  },
  cache: {
    cacheLocation: "localStorage",
    storeAuthStateInCookie: true
  }
};

var graphConfig = {
  graphMeEndpoint: "https://graph.microsoft.com/v1.0/me"
};

// this can be used for login or token request, however in more complex situations
// this can have diverging options
var requestObj = {
  scopes: ["user.read"]
};

var msal = new Msal.UserAgentApplication(msalConfig);
// Register Callbacks for redirect flow
msal.handleRedirectCallback(authRedirectCallBack);

function signIn() {
  msal
    .loginPopup(requestObj)
    .then(function(loginResponse) {
      //Login Success
      showWelcomeMessage();
      acquireTokenPopupAndCallMSGraph();
    })
    .catch(function(error) {
      console.log(error);
    });
}

function acquireTokenPopupAndCallMSGraph() {
  //Always start with acquireTokenSilent to obtain a token in the signed in user from cache
  msal
    .acquireTokenSilent(requestObj)
    .then(function(tokenResponse) {
      callMSGraph(
        graphConfig.graphMeEndpoint,
        tokenResponse.accessToken,
        graphAPICallback
      );
    })
    .catch(function(error) {
      console.log(error);
      // Upon acquireTokenSilent failure (due to consent or interaction or login required ONLY)
      // Call acquireTokenPopup(popup window)
      if (requiresInteraction(error.errorCode)) {
        msal
          .acquireTokenPopup(requestObj)
          .then(function(tokenResponse) {
            callMSGraph(
              graphConfig.graphMeEndpoint,
              tokenResponse.accessToken,
              graphAPICallback
            );
          })
          .catch(function(error) {
            console.log(error);
          });
      }
    });
}

function graphAPICallback(data) {
  document.getElementById("json").innerHTML = JSON.stringify(data, null, 2);
}

function showWelcomeMessage() {
  var divWelcome = document.getElementById("result");
  divWelcome.innerHTML =
    "Welcome " + msal.getAccount().userName + " to Microsoft Graph API";
  //   var loginbutton = document.getElementById("SignIn");
  //   loginbutton.innerHTML = "Sign Out";
  //   loginbutton.setAttribute("onclick", "signOut();");
}

//This function can be removed if you do not need to support IE
function acquireTokenRedirectAndCallMSGraph() {
  //Always start with acquireTokenSilent to obtain a token in the signed in user from cache
  msal
    .acquireTokenSilent(requestObj)
    .then(function(tokenResponse) {
      callMSGraph(
        graphConfig.graphMeEndpoint,
        tokenResponse.accessToken,
        graphAPICallback
      );
    })
    .catch(function(error) {
      console.log(error);
      // Upon acquireTokenSilent failure (due to consent or interaction or login required ONLY)
      // Call acquireTokenRedirect
      if (requiresInteraction(error.errorCode)) {
        msal.acquireTokenRedirect(requestObj);
      }
    });
}

function authRedirectCallBack(error, response) {
  if (error) {
    console.log(error);
  } else {
    if (response.tokenType === "access_token") {
      callMSGraph(
        graphConfig.graphEndpoint,
        response.accessToken,
        graphAPICallback
      );
    } else {
      console.log("token type is:" + response.tokenType);
    }
  }
}

function requiresInteraction(errorCode) {
  if (!errorCode || !errorCode.length) {
    return false;
  }
  return (
    errorCode === "consent_required" ||
    errorCode === "interaction_required" ||
    errorCode === "login_required"
  );
}
