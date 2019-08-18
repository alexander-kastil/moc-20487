# Module 10: Scaling Services

# Lab: Load Balancing Azure Web Apps

1. Wherever a path to a file starts at *[Repository Root]*, replace it with the absolute path to the directory in which the 20487 repository resides. 
 For example, you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, change the path: *[Repository Root]***\AllFiles\20487D\Mod01** to **C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01**.
2. Wherever **{YourInitials}** appears, replace it with your actual initials. For example, the initials for **John Doe** will be **jd**.
3. Before performing the demonstration, you should allow some time for the provisioning of the different Microsoft Azure resources required for the demonstration. You should review the demonstrations before the actual class, identify the resources, and then prepare them beforehand to save classroom time.

### Preparation Steps

1. Open Windows PowerShell as Administrator.
2. In the **User Account Control** modal, click **Yes**.
3. Run the following command: 
   ```
    Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1.
   ``` 
   > **Note**: If prompted install for NuGet provider type **Yes** and then press **Enter**.

   >**Note**: If prompted for **Untrusted repository** press **A** and then press **Enter**. 
4. To change the directory to the **setup** folder, run the following command:
    ```bash
    cd [Repository Root]\AllFiles\Mod10\Labfiles\Lab1\Setup
    ```
5. Run the following command:
    ```batch
     .\createAzureServices.ps1
    ```
    > **Note**: If prompted for Security warning type **R** and then press **Enter**.
6. You will be asked to supply a **Subscription ID**, which you can get by performing the following steps:
   1. Open Microsoft Edge and go to **http://portal.azure.com**. If a box appears, asking for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In**.
   2. On the top bar, in the **search** box, type **Cost**, and then, in **results**, click **Cost Management + Billing(Preview)**. The **Cost Management + Billing** window should open.
   3. Under **Individual Billing**, click **Subscriptions**.
   4. Under **My subscriptions**, you should have at least one subscription. Click the subscription that you want to use.
   5. Copy the value from **Subscription ID**, and then at the PowerShell command line, paste the copied value.
7. In the **Sign in** window that appears, enter your details, and then sign in.
8. In the **Administrator: Windows PowerShell** window, follow the on-screen instructions. Wait for the deployment to complete successfully.
9.  Write down the name of the Microsoft Azure App Service that is created.
10. Close the **Administrator: Windows PowerShell** window.

### Exercise 1: Prepare the Application for Load Balancing

#### Task 1: Add code to print out the server name

1. Open the command prompt.
2. To change the directory to the starter project, run the following command:
    ```bash
    cd [Repository Root]\AllFiles\Mod10\Labfiles\Lab1\Starter
    ```
3. To restore all the dependencies and tools of a project, run the following command:
    ```base
    dotnet restore
    ```
4. To open the project in Microsoft Visual Studio Code, run the following command:
    ```bash
    code .
    ```
5. Right-click on the **Explorer pane** on the left, select **New Folder**, and then name it **Middleware**.
6. Right-click the **Middleware** folder, select **New File**, and then name it **MachineNameMiddleware.cs**.
7. To the class, add the following **using** statements:
    ```cs
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    ```
8. To add namespace, enter the following code:

   ```cs
   namespace BlueYonder.Flights.Service.Middleware
   {

   }
   ```

9. To add a class declaration, enter the following code in namespace brackets:

   ```cs
   public class MachineNameMiddleware
   {

   }
   ```

10. To add a constructor, inside the class brackets, enter the following code:

    ```cs
    private readonly RequestDelegate _next;

    public MachineNameMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    ```

11. To add the **Invoke** method that adds the **X-BlueYonder-Server** header with **MachineName** as value, enter the following code:

    ```cs
    public async Task Invoke(HttpContext httpContext)
    {
        httpContext.Response.Headers.Add("X-BlueYonder-Server", Environment.MachineName);

        await _next(httpContext);
    }
    ```

12. To add an extension method for **IApplicationBuilder**, outside the class brackets but inside the namespace brackets, enter the following code:
    ```cs
    public static class MachineNameMiddlewareExtensions
    {
        public static IApplicationBuilder UseMachineNameMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MachineNameMiddleware>();
        }
    }
    ```
13. on the **Explorer pane** on the left, locate the **Startup** file, and then click the file.
14. To the **Startup** file, add the following **using** statement:
    ```
    using BlueYonder.Flights.Service.Middleware;
    ```
15. To use **Exception Handling Middleware**, locate the **Configure** method, and then enter the following code:
    ```cs
    app.UseMachineNameMiddleware();
    ```

#### Task 2: Deploy the application to an Azure web app

