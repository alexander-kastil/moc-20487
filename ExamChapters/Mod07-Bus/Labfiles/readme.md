# Module 7: Microsoft Azure Service Bus

Wherever you see a path to file starting at [repository root], replace it with the absolute path to the directory in which the 20487 repository resides. e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [repository root]\AllFiles\20487C\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487C\Mod01

# Lab: Microsoft Azure Service Bus

## Setup

1. Click **Start** and in the **Search box** type **Visual Studio**. In the list of search results, right-click **Visual Studio 2017** and then select **Run as administrator**. Click **Yes** in the **User Account Control** dialogue box.
2. Run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**. To enable the relevant version of **PowerShellGet**, in the **Administrator: Windows PowerShell** window, press **Y** when you are asked whether to install and import the Buget provider now.To install the modules from **PSGallery**, in the **Administrator: Windows PowerShell** window, press **Y** when you are asked whether you are sure you want to install.
3. Go to **[repository root]\AllFiles\20487C\Mod07\LabFiles\Setup** (For example, if the repository root is in **C:\Users\JohnDoe\Azure**, change the path to **C:\Users\JohnDoe\Azure\AllFiles\20487C\Mod07\LabFiles\Setup**).
4. Run **.\createAzureServices.ps1**. 
5. You are prompted to supply a subscription ID, which you can get by performing the following steps:  
    a. Open a browser, and then go to **http://portal.azure.com**. If you are prompted to sign in, sign in by using your Azure account credentials.  
    b. In the search text box on the top bar, type **Cost** and then in results click **Cost Management + Billing(Preview)**. The **Cost Management + Billing** window should open  
    c. Under **BILLING ACCOUNT**, click **Subscriptions**.  
    d. Under **My subscriptions**, you should have at least one subscription. Click the subscription.  
    e. Copy the value from **Subscription ID**, and then paste it at the PowerShell prompt.  
6. In the **Sign in** window that appears, enter your details, and then sign in.
7. In the **Administrator: Windows PowerShell** window, enter your initials when prompted.
8. When the deployment ends, copy the output connection strings to a text file for later use.

### Exercise 1: Using a Service Bus Relay for the WCF Booking Service

