# Module 6: Deploying and Managing Services

# Lab: Deploying an ASP.NET Core Web Service on Linux

#### Scenario

In this exercise, you will deploy an ASP.NET Core web service on Linux

#### Objectives

After completing this lab, you will be able to:

- Deploy an ASP.NET Core Web API service to a Linux Nginx web server.
- Configure Nginx wbe server as reverse proxy 
- Create a new slot in Azure Web Apps
- Publish new version to the staging slot
- Swap between production and staging slot
- Create an API Management in Azure Portal
- Configure the API Management for your service
- Test the API with cache rules

### Exercise 1: Publishing the ASP.NET Core Web Service for Linux

#### Task 1: Use a Docker container to build a self-contained ASP.NET Core web service

1. Open the command prompt and go to the following directory:
   ```bash
   cd [Repository Root]\Allfiles\Mod06\Labfiles\Exercise1\Starter
   ```
2. To the **BlueYonder.Flights.Service** project, add a new **.dockerignore** file.
3. To the **BlueYonder.Flights.Service** project, add a new **Dockerfile** file.
4. Right-click **BlueYonder.Flights.Service**, select **New File**, and then name the new file as **Dockerfile**.
5. In the **Dockerfile** file, get the image named **dotnet:2.1-sdk**.
6. Restore and build the project.
7. Get the image named **dotnet:2.1-aspnetcore-runtime** and build it.

#### Task 2: Use a Linux Docker container to host the web service

1. Build the container and name it **blueYonder** by using the following command:
   ```bash
   docker build -t blueyonder .
   ```
2. Run the container on port **1234**.
3. In Microsoft Edge, navigate to the following URL:
   ```url
   http://localhost:1234/api/destinations
   ```
4. The **GET** response should return a JSON with all the **Destinations**.

### Exercise 2: Configure Nginx as a reverse proxy

#### Task 1: Use Nginx in a container to reverse-proxy the ASP.NET web service

1. Open the **BlueYonder.Flights.Service** project in Visual Studio Code.
2. Add a new **nginx** folder.
3. In the **nginx** folder, add a new **nginx.conf** file.
4. In the new file, paste the following code:

   ```sh
   user  nginx;
   worker_processes  1;

   error_log  /var/log/nginx/error.log warn;
   pid        /var/run/nginx.pid;

   events {
       worker_connections  1024;
   }

   http {
     server {
       listen 80 default_server;

       location /test {
         proxy_pass http://host.docker.internal:1234;
         rewrite ^/test(.*)$ $1 break;
       }
     }
   }
   ```

**Note**: Check **proxy_pass**. This is the path of the reverse proxy and **test** is what should be added for using **proxy_pass**.

5. To the **nginx** folder, add a new **dockerfile** file.
6. Copy the **nginx** image and change the **conf** file with the following code:

   ```sh
   FROM nginx

   COPY ./nginx.conf /etc/nginx/

   CMD ["nginx", "-g", "daemon off;"]
   ```

7. Switch to the command prompt.
8. To change the directory to **nginx** folder, run the following command:
   ```bash
   cd nginx
   ```
9. Open the command prompt in the **nginx** folder, build the container, and then name it **nginxproxy**.
   ```bash
   docker build -t nginxproxy .
   ```
10. Run **nginx** on port **1235**.
11. In Microsoft Edge, navigate to the following URL:
    ```url
    http://localhost:1235/test/api/destinations
    ```
12. The **GET** response should return a JSON with all the **Destinations** similar to that in **Exercise 1**.
13. Stop the **webapp** process.
14. Stop the **ngninx** process.

#### Task 2: Create a Docker Compose file for bringing up both containers

1. Switch to Visual Studio Code.
2. In the **nginx** folder, add a new **docker-compose.yml** file.
3. To compose all the services that need to be running, paste the following code:
   ```sh
   version: '3'
   services:
     nginx:
       image: nginxproxy
       ports:
        - "1235:80"
     webapp:
       image: "myapp"
   ```
4. Locate **proxy_pass** in **nginx.conf** and change the existing URL to the following URL:
   ```url
   http://webapp:80
   ```
5. Build the container after the change at the command prompt.
6. Run **docker-compose**.
7. In Microsoft Edge, navigate to the following URL:
   ```url
   http://localhost:1235/test/api/destinations
   ```
8. The **GET** response should return a JSON with all the **Destinations** similar to that in **Exercise 1**.
9. Stop all the services in **docker-compose**.

# Lab: Deploying to Staging and Production

### Exercise 1: Deploying the application to production

#### Task 1: Create a standard web app

