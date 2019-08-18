//https://msdn.microsoft.com/en-us/office/office365/api/api-catalog

var authContext = null;
var config = null;

$(document).ready(function () {
    // Assign variables
    var params = {
        // Domain of Azure AD tenant
        azureAD: "integrations.at",
        // ClientId of Azure AD application principal
        clientId: "4e60c128-a813-4031-bd99-cf4327cce885",
        // Name of SharePoint tenant
        sharePointTenant: "integrationsonline",
        // Endpoints you like to call
        endPoints: {
            discovery: "https://api.office.com/discovery/",
            graphApiUri: "https://graph.microsoft.com",
            sharePointUri: "https://integrationsonline.sharepoint.com"
        }
    }

    // Create config and get AuthenticationContext
    config = {
        tenant: params.azureAD,
        clientId: params.clientId,
        postLogoutRedirectUri: window.location.origin,
        endpoints: params.endPoints,
        cacheLocation: "localStorage"
    };
    authContext = new AuthenticationContext(config);

    var isCallback = authContext.isCallback(window.location.hash);
    authContext.handleWindowCallback();

    if (isCallback && !authContext.getLoginError()) {
        window.location = authContext._getItem(authContext.CONSTANTS.STORAGE.LOGIN_REQUEST);
    }

    var user = authContext.getCachedUser();
    if (!user) {
        authContext.login();
    }
});

function signOut() {
    authContext.logOut();
    window.location.href = "http://localhost:5000";
}

function useDiscoveryService() {
    if (authContext != null) {
        $("#pResult").show();
        authContext.acquireToken(config.endpoints.discovery,
            function (error, token) {
                if (error || !token) {
                    console.log("ADAL error occurred: " + error);
                    return;
                } else {

                    var uri = "https://api.office.com/discovery/v2.0/me/";

                    $.ajax({
                        type: "GET",
                        url: uri,
                        headers: {
                            "Content-type": "application/json",
                            "Authorization": "Bearer " + token
                        }
                    }).done(function (response) {
                        console.log("succeded calling discovery - result:");
                        console.log(response);
                    }).fail(function (err) {
                        console.log("faild discovery");
                        console.log(err);
                    });
                }
            });
    }
}
