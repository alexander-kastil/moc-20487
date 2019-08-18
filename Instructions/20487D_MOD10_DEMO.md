# Module 10: Scaling services

1. Wherever a path to a file starts at *[Repository Root]*, replace it with the absolute path to the directory in which the 20487 repository resides. 
 For example, you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, change the path: *[Repository Root]***\AllFiles\20487D\Mod01** to **C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01**.
2. Wherever *{YourInitials}* appears, replace it with your actual initials. For example, the initials for **John Doe** will be **jd**.
3. Before performing the demonstration, you should allow some time for the provisioning of the different Microsoft Azure resources required for the demonstration. You should review the demonstrations before the actual class, identify the resources, and then prepare them beforehand to save classroom time.

### Preparation Steps

1. Open Windows PowerShell as Administrator.
2. In the **User Account Control** modal, click **Yes**.
3. Run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**.
   >**Note**: If prompted for you trust this repository Type **A** and then press **Enter**.
4. To change the directory,  run the following command:
   ```bash
    cd  [Repository Root]\AllFiles\Mod10\DemoFiles\Setup1
   ```
5. Run the following command:
   ```batch
    .\createAzureServices.ps1
   ```
6. You will be asked to supply a **Subscription ID**, which you can get by performing the following steps:
   1. Open Microsoft Edge and go to **http://portal.azure.com**. If a box appears, asking for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In**.
   2. On the top bar, in the **search** box, type **Cost**, and then, in **results**, click **Cost Management + Billing(Preview)**. The **Cost Management + Billing** window should open.
   3. Under **Individual billing**, click **Subscriptions**.
   4. Under **My subscriptions**, you should have at least one subscription. Click the subscription that you want to use.
   5. Copy the value from **Subscription ID**, and then at the PowerShell command line, paste the copied value.
7. In the **Administrator: Windows PowerShell** window, follow the on-screen instructions. Wait for the deployment to complete successfully.
8.  Write down the name of the Azure App Service that is created.
9.  Close the **Administrator: PowerShell Window**.

# Lesson 1: Introduction to Scalability

### Demonstration: Scaling Out with Microsoft Azure Web Apps

#### Demonstration Steps

1. Open the command prompt.
2. To change the directory to the starter project, run the following command:
   ```bash
    cd [Repository Root]\AllFiles\Mod10\DemoFiles\Code\BlueYonder.Hotels.Service\RunCPU
   ```
3. To run the project, run the following command:
   ```base
    dotnet run
   ```
4. Wait for five minutes, and then write down the time it took to sign 180,000 reservations.
5. Open Microsoft Edge and go to **http://portal.azure.com**.
6. To display all the Microsoft Azure App Services, on the left menu panel, click**App Services**.
7. Click the **blueyonderMod10Demo1**{YourInitials} service.
8. In the **Settings** section, click **Scale out (App Service plan)**.
9. Click the **Configure** tab.
10. To increase the instances, locate the **Override condition** scale, and increase it to **2**.
11. Click **Save**.
12. Wait until all the changes are saved.
13. Switch to the command prompt.
14. To run the project, run the following command:
   ```base
    dotnet run
   ```
15. When Prompted for Enter service **uri** ,Enter the Azure app service uri copied eralier and then press **Enter**.
16. Wait for five minutes, and then write down the time taken to sign 180,000 reservations.
   > **Note**: Now the time taken is less. We have two instances because of the scaling out.

# Lesson 2: Automatic Scaling

### Demonstration: Configuring Automatic Scaling for Azure Web Apps

#### Demonstration Steps

1. To deploy the Azure web app, repeat the preparation steps.
2. Open Microsoft Edge and go to **http://portal.azure.com**.
3. Click the **blueyonderMod10Demo**{YourInitials} service.
4. In the **Settings** section, click **Application Insights**.
5. Click **Setup Application Insights**, Select **Create new resource**.
6. In **Runtime/Framework**, select **ASP.NET Core**.
7. Click **OK** and then click **OK**, and wait until all the changes are saved.
8. In the **Settings** section, click **Scale out (App Service plan)**.
9. Click **Enable autoscale**.
10. In **Autoscale setting name**, enter a name for the automatic scaling setting.
11. To add a rule, under **Rules**, click **+ Add a rule**. In **Threshold**, enter **80**. In **Duration**, enter **5**, click **Add** and then click **Save** and wait until all the changes are saved.
12. To add another rule, under **Rules**, click **+ Add a rule**. In **Operator**, select **Less than**. In **Threshold**, enter **20**. In **Duration**, enter **5**, under **Action** header in **Operation** select **Decrease count by**, click **Add** and then click **Save** and wait until all the changes are saved.
13. In **Instance limits**, in **Maximum**, enter **2**.
14. Click **Save**, and wait until all the changes are saved.
15. Open the command prompt.
16. To change the directory to the **RunCPU** project, run the following command:
    ```bash
    cd [Repository Root]\AllFiles\Mod10\DemoFiles\Code\BlueYonder.Hotels.Service\RunCPU
    ```