1. Open the Microsoft Azure Portal.
2. Display all the app services.
   - To select the app service template, in the **App Services** blade, click **Add**.
   - In the **Web** blade, click **Web App**. An overview of the template will be shown. Click **Create**.
3. Create the new **Web App** by entering details in the following fields:
   - In the **App Name** box, enter the following web app name: **blueyonder-flights**-*{YourInitials}*
   - In the **Resource Group** box, select **Create new** and then type **Mod6Resource**.
   - Select **App Service plan/Location** click **Create new**, in the **New App Service Plan** blade enter the following information:
     - In the **App Service plan** box, type **Mod6Lab2ServicePlan**.
     - Click **Pricing tier**.
       - Select the **Production** tab.
       - In **Recommended pricing tiers**, select **S1**.
       - Click **Apply**.
     - Click **OK**.
   - Click **Create** and wait until the app service is created.

#### Task 2: Configure environment variables in the production slot

1. Display all the app services.
2. Click the **blueyonder-flights**-*{YourInitials}* app service.
3. On the left blade menu, in the **SETTINGS** section, click **Application settings**.
   - Locate **Application settings** and click **Add new setting**, add then enter the following information:
     - In the **Enter a name** box, type **BLUEYONDER_TENANT**.
     - In the **Enter a value** box, type **Production**.
   - At top of the blade, click **Save**.

#### Task 3: Deploy an ASP.NET Core application to the production slot

1. To add credentials to the app service, under the **DEPLOYMENT** section, click **Deployment Center**, Select **FTP**, click **Dashboard** and then provide the following information:
   - In **FTP** pane click **User Credentials** , in **username** box type **FTPMod6Lab2**{YourInitials}.
   - In the **Password** and **Confirm password** boxes, type **Password99**.
2. In Visual Studio Code, open the **Starter** project.
3. In the **Middleware** folder, click the **TenantMiddleware.cs** file.
4. To add the **X-Tenant-ID** header, locate the **Invoke** method and paste the following code:
   ```cs
   string tenant = _configuration["BLUEYONDER_TENANT"] ?? "Localhost";
   httpContext.Response.Headers.Add("X-Tenant-ID", tenant);
   ```
5. In the **Properties** folder, add a new **PublishProfiles** folder.
6. In the **PublishProfiles** folder, add the file **Azure.pubxml**, and then add the following code:
   ```xml
   <Project>
       <PropertyGroup>
         <PublishProtocol>Kudu</PublishProtocol>
         <PublishSiteName>blueyonder-flights-{YourInitials}</PublishSiteName>
         <UserName>FTPMod6Lab2{YourInitials}</UserName>
         <Password>Password99</Password>
       </PropertyGroup>
   </Project>
   ```
   > **Note**: This file has the information to deploy to Azure with the **Deployment credentials** that we added in step 1.
7.  Switch to the command prompt and paste the following command:
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
   > **Note**: If there is an error in the publishing process, restart the blueyonder-flights-*{YourInitials}* app services.

#### Task 4: Run the application and verify its output

1. In Microsoft Edge, open **Developer Tools**.
2. In **Developer Tools**, navigate to the **Network** tab.
3. Navigate to the following URL:
   ```url
   https://blueyonder-flights-{YourInitials}.azurewebsites.net/api/destinations
   ```
4. In the **Network** tab, locate the URL and check the following information:
   - In the **Response Headers** section, locate **X-Tenant-ID** and verify that the value is **Production**.

### Exercise 2: Creating a Staging Slot

#### Task 1: Create a staging deployment slot

1. Display all the app services in your Azure Portal.
2. Click the **blueyonder-flights**-*{YourInitials}* app service.
3. On the left blade menu, in the **DEPLOYMENT** section, click **Deployment slots**.
   - Click **Add Slot**:
     - In the **Name** box, type **Staging**.
     - In **Configuration Source**, select **blueyonder-flights**-*{YourInitials}*.
     - Click **OK**.

#### Task 2: Configure environment variables in the staging slot

1. In the **Deployment slots** blade, click **blueyonder-flights**-*{YourInitials}*-**staging**.
2. In the **SETTINGS** section, click **Application settings**.
3. In **Application settings**, in **BLUEYONDER_TENANT**, change the value to **Staging**, and then click **Save**.

#### Task 3: Deploy a newer version to the staging slot

1. Switch to Visual Studio Code.
2. In the **Controllers** folder, double-click **DestinationsController.cs**.
3. Add more destinations.
   ```cs
   _destinations.Add(new Destination { Id = 6, CityName = "Milan", Airport = "Malpensa" });
   _destinations.Add(new Destination { Id = 7, CityName = "Rome", Airport = "Leonardo da Vinci-Fiumicino" });
   ```
