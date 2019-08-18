# Module 10: Scaling Services

# Lab: Load Balancing Azure Web Apps

### Exercise 1: Prepare the Application for Load Balancing

#### Task 1: Add code to print out the server name

1. In the Microsoft Azure portal, create an Azure web app.
2. In Microsoft Visual Studio Code, Open the *[Repository Root]***\AllFiles\Mod10\Labfiles\Lab1\Starter**.
3. Create a new folder and name it **Middleware**. In this folder, create a new file, and name it **MachineNameMiddleware.cs**.
4. In the **MachineNameMiddleware.cs** file, add a **RequestDelegate** instance, and initiate it as the constructor.
5. To add the **X-BlueYonder-Server** header with **MachineName** as the value, add the **Invoke** method.
6. In **IApplicationBuilder**, add an extension method.
7. In the **Configure** method, inside the **Startup** class, add **Exception Handling Middleware**.

#### Task 2: Deploy the application to an Azure web app

1. Publish the **blueyonder**{YourInitials} service.
2. Open the Azure portal, and then click the **blueyonder**{YourInitials} service.
3. In the **Settings** section, click **Scale up (App Service plan)**.
4. On the **Dev/Test** tab, in the **Recommended pricing tiers** box, enter **B1**.

#### Task 3: Configure the web app for multiple instances

1. In the **Settings** section, click **Scale out (App Service plan)**.
2. On the **Configure** tab, increase the **Override condition** scale to **2**.

### Exercise 2: Test the Load Balancing with Instance Affinity

#### Task 1: Browse the website multiple times

1. Open Microsoft Edge, and then browse to the following URL:
   ```url
   https://blueyonder{YourInitials}.azurewebsites.net/api/destinations
   ```
2. Verify that the response has a list of destinations in JSON.

#### Task 2: Verify that you reach the same server each time

1. In Microsoft Edge, open **Developer Tools**, and then click the **Network** tab.
2. Browse to the following URL:
   ```url
   https://blueyonder{YourInitials}.azurewebsites.net/api/destinations
   ```
3. Verify that the response has a list of destinations in JSON.
4. In the **Network** tab, locate the following URL:
   ```url
    https://blueyonder{YourInitials}.azurewebsites.net/api/destinations
   ```
 5. In the **Response Headers** section, locate the **X-BlueYonder-Server**, and then verify that the **Status** is **200**.

### Exercise 3: Test the Load Balancing Without Affinity

#### Task 1: Update the application to not use affinity

1. In the Azure portal, click the **blueyonder**{YourInitials} web app.
2. Click **Application settings**. In the **Settings** section, click **General settings**.
3. In **General settings**, switch **ARR Affinity** to **Off**.

#### Task 2: Retest and verify that you reached more than one instance

1. In Microsoft Edge, refresh the page a couple of times.
2. View the response with all the destinations as the previous exercise.
3. In the **Network** tab,  view **X-BlueYonder-Server**.
   > **Note**: Now there are two server instances in **X-BlueYonder-Server**, because of which you can get two different values for **machine name**.

# Lab: Load Balancing with Azure Traffic Manager

### Exercise 1: Deploy an Azure Web App to Multiple Regions

#### Task 1: Deploy an Azure web app to the West Europe region

1. Open Windows PowerShell as Administrator.
2. From the **[repository root]\AllFiles\Mod10\Labfiles\Lab2\Setup** directory, run the following command:
   ```batch
    .\createAzureServicesWestEurope.ps1
   ```
3. Sign in with your **Subscription ID** and follow the on-screen instructions.

#### Task 2: Deploy an Azure web app to the West US region

1.  Open PowerShell as Administrator.
2.  From the **[repository root]\AllFiles\Mod10\Labfiles\Lab2\Setup** directory, run the following command:
    ```batch
     .\createAzureServicesWestUS.ps1
    ```
3.  Sign in with your **Subscription ID** and follow the on-screen instructions.

### Exercise 2: Create an Azure Traffic Manager Profile

#### Task 1: Create an Azure Traffic Manager profile in the Azure portal

1. Create a Traffic Manager profile with the **Priority** routing method.

#### Task 2: Configure the profile to point to the two Azure web apps

1. In the Traffic Manager profile, create the primary end point.
2. In **Type**, select **Azure endpoint**.
3. In **Target resource type**, select **App Service**.
4. In the **Priority** box, enter **1**.
5. In the Traffic Manager profile, create the primary end point.
6. In **Type**, select **Azure endpoint**.
7. In **Target resource type**, select **App Service**.
8. In the **Priority** box, enter **2**.

#### Task 3: Test the Azure Traffic Manager DNS resolver by checking which instance of the app was reached

1. Browse to the following URL:
    ```url
    http://blueyondermod10lab2TM{YourInitials}.trafficmanager.net/api/destinations
    ```
   >**Note**: All requests are routed to the primary end point that is set to Priority 1.
2. To find our trafficmanager, run the following command:
    ```bash
    nslookup blueyondermod10lab2TM{YourInitials}.trafficmanager.net
    ```
3. Verify that the primary end point is the **blueyondermod10lab2t1{YourInitials}** service.
4. In Azure Traffic Manager, in the **Settings** section, in **myPrimaryEndpoint**, change the **Status** to **Disabled**.
5. Browse to the following URL:
    ```url
    http://blueyondermod10lab2TM{YourInitials}.trafficmanager.net/api/destinations
    ```
   > **Note**: All requests are routed to the secondary end point that is set to Priority 2.
6. To find our trafficmanager, run the following command:
    ```bash
    nslookup blueyondermod10lab2TM{YourInitials}.trafficmanager.net
    ```
7. Verify that the secondary endpoint is the **blueyondermod10lab2t2{YourInitials}** service.
8. Close all open windows.

  ©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