17. To run the project, run the following command:
    ```base
    dotnet run
    ```
18. When Prompted for Enter service **uri** ,Enter the Azure app service uri copied eralier and then press **Enter**.
19. Wait for five minutes, and then switch to the Azure portal.
20. Click the **Run history** tab.
21. In the **scaling** table, under **OPERATION NAME**, verify that the value is **Autoscale scale up completed**.
22. Wait for another five minutes.
23. In the **scaling** table, under **OPERATION NAME**, verify that the value is **Autoscale scale down completed**.

# Lesson 3: Azure Application Gateway and Traffic Manager

### Demonstration: Using an Azure Web App Behind Azure Application Gateway

#### Demonstration Steps

1. Open PowerShell.
2. To change the directory to the **Setup** folder, run the following command:
    ```bash
    cd [Repository Root]\AllFiles\Mod10\DemoFiles\Setup3
    ```
3. Run the following command:
    ```batch
     .\createAzureServices.ps1
    ```
4. You will be asked to supply a subscription ID, which you can get by performing the following steps:
    1. Open a browser and go to **http://portal.azure.com**. If a page appears, asking for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In**.
    2. On the top bar, in the search box, type **Cost**, and then, in results, click **Cost Management + Billing(Preview)**. The **Cost Management + Billing** window should open.
    3. Under **Individual billing**, click **Subscriptions**.
    4. Under **My subscriptions**, you should have at least one subscription. Select the subscription that you want to use.
    5. Copy the subscription ID, and then paste it at the PowerShell prompt. 
5. Open Microsoft Edge and go to **http://portal.azure.com**.
6. Sign in to the Azure portal.
7. On the Azure portal, click **Create a resource**.
8. Under **Azure Marketplace**, select **Networking**, and then select **Application Gateway**.
9. For the new application gateway, enter the following values:
    - **myAppGateway** - for the name of the application gateway.
    - In resource group section click **Create new**, in the **Name** box Type **myResourceGroupAG** an then click **OK**.
10. Accept the default values for the other settings, and then click **OK**.
11. Click **Choose a virtual network**, select **Create new**, and then, for the virtual network, enter the following values:
	- **myVNet** - for the name of the virtual network.
	- **10.0.0.0/16** - for the virtual network address space.
	- **myAGSubnet** - for the subnet name.
	- **10.0.0.0/24** - for the subnet address range.
12. To create the virtual network and subnet, click **OK**.
13. Under **IP address type** select **Public**, under**Public IP address**, select **Create new**, and then type **myAGPublicIPAddress** as the name of the public address.
14. Accept the default values for the other settings and listener configuration, leave the web application firewall disabled and then click **OK**.
15. To create the virtual network, the public IP address, and the application gateway, click **OK**.
16. Wait until the deployment finishes successfully before moving on to the next section.
   > **Note**: It may take up to 30 minutes for the application gateway to be created.
17. In the left menu, click **All resources**, and then, in the **resources** list, click **myVNet**.
18. Click **Subnets**, and then click **+ Subnet**.
19. Name the subnet as **myBackendSubnet**, and then click **OK**.
20. Click on **All resources** on the left pane, click on **myAGPublicIPAddress** and them copy **IP address** value.
21. Switch to PowerShell.
22. To change the directory to the **Setup** folder, run the following command:
    ```bash
    cd [Repository Root]\AllFiles\Mod10\DemoFiles\Setup3
    ```
23. Run the following command:
    ```batch
     .\addAzureServiceToGateway.ps1
    ```
    > **Note**: If prompted for Security warning to allow the script, Type **R** and then press **Enter**.
24. In the **Sign in** window that appears, enter your details, and then **sign in**.
25. To add a new reservation, run the following command:
    ```batch
    Invoke-WebRequest -Uri http://[IP address]/api/reservation/sign -Method Post -ContentType 'application/json' -Body '{"ReservationId":215697,"Room":{"RoomId":689331,"Number":476,"Price":2588.0},"DateCreated":"2018-08-30T08:32:55.3930115Z","CheckIn":"2019-03-09T08:32:55.3909954Z","CheckOut":"2019-04-02T08:32:55.3909954Z","Guests":2,"Traveler":{"TravelerId":102583,"Name":"Donovan","Email":"donovan+877@hotmail.com"}}'
    ```
   > **Note**: Replace **IP address** with the value from point 21.
26. In the response, verify that the **StatusCode** box displays **200**, and the **StatusDescription** box displays **OK**.
27. Close all open windows.

### Demonstration: Using Traffic Manager With an Azure Web App in Multiple Regions

#### Demonstration Steps


1. Open PowerShell as Administrator.
2. In the **User Account Control** modal, click **Yes**.
3. Run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**.
   > **Note**: If prompted for you trust this repository Type **A** and then press **Enter**.
