# Module 09: Securing services on-premises and in Microsoft Azure

# Lab: Using ASP.NET Core Identity

### Exercise 1: Add ASP.NET Core Identity middleware

#### Task 1: Add ASP.NET Core Identity NuGet

1. Open command prompt window and then browse to **[Repository Root]\Allfiles\Mod09\LabFiles\Lab1\Starter**.
2. At the command prompt, create a new **ASP.NET Core** project. 
3. To use **Entity Framework Core**, install the **Microsoft.EntityFrameworkCore.Sqlite** NuGet package.

#### Task 2: Create a new DbInitializer with seed for users and groups

1. Open the project in Microsoft Visual Code.
2. Under **Identity** area, create a new folder, and then name it **Data**.
3. In the **Data** folder, create a new **ApplicationDbContext** class derived from **IdentityDbContext**.
4. To add a constructor, paste the following code:
    ```cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) 
            : base(option)
        {
            DbInitializer.Initialize(this);
        }
    ```
5.  In the **Data** folder, create the **DbInitializer** class.
6.  To add the **Initialize** and **Seed** methods, paste the following code:
    ```cs
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                // Code to create initial data
                Seed(context);
            }
        }

        private static void Seed(ApplicationDbContext context)
        {
            // Create list with dummy users 
            List<IdentityUser> userList = new List<IdentityUser>
            {
                new IdentityUser("JonDue") { Email = "jond@outlook.com", PasswordHash = Convert.ToBase64String(Encoding.ASCII.GetBytes("password1234"))},
            };
            context.Users.AddRange(userList);
            context.SaveChanges();
        }
    ```

#### Task 3: Register ASP.NET Core Identity in the startup file

1.  To configure **ASP.NET Core Identity**, in **Startup.cs**, paste the following code:
    ```cs
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(
                Configuration.GetConnectionString("DefaultConnection")));
        services.AddDefaultIdentity<IdentityUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = Configuration["JwtIssuer"],
                ValidAudience = Configuration["JwtIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                ClockSkew = TimeSpan.Zero // remove delay of token when expire
            };
        });
    ```
2. Add the authentication middleware.
3. To the **appsettings.json** file, paste the following configuration:
   ```json
    "ConnectionStrings": {
    "DefaultConnection": "DataSource=app.db"
    },
    "JwtKey": "1234567891011121314151617181920",
    "JwtIssuer": "http://localhost.com",
    "JwtExpireDays": 30,
   ```

### Exercise 2: Add authorization code

#### Task 1: Add a user controller

1. Under **Identity** Area, Add a new folder, and then name it **Models**.
2. In the **Models** folder, create a new **LoginDto** class, and then add the following properties:
    ```cs
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    ```
3. In the **Models** folder, create a new **RegisterDto** class, and then add the following properties:
    ```cs
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    ```
4.  In the  **Controllers** folder, create a new **UserController** class, and then add the following fields to the class:
    ```cs
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration; 
    ```
5. Initialize the fields in the constructor.
    ```cs
        public UserController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
    ```

6. To the **UserController** class, add the **GenerateJwtToken** method with the **email** and **user** parameters.
7. To create a new token, inside the **GenerateJwtToken** method, paste the following code:
    ```cs
        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        DateTime expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

        JwtSecurityToken token = new JwtSecurityToken(
            _configuration["JwtIssuer"],
            _configuration["JwtIssuer"],
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    ```
8. Add a **Register** action, and then create a new user by using **_userManager**.
9. Verify whether the result succeeded and return the token.
10. To verify the username and password and return the token, add a **Login** action, and then call **PasswordSignInAsync**.

#### Task 2: Add authorization attributes to the value controller

1. In the **ValuesController** class, to the  **Get** action, add the **Authorize** attribute.

### Exercise 3: Run a client application to test the server

#### Task 1: Examine the client code to retrieve the access token

1.  From the **[Repository Root]\Allfiles\Mod09\LabFiles\Lab1\Starter\IdentityClient** folder, open the **Program.cs** file.
2.  Review the code to retrieve access token.

#### Task 2: Run client code and inspect user claims in the service

1. Run the **[Repository Root]\Allfiles\Mod09\LabFiles\Lab1\Starter\Identity** project.
2. Run the **[Repository Root]\Allfiles\Mod09\LabFiles\Lab1\Starter\IdentityClient** project.
3. Verify that **[value1, value2]** return back to the command prompt.
4. Close all open windows.

# Lab: Using Azure Active Directory with ASP.NET Core 

### Exercise 1: Authenticate a client application by using AAD B2C and MSAL.js

### Task 1: Configure Azure Active Directory B2C