1. Switch to the command prompt.
2. To publish in the service, run the following command:
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
3. Open the Azure portal.
4. To display all the app services, in the left pane, click **App Services**.
5. Click the **blueyonder**{YourInitials} service.
6. In the left pane, in the **Settings** section, click **Scale up (App Service plan)**.
7. Click the **Dev/Test** tab.
8. In the **Recommended pricing tiers** box, select type **B1**, and then click **Apply**.
9. Wait until all the changes are saved.

#### Task 3: Configure the web app for multiple instances

1. In the **Settings** section, click **Scale out (App Service plan)**.
2. Click the **Configure** tab.
3. To increase the instances, locate the **Override condition** scale, and then increase it to **2**.
4. Click **Save**.
5. Wait until all the changes are saved.

### Exercise 2: Test the Load Balancing with Instance Affinity

#### Task 1: Browse the website multiple times

1. Open Microsoft Edge.
2. Browse to the following URL:
    ```url
    https://blueyonder{YourInitials}.azurewebsites.net/api/destinations
    ```
3. Verify that the response has a list of destinations similar to the following:
   ```json
    [
      { "id": 1, "cityName": "Seattle", "airport": "Sea-Tac" },
      { "id": 2, "cityName": "New-york", "airport": "JFK" },
      { "id": 3, "cityName": "Amsterdam", "airport": "Schiphol" },
      { "id": 4, "cityName": "London", "airport": "Heathrow" },
      { "id": 5, "cityName": "Paris", "airport": "Charles De Gaulle" }
    ]
   ```

#### Task 2: Verify you reached the same server each time

1. In Microsoft Edge, on the top bar, click **Settings and more** or press Alt+X.
2. Select **Developer Tools**.
3. In **Developer Tools**, click **Network**.
4. Browse to the following URL:
   ```url
    https://blueyonder{YourInitials}.azurewebsites.net/api/destinations
   ```
5. Verify that the response has a list of destinations similar to the following:
   ```json
    [
      { "id": 1, "cityName": "Seattle", "airport": "Sea-Tac" },
      { "id": 2, "cityName": "New-york", "airport": "JFK" },
      { "id": 3, "cityName": "Amsterdam", "airport": "Schiphol" },
      { "id": 4, "cityName": "London", "airport": "Heathrow" },
      { "id": 5, "cityName": "Paris", "airport": "Charles De Gaulle" }
    ]
   ```
6. In the **Network** tab, locate the the following URL:
   ```url
    https://blueyonder{YourInitials}.azurewebsites.net/api/destinations
   ```
 7. In the **Response Headers** section, locate the **X-BlueYonder-Server**, and then verify that the **Status** is **200**.
   > **Note**: You should always get the same **machine name** value in **X-BlueYonder-Server** header.

### Exercise 3: Test the Load Balancing Without Affinity

#### Task 1: Update the application to not use affinity

1. Switch to the Azure portal.
2. To display all the app services, in the left menu, click **App Services**.
3. Click the newly created web app **blueyonder**{YourInitials}.
4. In the left menu, locate the **Settings** blade.
5. Inside the **Settings** section, click **Application settings**.
6. In **General settings**, locate the **ARR Affinity** switch.
7. Switch the **ARR Affinity** to **Off**.
8. Click **Save**.
9. Wait until all the changes are saved.

#### Task 2: Retest and verify you reached more than one instance

1. Switch to Microsoft Edge.
2. Refresh the page twice.
3. Verify that the response has a list of destinations similar to the following:
   ```json
    [
      { "id": 1, "cityName": "Seattle", "airport": "Sea-Tac" },
      { "id": 2, "cityName": "New-york", "airport": "JFK" },
      { "id": 3, "cityName": "Amsterdam", "airport": "Schiphol" },
      { "id": 4, "cityName": "London", "airport": "Heathrow" },
      { "id": 5, "cityName": "Paris", "airport": "Charles De Gaulle" }
    ]
   ```
4. In the **Network** tab, view the **X-BlueYonder-Server**.
   > **Note**: In **X-BlueYonder-Server**, now there are two server instances, because of which you can get two different values for the machine name.
5. Close all open windows.

# Lab: Load Balancing with Azure Traffic Manager

### Exercise 1: Deploy an Azure Web App to Multiple Regions 

#### Task 1: Deploy an Azure web app to the West Europe region

1. Open Windows PowerShell as Administrator.
2. In the **User Account Control** modal, click **Yes**.
3. Run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**.
   > **Note**: If prompted for if you trust this Repository Type **A** and press **enter**.
4. To  change the directory to the **setup** folder, run the following command:
    ```bash
    cd [repository root]\AllFiles\Mod10\Labfiles\Lab2\Setup
    ```
5. To create a web app in the **West Europe** region, run the following command:
    ```batch
     .\createAzureServicesWestEurope.ps1
    ```
   > **Note**: If prompted for if you trust this Script, Type **R** and press **enter**.