4. To change the directory to the **Setup** folder, run the following command:
    ```bash
    cd [repository root]\AllFiles\Mod10\DemoFiles\Setup4
    ```
5. To create a web app in the **West Europe** region, run the following command:
    ```batch
     .\createAzureServicesWestEurope.ps1
    ```
    > **Note**: If prompted for you trust this Script, Type **R** and then press **Enter**.
6. In the **Sign in** window that appears, enter your details, and then sign in.
7. You need to enter a **Subscription ID**, which you can get by performing the following steps:
   1. Open Microsoft edge and go to **http://portal.azure.com**. If a page appears, asking for your email address, enter your email address, and then click **Continue**. Wait for the sign-in page to appear, enter your email address and password, and then click **Sign In**.
   2. On the top bar, in the **search** box, type **Cost**, and then, in **results**, click **Cost Management + Billing(Preview)**. The **Cost Management + Billing** window should open.
   3. Under **Individual billing**, click **Subscriptions**.
   4. Under **My subscriptions**, you should have at least one subscription. Select the subscription that you want to use.
   5. Copy the value from **Subscription ID**, and then, at the PowerShell command line, paste the copied value. 
8. In the **Administrator: Windows PowerShell** window, follow the on-screen instructions. Wait for the deployment to complete successfully.
9.  Write down the name of the Azure App Service that is created.
10. To change the directory to the **Setup** folder, run the following command:
    ```bash
    cd [repository root]\AllFiles\Mod10\DemoFiles\Setup4
    ```
11. To create a web app in the **West US** region, run the following command:
    ```batch
     .\createAzureServicesWestUS.ps1
    ```
12. Sign in with your **Subscription ID** and follow the on-screen instructions.
13. repeat steps 6 to 9.
14. Close the **PowerShell** window.
15. Open Microsoft Edge and go to **http://portal.azure.com**.
16. In the left pane, click **All resources**.
17. Click **+Add**, and then search for **Traffic Manager profile**.
18. In the **Traffic Manager profile** blade, click the **Create** button.
19. In the **Name** box, type **blueyondermod10demoTM**{YourInitials}.
20. In **Routing method**, select the **Priority** routing method. 
21. In **Resource group**, select **Use existing**, and then select **Mod10demo4Europe-RG**.
22. Click **Create**, and wait until the traffic manager is created successfully.
23. In **All resources**, click **blueyondermod10demoTM**{YourInitials}.
24. In the **Settings** section, click **Endpoints**.
25. To create the primary end point, click **+ Add**.
26. In **Type**, select **Azure endpoint**.
27. In **Name**, type **myPrimaryEndpoint**.
28. In **Target resource type**, select **App Service**.
29. In **Target resource**, select **blueyondermod10demo4Europe**{YourInitials}.
30. In **Priority**, enter **1**.
31. Click **Ok**, and wait until the primary end point is created successfully.
32. To create a secondary end point, click **+ Add**.
33. In **Type**, select **Azure endpoint**.
34. In **Name**, type **mySecondaryEndpoint**.
35. In **Target resource type**, select **App Service**.
36. In **Target resource**, select **blueyondermod10demo4US**{YourInitials}.
37. In **Priority**, enter **2**.
38. Click **Ok**, and wait until the secondary end point is created successfully.

39. Open Microsoft Edge, and then browse to the following URL:
    ```url
    http://blueyondermod10demoTM{YourInitials}.trafficmanager.net/api/reservation
    ```
    > **Note**: All requests are routed to the primary end point, which is set to Priority 1.
40. Open the command prompt.
41. To find our trafficmanager, run the following command:
    ```bash
    nslookup blueyondermod10demoTM{YourInitials}.trafficmanager.net
    ```
42. In the result, locate the **Aliases:** property.
43. Verify that the **Aliases:** property uses the **blueyondermod10demo4Europe**{YourInitials} service.
   > **Note**: **blueyondermod10demo4Europe** is the primary end point.
44. Switch to the **Azure** portal.
45. In **All resources**, click **blueyondermod10demoTM**{YourInitials}.
46. In the **Settings** section, click **Endpoints**.
47. Click **myPrimaryEndpoint**, and then change the status to **Disabled**.
48. Click **Save**, and then wait until all the changes are saved.
49. Browse to the following URL:
    ```url
    http://blueyondermod10demoTM{YourInitials}.trafficmanager.net/api/reservation
    ```
    > **Note**: All requests are routed to the secondary end point, which is set to Priority 2.
50. Switch to the command prompt.
51. To find our trafficmanager, run the following command:
    ```bash
    nslookup blueyondermod10demoTM{YourInitials}.trafficmanager.net
    ```
52. In the result, locate the **Aliases:** property.
53. Verify that the **Aliases:** property uses **blueyondermod10demo4US**{YourInitials} service.
     > **Note**: **blueyondermod10demo4US**{YourInitials} is the secondary end point.
54. Close all open wondows.
	 
©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