1. Open the Azure portal.
2. In the left pane, click **+ Create a resource**.
3. In the search box, type **Azure Active Directory B2C**,  from the search results, select **Azure Active Directory B2C**, and then click **Create**.
4. Select **Create a new Azure AD B2C Tenant**.
5. Enter the values for **Organization name** and **Initial domain name**, and then click **Create**.
6. After creating the tenant, click **Directory + subscription filter** and select your tenant.
7. In the top left-hand corner of the Azure portal, select **All services**, search for **Azure AD B2C**, and then from the results, click **Azure AD B2C**.
8. From the blade, select **Applications**, and click **Add**.
9. Enter the name of the application.
10. Under **Web App / Web API**, select **Yes**.
11. Under **Reply URL**, add **https://jwt.ms** and **https://localhost:5001/index.html**, then click **Create**.
12. From the blade, select **User flows (policies)**, click **New user flow**, and then select **Sign-up and sign in**.
13. Enter a policy name for your application to reference.
14. Select the identity providers and select **Email signup**. Optionally, you can also select social identity providers, if already configured. Click **OK**.
15. Under **User attributes and claims**, Choose attributes you want to collect from the consumer during sign-up. For example, select Country/Region, Display Name, and Postal Code. Click **Create**.

#### Task 2: Add Azure B2C Authentication

1. In **[Repository Root]\Allfiles\Mod09\LabFiles\Lab2\Starter**, create a new **ASP.NET Core** project, and then name it **AzureActiveDirectoryB2C**.
2. Open the project in Microsoft Visual Studio Code, and open **Startup.cs** file.
3. To configure the authentication, paste the following code:
    ```cs
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtOptions =>
        {
            jwtOptions.Authority = $"https://{Configuration["AzureAdB2C:TenantName"]}.b2clogin.com/{Configuration["AzureAdB2C:TenantName"]}.onmicrosoft.com/{Configuration["AzureAdB2C:Policy"]}/v2.0/";
            jwtOptions.Audience = Configuration["AzureAdB2C:ClientId"];
            jwtOptions.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = async arg =>
                {
                    var s = $"AuthenticationFailed: {arg.Exception.Message}";
                    arg.Response.ContentLength = s.Length;
                    await arg.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(s), 0, s.Length);
                }
            };
        });
    ```
4.  Add the authentication middleware.
5.  Open the **appsettings.json** file, add the following configuration, and then replace *[TenantName]* and *{YourInitials}* with the corresponding values.
    ```json
        "AzureAdB2C": {
            "TenantName": "[Mod09Lab02-{YourInitials}]",
            "ClientId": "[ClientId]",
            "Policy": "[B2C_1_{YourInitials}]"
        }
    ```
6.  Open the **ValuesController.cs** file and to the **Get()** method, add the **Authorize** attribute.

### Exercise 2: Authenticate a client application by using AAD B2C and MSAL.js

#### Task 1: Add a simple js client

1. Open the **Startup.cs** file.
2. To the **Configure** method, before the **app.UseAuthentication();** call, add the following code:
    ```cs
        app.UseStaticFiles();
    ```
3. In Visual Studio Code, right-click the **wwwroot** folder, select **New File**, and then name it **index.html**.
4. Add the following code:
    ```html
        <html>
            <head>
                <script src="https://secure.aadcdn.microsoftonline-p.com/lib/0.2.3/js/msal.js"></script>
                <script src="index.js"></script>
            </head>
            <body>
                <div>
                    <button onclick="login()">Login</button>
                    <button onclick="request(token)">Request</button>
                    <span id="loginStatus"></span>
                    <span id="responseData"></span>
                </div>
            </body>
        </html>
    ```
5. In Visual Studio Code, right-click the **wwwroot** folder, select **New File**, and then name it **index.js**.
6. Add the following code and replace *[TenantName]* and *{YourInitials}* with the corresponding values:
    ```js
        var applicationConfig = {
            clientID: '[ClientId]',
            authority: "https://[TenantName].b2clogin.com/[TenantName].onmicrosoft.com/B2C_1_{YourInitials}}",
        };

        var clientApplication = new Msal.UserAgentApplication(applicationConfig.clientID, applicationConfig.authority, authCallback, { cacheLocation: 'localStorage', validateAuthority: false });
        function authCallback(errorDesc, token, error, tokenType) {
            if (token) {
            }
            else {
                logMessage(error + ":" + errorDesc);
            }
        }

        var token = "";

        function login() {
            clientApplication.loginPopup().then(function (idToken) {
                //Login Success
                document.getElementById("loginStatus").innerText = "Login Successfully";
                token = idToken;
            }, function (error) {
                document.getElementById("loginStatus").innerText = error;
            });
        }

        function request(token) {
            var headers = new Headers();
            var bearer = "Bearer " + token;
            headers.append("Authorization", bearer);
            var options = {
                method: "GET",
                headers: headers
            };

            fetch("https://localhost:5001/api/values", options)
                .then(function (response) {
                    response.json().then(function (body) {
                        document.getElementById("responseData").innerText = JSON.stringify(body);
                    });
                });
        }
    ```

#### Task 2: Test your service

1. Run the **[Repository Root]\Allfiles\Mod09\LabFiles\Lab2\Starter\AzureActiveDirectoryB2C** project.
2. Open a browser and navigate to the following URL:
    ```
    https://localhost:5001/index.html
    ```
3. Click **Login** and sign-in as an Azure Active DIrectory B2C user.
4. To see the result, click **Request**.
5. Close all open windows.


  ©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