6. In the **Sign in** window that appears, enter your details, and then sign in.
7. You will be asked to supply a **Subscription ID**, which you can get by performing the following steps:
   1. Open Microsoft Edge and go to **http://portal.azure.com**. If a box appears, asking for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In**.
   2. On the top bar, in the **search** box, type **Cost**, and then, in **results**, click **Cost Management + Billing(Preview)**. The **Cost Management + Billing** window should open.
   3. Under **Individual billing**, click **Subscriptions**.
   4. Under **My subscriptions**, you should have at least one subscription. Click the subscription that you want to use.
   5. Copy the value from **Subscription ID**, and then at the PowerShell command line, paste the copied value.
8.  In the **Administrator: Windows PowerShell** window, follow the on-screen instructions. Wait for the deployment to complete successfully.
9.  Write down the name of the Azure App Service that is created.
10. Close **Administrator: Windows PowerShell** window.


#### Task 2: Deploy an Azure Web App to the West US region

1. Open **Powershell**.
2. To change the directory to the **setup** folder, run the following command:
   ```bash
    cd [repository root]\AllFiles\Mod10\Labfiles\Lab2\Setup
   ```
3. To create a web app in the **West Us** region, run the following command:
    ```batch
     .\createAzureServicesWestUS.ps1
    ```
    >**Note**: If prompt for security warning type **R** and then press **Enter**.
4. Sign in with your **Subscription ID** and follow the on-screen instructions.
5. Repeat the task 1 steps 6 to 10.

### Exercise 2: Create an Azure Traffic Manager Profile

#### Task 1: Create an Azure Traffic Manager profile in the Azure portal

1. Open the Azure portal.
2. In the left pane, click **All resources**.
3. Click **+Add**, and then search for **Traffic Manager profile**.
4. In the **Traffic Manager profile** blade, click **Create**.
5. In the **Name** box, type **blueyondermod10lab2TM**{YourInitials}.
6. In **Routing method**, select the **Priority** routing method. 
7. In **Resource group**, select **Use existing**, and then select **Mod10Lab2t1-RG**.
6. Click **Create**, and wait until the traffic manager is created successfully.

#### Task 2: Configure the profile to point to the two Azure web apps

1. In **All resources**, click **blueyondermod10lab2TM**{YourInitials}.
2. In the **Settings** section, click **Endpoints**.
3. To create the primary end point, click **+ Add**.
4. In **Type**, select **Azure endpoint**.
5. In **Name**, type **myPrimaryEndpoint**.
6. In **Target resource type**, select **App Service**.
7. In **Target resource**, select **blueyondermod10lab2t1**{YourInitials}.
8. In **Priority**, enter **1**.
9. Click **Ok**, and wait until the primary end point is created successfully.
10. To create a secondary end point, click **+ Add**.
11. In **Type**, select **Azure endpoint**.
12. In **Name**, type **mySecondaryEndpoint**.
13. In **Target resource type**, select **App Service**.
14. In **Target resource**, select **blueyondermod10lab2t2**{YourInitials}.
15. In **Priority**, enter **2**.
16. Click **Ok**, and wait until the secondary end point is created successfully.

#### Task 3: Test the Azure Traffic Manager DNS resolver by checking which instance of the app was reached

1. Open Microsoft Edge.
2. Browse to the following URL:
    ```url
    http://blueyondermod10lab2TM{YourInitials}.trafficmanager.net/api/destinations
    ```
   > **Note**: All requests are routed to the primary endpoint that is set to Priority 1.
3. Open the command prompt.
4. To find our trafficmanager, run the following command:
    ```bash
    nslookup blueyondermod10lab2TM{YourInitials}.trafficmanager.net
    ```
5. In the result, locate the **Aliases:** property.
6. Verify that the **Aliases:** property is used in the **blueyondermod10lab2t1**{YourInitials} service.
    > **Note**: **blueyondermod10lab2t1** is the primary end point.
7. Switch to the Azure portal.
8. In **All resources**, click **blueyondermod10lab2TM**{YourInitials}.
9. In the **Settings** section, Click **Endpoints**.
10. Click **myPrimaryEndpoint**, and then change the **Status** to **Disabled**.
11. Click **Save**, and wait until all the changes are saved.
12. Browse to the following URL:
   ```url
    http://blueyondermod10lab2TM{YourInitials}.trafficmanager.net/api/destinations
   ```
   > **Note**: All requests are routed to the secondary end point that is set to Priority 2.
13. Switch to the command prompt.
14. To find our traffic manager, run the following command:
   ```bash
    nslookup blueyondermod10lab2TM{YourInitials}.trafficmanager.net
   ```
15. In the result, locate the **Aliases:** property.
16. Verify that the **Aliases:** property is used the **blueyondermod10lab2t2**{YourInitials} service.
   > **Note**: **blueyondermod10lab2t2**{YourInitials} is the secondary end point.
17. Close all open windows.
   
  ©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