4. In the **Properties** folder, in the **PublishProfiles** folder, add a new **Staging.pubxml** file.
5. In the new file, paste the following code:
   ```xml
   <Project>
       <PropertyGroup>
         <PublishProtocol>Kudu</PublishProtocol>
         <PublishSiteName>blueyonder-flights-{YourInitials}-staging</PublishSiteName>
         <UserName>FTPMod6Lab2{YourInitials}</UserName>
         <Password>Password99</Password>
       </PropertyGroup>
   </Project>
   ```
6. To publish in the staging slot, at the command prompt, run the following code:
   ```bash
   dotnet publish /p:PublishProfile=Staging /p:Configuration=Release
   ```

#### Task 4: Run the application from staging and verify its output

1. In Microsoft Edge, open **Developer Tools**.
2. In the **Developer Tools**, navigate to the **Network** tab.
3. Navigate to the following URL:
   ```url
   https://blueyonder-flights-{YourInitials}-staging.azurewebsites.net/api/destinations
   ```
4. View the response with all the destinations, including Milan and Rome.
5. In the **Network** tab, locate the URL and check the following information:
   - In the **Response Headers** section, locate **X-Tenant-ID** and verify that the value is **Staging**.


### Exercise 3: Swapping the Environments

#### Task 1: Perform a swap of the staging and production environments

1. Switch to Azure Portal.
2. Display all the app services.
3. Click the **blueyonder-flights**-*{YourInitials}* app service.
4. In the **Overview** blade, on the top bar, click **Swap**.
5. In the **Swap** blade, add the following steps:
    - In **Swap type**, select **Swap**.
    - In **Source**, select **production**.
    - In **Destination**, select **Staging**.
    - Click **OK**. 

#### Task 2: Run the production application and verify its output

1. Switch to Microsoft Edge with the production URL (Exercise 1, Task 4), and to refresh the page, press F5.
2. View the response with all the destinations including Milan and Rome.
3.  In the **Network** tab, locate the URL and check the following information:
    - In the **Response Headers** section, locate **X-Tenant-ID** and verify that the value is **Staging**.

#### Task 3: Undo the swap

1. Switch to Azure Portal.
2. Display all the app services.
3. Click the **blueyonder-flights**-*{YourInitials}* app service.
4. In the **Overview** blade, on the top bar, click **Swap**.
5. In the **Swap** blade, add the following steps:
    - In **Swap type**, select **Swap**.
    - In **Source**, select **Staging**.
    - In **Destination**, select **production**.
    - Click **OK**. 
6. Switch to Microsoft Edge with the production URL (Exercise 3, Task 2) and to refresh the page, press F5.
7. View the response with all the destinations without Milan and Rome.
8.  In the **Network** tab, locate the URL and check the following information:
    - In the**Response Headers** section, locate **X-Tenant-ID** and verify that the value is **Production**.
   
#### Task 4: Configure the application settings as sticky to the slot

1. Switch to Azure Portal.
2. Display all the app services.
3. Click the **blueyonder-flights**-*{YourInitials}* app service.
4. In the left blade menu, in the **SETTINGS** section, click **Application settings**.
5. In **Application settings**, locate **BLUEYONDER_TENANT** and check **SLOT SETTING**.
6. At the top of the blade, click **Save**.

#### Task 5: Redo the swap and re-test the output of the production slot

1. Switch to Azure Portal.
2. Display all the app services.
3. Click the **blueyonder-flights**-*{YourInitials}* app service.
4. In the **Overview** blade, on the top bar, click **Swap**.
5. In the **Swap** blade, add the following steps:
    - In **Swap type**, select **Swap**.
    - In **Source**, select **production**.
    - In **Destination**, select **Staging**.
    - Click **OK**. 
6. Switch to Microsoft Edge with the production URL (Exercise 3, Task 2).
7. To refresh the page, press **F5**.
8. View the response with all the destinations including Milan and Rome.
9.  In the **Network** tab, locate the URL and check the following information:
    - In the **Response Headers** section, locate **X-Tenant-ID** and verify that the value is **Production**.
10. Close all open windows.


# Lab: Publishing a Web API with Azure API Management

### Preparation Steps

1. In PowerShell as Administrator, run **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**.
   >**Note**: If prompted for to install NuGet provider type **Y** and then press **Enter**.

   >**Note**: If prompted for trust this repository type **A** and then press **Enter**.
2. Navigate to *[repository root]***\Mod06\Labfiles\Exercise3\Setup** and run the following command:
    ```batch
     .\createAzureServices.ps1
    ```