#### Task 1: Create the Service Bus Relay by using the Azure Portal
1. Open a browser (For example, Microsoft Edge).
2. Go to the Microsoft Azure portal (**http://portal.azure.com**).
3. If a page appears requesting for your email address, enter your email address, and then click **Next**. enter your password, and then click **Sign In**.

   >**Note:** If during the sign-in process, a page appears asking you to choose from a list of previously used accounts, select the account you previously used, and then continue to type your credentials.

4. If the **Windows Azure Tour** dialog box appears, close it.
5. In the navigation pane on the left side, click **+ Create a resource**.
6. In the **Search the Marketplace** field, type **Relay**, and then press Enter.
7. After the search results appear, click **Relay** and then click **Create**.
8. In the **Create Namespace** dialog box provide the following information:

    - Name: **BlueYonderLab07Relay-**_YourInitials_ (Replace _YourInitials_ with your initials).
    - Resource Group: Select **Use existing**, and then select **BlueYonder.Lab.07** from the drop-down list.
    - Region: Select the region closest to your location.

9.  To create the namespace, click the **Create** button. Wait until the namespace is created. 
10. In the **All Resources** pane, locate the newly created namespace, and then click it.
11. Click **Shared access policies**.
12. Click **RootManageSharedAccessKey**.
13. Click the **Copy** icon to the right side of the **Primary Key** text box.

#### Task 2: Add a new WCF Endpoint with a relay binding

1. Open **Visual Studio 2017**.
2. On the **File** menu, point to **Open**, and then click **Project/Solution**.
3. In the **File name** text box, type **[repository root]\AllFiles\20487C\Mod07\LabFiles\begin\BlueYonder.Server\BlueYonder.Server.sln**, and then click **Open**.
4. In **Solution Explorer**, right click the **BlueYonder.Server.Booking.WebHost** project, and then click **Manage NuGet packages**.
5. Select the **Browse** tab, and then in the search text box, type **windowsazure.servicebus**.
6. Wait until the results load, select **WindowsAzure.ServiceBus**, and then click **Install**.
7. In the **Preview Changes** dialogue box, Click **OK**.
8. In the **License Acceptance**  dialogue box, Click **I Accept**.
9. In **Solution Explorer**, expand the **BlueYonder.Server.Booking.WebHost** project, and then double-click **Web.config**.
10. In the **&lt;system.serviceModel&gt;** section group, locate the **&lt;services&gt;** section, and then inside it locate the **&lt;endpoint&gt;** element named **BookingTcp**.
11. In the **&lt;endpoint&gt;** element, change the value of the **binding** attribute from **netTcpBinding** to **netTcpRelayBinding**.
12. Add the **address** attribute to the **&lt;endpoint&gt;** element with the value **sb://BlueYonderLab07Relay-**_YourInitials_**.servicebus.windows.net/booking** (Replace _YourInitials_ with your initials).
13. In the **&lt;system.serviceModel&gt;** section group, locate the **&lt;behaviors&gt;** section.
14. In the **&lt;behaviors&gt;** section, add the following configuration:

    ```cs
        <endpointBehaviors>
           <behavior name="sbTokenProvider">
             <transportClientEndpointBehavior>
                   <tokenProvider>
                     <sharedAccessSignature keyName="RootManageSharedAccessKey" key="{AccessKey}" />
                   </tokenProvider>
             </transportClientEndpointBehavior>
           </behavior>
        </endpointBehaviors>
    ```
15. Substitute the **{AccessKey}** placeholder with the Service Bus namespace access key that you copied in the previous task.

    >**Note:** Visual Studio Intellisense uses built-in schemas to perform validations. Therefore, it does not recognize the **transportClientEndpointBehavior** behavior extension, and displays a warning. Disregard this warning.

16. In the **&lt;system.serviceModel&gt;** section group, locate the **&lt;services&gt;** section, and then inside it, locate the **&lt;endpoint&gt;** element.
17. Add a **behaviorConfiguration** attribute to the element, and then set its value to **sbTokenProvider**.
18. Locate the **&lt;system.webServer&gt;** section group, and then add the following configuration to it:

    ```cs
        <applicationInitialization>
          <add  initializationPage="/booking.svc"/>
        </applicationInitialization>
    ```
19. To save the changes, press Ctrl+S.

    >**Note:** Application initialization automatically sends requests to specified addresses after the web application loads. Sending the request to the service makes the service host load and initiate the Service Bus connection.
20. In **Solution Explorer**, right-click the **BlueYonder.Server.Booking.WebHost** project, and then click **Build**.

#### Task 3: Configure the ASP.NET Web API back-end service to use the new relay endpoint

1. Click **Start** and in the Search box type **Visual Studio**. In the list of search results, right-click **Visual Studio 2017** and then select **Run as administrator**. Click **Yes** in the **User Account Control** dialogue box.
2. On the **File** menu, point to **Open**, and then click **Project/Solution**.
3. In the **File name** text box, type **[repository root]\AllFiles\20487C\Mod07\LabFiles\begin\BlueYonder.Server\BlueYonder.Companion.sln**, and then click **Open**.
4. In **Solution Explorer**, right-click **BlueYonder.Companion.Host** project, and then click **Manage NuGet packages**.
5. Select the **Browse** tab, and then in the search text box, type **windowsazure.servicebus**.
6. Wait until the results are loaded select **WindowsAzure.ServiceBus**, and then click **Install**.
7. In the **Preview Changes** dialogue box, Click **OK**.
8. In the **License Acceptance**  dialogue box, Click **I Accept**.
9. In **Solution Explorer**, expand the **BlueYonder.Companion.Host** project, and then double-click **Web.config**.
10. In the **&lt;system.serviceModel&gt;** section group, locate the **&lt;client&gt;** section, and then inside it, locate the **&lt;endpoint&gt;** element.
11. Change the value of the **binding** attribute from **netTcpBinding** to **netTcpRelayBinding**. If it's laready done please ignore.
12. Substitute the value of the **address** attribute with the following value: **sb://BlueYonderLab07Relay-**_YourInitials_**.servicebus.windows.net/booking** (Replace _YourInitials_ with your initials).
13. In the **&lt;system.serviceModel&gt;** section group, locate the **&lt;behaviors&gt;** section group and locate the **&lt;transportClientEndpointBehavior&gt;** section.
14. Substitute the **{ServiceBusRelaySASKey}** placeholder by pasting the Service Bus namespace access key that you copied in the first task.
15. To save the file, press Ctrl+S.
16. To close the file, press Ctrl+F4.

    >**Note:** Visual Studio Intellisense uses built-in schemas to perform validations. Therefore, it does not recognize **transportClientEndpointBehavior** behavior extension, and displays a warning. Disregard this warning.

#### Task 4: Test the WCF service

1. Switch to the Visual Studio 2017 instance that has the **BlueYonder.Server** solution open.
2. Right-Click on the **BlueYonder.Server.Booking.WebHost** project and click **Debug-> Start new instance**.
3. Locate the address bar and add **/booking.svc** to the end of your address. Then wait until it will finish to load.
4. if Azure Portal was closed, perform the below stpes to re-open.
    a. Go to the Azure portal at **http://portal.azure.com**.
    b. If during the sign-in process, a page appears asking you to choose from a list of previously used accounts, select the account you previously used, and then enter your credentials. Enter your email address and password, and then click **Sign In.**
6. Click on **SQL databases** in the left menu. Then click on **BlueYonder.Companion.Lab07** database.
7. Click on **Set server firewall** in the top menu.
8. Click on **Add client IP** in the top menu and then click **Save**, and in **Success window** click **OK**.
9. In the **All Resources** pane, click the **BlueYonderLab07Relay-**_YourInitials_ (Replace _YourInitials_ with your initials) relay.
10. Verify that the **booking** relay is listed in the **Overview** tab.
11. In the **All Resources** pane, click the **blueyonder07Hub**.
12. Click on **Access Policies** under **MANAGE**, and copy **DefaultListenSharedAccessSignature** for later use.
13. Go back to the Visual Studio 2017 instance that has the **BlueYonder.Companion** solution open.
14. In  **Solution Explorer**, right-click **Sloution 'BlueYonder.Companion'** and select **Build Solution**.
15. On the **View** menu, click **Task List**.
16. In **Task List**, click **Entire Solution** in the drop-down list at the top.
17. Double-click the **// TODO: Lab 07 Exercise 1: Task 4.3: Bring back the call to the backend WCF service** comment.
18. Uncomment the call to the **CreateReservationOnBackendSystem** method. Make sure the return value of the method is stored in the **confirmationCode** variable.
19. To save the file, press Ctrl+S.
20. In the **Pick a Publish** target dialogue box select **App Service** and make sure **Create New** is selected, and then click **Publish**.
21. Select **Microsoft Azure App Service** and make sure **Create New** is selected, and then click **Publish**.
22. In **Create App Service**, type the following values:
     - App Name: **BlueYonderLab07Companion**_YourInitials_
     - Resource Group: **BlueYonder.Lab.07(eastus)**
23. Click **Create** and then if the App is not published automatically, then click **Publish**.
24. Switch to the Visual Studio 2017 instance that has the **BlueYonder.Server** solution open.
25. In **Solution Explorer**, expand the **BlueYonder.BookingService.Implementation** project, and then double-click **BookingService.cs**.
26. In the **CreateReservation** method, right-click the line of code that starts with **if(request.DepartureFlight**, point to **Breakpoint**, and then click **Insert Breakpoint**.
27. Open another instance of **Visual Studio 2017**.
28. On the **File** menu, point to **Open**, and then click **Project/Solution**.
29. In the **File name** text box, type 
**[repository root]\Allfiles\20487C\Mod07\Labfiles\begin\BlueYonder.Companion.Client\BlueYonder.Companion.Client.sln**, and then click **Open**.

30. If the Windows **Settings** window is open in the **For Developers** section, select **Developer mode**. In the dialog box that appears, click **Yes**.
31. In **Solution Explorer**, expand the **BlueYonder.Companion.Shared** project, and then double-click **Addresses.cs**.
32. In the **Addresses** class, locate the **BaseUri** property, and then replace the **[Web Api web app name]** string with the **BlueYonderLab07Companion**_YourInitials_.
33. In **Solution Explorer**, expand the **BlueYonder.Companion.Client** project, expand **Helpers** folder, and then double-click **WnsManager.cs**.
34. In the **WnsManager** class, locate the **Register** method, and then replace the **[NotificationHub connection string]** with the value pervious task (Task 4, Point 12).
35. To save the changes, press **Ctrl+S**.
36. Right-click the solution and then click **Properties**, In the **Properties box**, click **Configuration** and then enable **Debug and Build** check boxes for **BlueYonder.Companion** To start the client app without debugging, press **Ctrl+F5**.
37. If you are prompted to allow the app to run in the background, click **Yes**.
38. To display the app bar, right click or swipe from the bottom of the screen.
39. Click **Search**, and then in the **Search** text box, type **Sea**.
40. If you are prompted to allow the app to share your location, click **Yes**.
41. Wait for the app to show a list of flights from Seattle to Rome.
42. Select flight **Seatle to Rome**, and click on **Purchase this trip**.
43. In the **First Name** text box, enter your first name.
44. In the **Last Name** text box, enter your last name.
45. In the **Passport** text box, type **Aa1234567**.
46. In the **Mobile Phone** text box, type **555-5555555**.
47. In the **Home Address** text box, type **423 Main St.**.
48. In the **Email Address** text box, enter your email address.
49. Click **Purchase**.
50. Switch to the Visual Studio 2017 instance that has the **BlueYonder.Server** solution open.  
    Note that the code execution breaks, and the line in the breakpoint is highlighted in yellow.
51. To resume execution, press F5.
52. Go back to the client app.
53. To close the confirmation message, click **Close**, and then close the client app.
54. Return to the Visual Studio 2017 instance where the **BlueYonder.Server** solution is open, and then press Shift+F5 to stop debugging the WCF application.

    >**Results**: After completing this exercise, you should have successfully run the client app to book a flight, and have the ASP.NET Web API services, running in the Azure Web Role, communicate with the on-premises WCF services by using Azure Service Bus Relays.

### Exercise 2: Publishing Flight Updates to Clients by using Azure Service Bus Queues

#### Task 1: Send flight update messages to the Service Bus Queue

##### Create a Service Bus for using a Service Bus Queue

1. Open a browser (For example, Microsoft Edge).
2. Go to the Microsoft Azure portal at **http://portal.azure.com**.
3. If a page appears, asking for your email address, enter your email address, and then click **Continue**. Wait for the **Sign In**  page to appear, enter your email address and password, and then click **Sign In.**

   >**Note:** If during the sign-in process, a page appears asking you to choose from a list of previously used accounts, select the account you previously used, and then continue to enter your credentials.

4. In the navigation pane, click **+ Create a resource**, click **Enterprise Integration**, and then click **Service Bus**.
5. In the **Create Namespace** dialog box, type the following values:
    - Name: **BlueYonderLab07ServiceBus-**_YourInitials_
    - Pricing Tier: **Standard**
    - Resource Group: Make sure **Use Existing** is selected and then select **BlueYonder.Lab.07** from the drop-down list.
    - Location: Select the region to your location.
6. Click **Create**.
7. After the Service Bus is created, locate it in the **All Resources** pane, and then click it.
8. Under **SETTINGS**, click **Shared access policies**, and then click **RootManageSharedAccessKey**.
9. Click the **Copy** icon next to **Primary Connection String**.

##### Send updates by using a Service Bus Queue

10. Return to the Visual Studio 2017 instance where the **BlueYonder.Companion** solution is open.
11. In **Solution Explorer**, expand the **BlueYonder.Companion.Host** project, and click **web.config**.
12. Under **AppSettings**, add a new entry.
    ```xml
        <add key="ServiceBusConnectionString" value="[Connection String of the service bus you just created and copied]" />
    ```
13. To save the changes, press Ctrl+S.
14. In **Solution Explorer**, expand the **BlueYonder.Companion.Controllers** project node, and then double-click **ServiceBusQueueHelper.cs**.
15. Replace the **return null** statement in the **ConnectToQueue** method with the following code:

    ```cs
        string connectionString = ConfigurationManager.AppSettings["ServiceBusConnectionString"];
        var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
    ```
16. Add the following code to the end of the method to create the queue if it does not exist:

    ```cs
        if (!namespaceManager.QueueExists(QueueName))
        {
           namespaceManager.CreateQueue(QueueName);
        }
    ```
17. Add the following code to the end of the method to return a **QueueClient** object:

    ```cs
        return QueueClient.CreateFromConnectionString(connectionString, QueueName);
    ```
18. To save the changes, press Ctrl+S .
19. In **Solution Explorer**, under the **BlueYonder.Companion.Controllers** project, double-click **FlightsController.cs**.
20. Add the following static field to the class:

    ```cs
        private static QueueClient Client;
    ```
21. Create a static constructor in the class by adding the following code to it:

    ```cs
        static FlightsController()
        {
            Client = ServiceBusQueueHelper.ConnectToQueue();
        }
    ```
22. Locate the **Put** method.
23. Place the following code after the **// TODO: Lab07, Exercise 2, Task 1.6 : Send a flight update message to the queue** comment.

    ```cs
        updatedScheduleDto.Flight.FlightId = id;
        var msg = new BrokeredMessage(updatedScheduleDto);
        msg.ContentType = "UpdatedSchedule";
        Client.Send(msg);
    ```
24. To save the changes, press Ctrl+S.

#### Task 2: Configure BlueYonder's notification hub to use WNS

1. Open the Microsoft Azure portal (**http://portal.azure.com**).
2. Under the **All Resources** pane, click **BlueYonder07Hub**. 
3. Under **Notification Settings**, click **Windows (WNS)**.
5. In the **Package SID** text box, type **ms-app://s-1-15-2-4282730232-1326842515-3906499318-170594168-2218672132-3110390429-3067443127**.
6. In the **Security Key** text box, type **3dl+J0UEuvzdc8ihZilFH+L3DIdCXLKT**.
7. Click **Save**.

#### Task 3: Create an Azure function that receives messages from a Service Bus Queue and sends notifications to clients

1. Right click the **BlueYonder.Companion** solution.
2. Point to **Add**, and then click **Existing Project**.
3. Go to **[repository root]\AllFiles\20487C\Mod07\LabFiles\begin\BlueYonder.Server\BlueYonder.Companion.WNS** 
4. Select **BlueYonder.Companion.WNS.csproj**, and then click **Open**.
5. Right-click the **BlueYonder.Companion** solution.
6. Point to **Add**, and then click **New Project**.
7. Expand **Installed**, and then expand **Visual C#**.
8. Click **Cloud**.
9. In the list of project types, select **Azure Functions**.
10. Name the project **BlueYonder.Companion.Functions**.
11. Click **OK** and in **New Template - BlueYonder.Companion.Functions** window, select **Azure Functions v1(.NET Framework)** and click **Empty** and click **OK**.
12. In **Solution Explorer**,Right-click project **BlueYonder.Companion.Functions**, select **Properties**.
13. In Properties windows locate **Target framework**, select **.NET Framework 4.6.2**, and then press **Ctrl+S** to **Save**.
14.  In **Solution Explorer**, right click the **BlueYonder.Companion.Functions** project, and then click **Manage NuGet packages**.
15. Select the **Browse** tab, and then in the search text box, type **EntityFramework**.
16. Wait until the results load, select **EntityFramework**, and then click **Install**.
17. In the **Preview Changes** dialogue box, Click **OK**.
18. In the **License Acceptance**  dialogue box, Click **I Accept**.
19. Right-click solution **BlueYonder.Companion** and point to **Add**, then click **Reference**. Select **Projects** and then add the below references and then click **OK**:
    - **BlueYonder.Companion.Entities**
    - **BlueYonder.Entities**
    - **BlueYonder.Companion.Entities.Mappers**
    - **BlueYonder.DataAccess**
    - **BlueYonder.Companion.WNS**
20. Right-click the new project you just created.
21. Point to **Add**, and then click **New Item**.
22. Select **Azure Function**.
23. Name the function **PublishFlightUpdate**.   
24. Click **Add**, a **New Azure Function - PublishFlightUpdate** window opens.
25. In the list on the left, select **Service Bus Queue trigger**.
26. In **Access Rights**, select **Manage**.
27. In the **Connection** text box, type **ServiceBusConnectionString**.
28. In the **Queue name** text box, type **FlightUpdatesQueue**.
29. Click **Ok**.  
    A new file **PublishFlightUpdates.cs** is added to the **BlueYonder.Companion.Functions** project.
30. Add the following **using** statements:
    ```cs
        using BlueYonder.Companion.Entities;
        using BlueYonder.Entities;
        using BlueYonder.Companion.Entities.Mappers;
     ```
31. Change the signature of the **Run** method as follows:
    ```cs
      public static void Run([ServiceBusTrigger("FlightUpdatesQueue", AccessRights.Manage, Connection = "ServiceBusConnectionString")]FlightScheduleDTO updatedScheduleDto, TraceWriter log)
    ```
32. Now replace the following line to the **Run** method:
    ```cs
    MessageHandler.Publish(updatedScheduleDto.ToFlightSchedule());
    ```
33. Right-click the **BlueYonder.Companion.Functions** project.
34. Point to **Add**, and then click **Existing Item**.
35. Select **MessageHandler.cs** that is located in **[repository root]\AllFiles\20487C\Mod07\LabFiles\Assets\MessageHandler.cs** and then, click **Add**.

#### Task 4: Publish the new Azure Functions project and configure it

1. Right click the **BlueYonder.Companion.Functions** project, and then click **Publish**.  
   Select **Azure Functions App**, make sure **Create New** is selected, and then click **Publish**
   
2. A new **Create App Service** window opens.
3. In the **App Name** text box, type **BlueYonderFunctions**_YourInitials_.
4. In the **Resource Group** field, if nothing is populated, click **New**, enter a name to identify the resource group, and then 
click **OK**.
5. If the **Hosting Plan** field is empty, click **New**:
    - In the **App Service Plan** text box, type **BlueYonderFunctions-**_YourInitials_.
    - In the **Location** field, pick the location closest to you.
    - Click **Ok**.
6. If the **Storage Account** field is empty, click **New**:
    - In the **Account Name** text box, type **blueyonderfuncstorage**.
    - Click **Ok**.
7. Click **Create**.
8. Open the Microsoft Azure portal (**http://portal.azure.com**).
9. Select the newly created Azure Functions resource.
10. Select the Azure Function App you created via the publish you performed in the previous step (**BlueYonderFunctions-**_YourInitials_).
11. Click **Application settings**.
12. Under the **Application settings** section, add the following entries:
    - Key: **ServiceBusConnectionString**, Value: The primary connection string of the Service Bus you created earlier for FlightUpdatesQueue
    - Key: **TravelCompanion**, Value: The connection string to the Azure SQL database as configured in the **web.config** file in the **BlueYonder.Companion.Host** project
    - Key: **NotificationHubConnectionString**, value: The connection string for the notification hub from the setup phase
    
      You can also obtain the connection string by going to the **blueyonder07Hub** notification hub from the Azure portal. To do this, click **MANAGE**, click **Access Policies**, click **DefaultFullSharedAccessSignature**, click **CONNECTION STRINGS**, and then click **Primary**.
        
13. Click **Save**.
14. Switch to the Visual Studio 2017 instance that has the **BlueYonder.Companion** solution open.
15. In **Solution Explorer**, right-click the **BlueYonder.Companion.Host**, and then click **Publish**.
16. In **Publish** window, click **Publish**.
17. Switch to the Visual Studio 2017 instance that has the **BlueYonder.Server** solution open.
18. Right-Click on the **BlueYonder.Server.Booking.WebHost** project and click **Debug-> Start new instance**.
19. Locate the address bar and add **\/booking.svc** to the end of your address. Then wait until it will finish to load.

#### Task 5: Test the Service Bus Queue with flight update messages

1. Open the **BlueYonder.Companion.Client solution from [repository root]\AllFiles\20487C\Mod07\LabFiles\begin\BlueYonder.Companion.Client**.
2. To run the application without debugging, press Ctrl + F5. 

   The trip you purchased in the previous exercise appears in the **Upcoming Trip** list.  
   Note the date of the trip.
   
3. Open the **BlueYonder.Companion.FlightsManager** solution file from the **[repository root]\AllFiles\20487C\Mod07\LabFiles\begin\BlueYonder.Server** folder in a new Visual Studio 2017 instance.
4. Open the **web.config** file from the **BlueYonder.FlightsManager** project, in the **&lt;appSettings&gt;** section, locate the **webapi:BlueYonderCompanionService** key, and then update the **[CloudService]** string to the Azure web app name you created by publishing the **BlueYonder.Companion.Host** project in the previous exercise.  
   Run the **BlueYonder.FlightsManager** web application, find the flights from Seattle to Rome, and then change the departure time of your purchased trip to **9:00 AM**.

5. A notification should appear on your machine.
6. Close all open windows.

   >**Results**: After completing this exercise, you should have run the Flight Manager web application, updated the flight departure time of a flight you booked in advance in your client app, and received Windows push notifications directly to your computer.


©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