3. You will be asked to supply a **Subscription ID**, which you can get from the **http://portal.azure.com** page, under **My subscriptions**.  
4. Enter your details and then sign in to your subscription.
5. Follow the on-screen instructions. Wait for the deployment to complete successfully.

### Exercise 1: Creating an Azure API Management Instance

#### Task 1: Create an Azure API Management instance in the Azure Portal

1. In Microsoft Edge, navigate to **https://portal.azure.com**.
2. Sign in with your Azure subscription.
3. Click **Create a resource**.
4. Create **API management**.
5. In the **API Management service** page, enter:
   - Name: **blueyonder-api**{*YourInitials*}
   - Resource Group:
        - Select **Use existing**.
        - Select **Mod6Lab3-RG**.
   - Organization name: Enter the name of your organization
6. Go to **All resources** and click the new **API Management** section that was created.

#### Task 2: Create an Azure API product and API manually

1. To add new products, in **API MANAGEMENT**, on the top menu bar, click **Add**, and then enter:
    - In **Display name**, type **Blueyonder**.
    - In **Description**, type **Blueyounder API**.
    - Select **Requires subscription**.
2. In the **API MANAGEMENT** section, under **APIs**, enter the following details in **Blank API**:
    - In **Display name**, type **BlueYonder**.
    - In **Name**, type **blueyonder**.
    - In **Web service URL**, type **https://blueyonder{YourInitials}.azurewebsites.net/api**.
    - In **Api URL suffix**, type **api**.
    - In **Products**, select **blueyonder**.
3. To create the **GET Destinations** operation, click **+ Add operation** and enter the following details:
    - In **Display name**, type **Destinations**.
    - In **Name**, type **destinations**.
    - In **URL**, select **GET** and then type **/destinations**.
4. To create the **GET Destinations By ID** operation, click **+ Add operation**, and then enter the following details:
    - In **Display name**, type **Destinations By ID**.
    - In **Name**, type **destinations-by-id**.
    - In **URL**, select **GET** and then type **/destinations/{id}**.
    - Select the **Template** tab, and then enter the following details:
        - In **Name**, type **id**.

### Exercise 2: Testing and Managing the API

#### Task 1: Test the API from the Azure API Management portal

1. In the **BlueYonder** API, click **Destinations**.
2. In the **Destinations** operation blade, click the **Test** tab.
3. To test the **GET Destinations** API, click **Send**.
4. Verify that the response is **200** with the destinations list.
5. In the **BlueYonder** API, click **Destinations By ID**.
6. In the **Destinations By ID** operation blade, click the **Test** tab.
7. In the **Template parameters** section, in the **id** parameter, under **VALUE**, enter **1**.  
8. To test the **GET Destinations by ID** API, click **Send**.
9. Verify that the response is **200** with the Seattle destination.

#### Task 2: Configure a caching policy for flight availability and weather information APIs

1. In the **BlueYonder** API, click **Destinations**.
2. Click the **Design** tab.
3. In the **Inbound processing** window, click the **+ Add policy**.
4. Select the **Cache response**, and then enter the following details:
    - Under **Cache responses**, select **On**.
    - In **Duration**, type **60**.
    - Click **Save**
5. Click the **Test** tab and then click **Destinations**.
6. Click **Send** and then click the **Trace** tab.
7. Verify that the **Backend** section exists and that the request was handled.
8. Click **Send** and then click the **Trace** tab.
9. Verify that the **Backend** section does not exist and that the request was cached.

#### Task 3: Configure a request rate limit (throttling) for the flight availability API

1. In the **BlueYonder** API, click **Destinations By ID**, and then click the **Design** tab.
2. In the **Inbound processing** window, click the **ellipses(...)** and select **Code editor**.
3. Position the cursor inside the **\<inbound\>** element.
4. In the right window, under **Access restriction policies**, click **+ Limit call rate per key**.
5. In the **\<inbound\>** element, modify your **rate-limit-by-key** code to the following code:
    ```xml
     <rate-limit-by-key calls="2" renewal-period="60" counter-key="@(context.Subscription.Id)" />
    ```

#### Task 4: Test the rate limit

1. In the **BlueYonder** API, click the **Destinations By ID**.
2. Click the **Test** tab and then click **Destinations By ID**.
3. In the **Template parameters** section, in the **id** parameter, under **VALUE**, enter **1**.  
4. Press **Send** two times in a row.
5. After sending the request two times, you get the **429 Too many requests** response.
6. Wait for 60 seconds and then press **Send** again. This time you should get the **200 OK** response.
7. Close all windows.


©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
