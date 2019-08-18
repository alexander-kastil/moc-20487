# Module 5: Creating WCF Services

Wherever  you see a path to a file that starts with *[repository root]*, replace it with the absolute path to the folder in which the 20487 repository resides. For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, the following path: **[repository root]\AllFiles\20487C\Mod03** should be changed to **C:\Users\John Doe\Downloads\20487\AllFiles\20487C\Mod03**.

# Lab: Creating and Consuming the WCF Booking Service

### Exercise 1: Creating the WCF Booking Service

#### Task 1: Create a data contract for the booking request

1. Open **Visual Studio 2017**.
2. On the **File** menu, point to **Open** , and then click **Project/Solution**.
3. In the **File name** text box, type **[repository root]\AllFiles\20487C\Mod05\LabFiles\begin\BlueYonder.Server\BlueYonder.Server.sln** , and then click  **Open**.
4. In **Solution Explorer**, right-click the **BlueYonder.BookingService.Contracts** project, point to **Add** , and then click **Class**.
5. In the **Add New Item - BlueYonder.BookingService.Contracts** dialog box, in the **Name** text box, enter **TripDto** (replace the existing name), and then click **Add**.
6. In the **TripDto.cs** file that opened, add the following **using** directives.

   ```cs
        using System.Runtime.Serialization;
        using BlueYonder.Entities;
   ```
7. To the **TripDto** class declaration, add a **public** access modifier.
8. Above the **TripDto** class declaration, add a **[DataContract]** attribute.
9. In the **TripDto** class, add the following code.

   ```cs
        [DataMember]
        public int FlightScheduleID { get; set; }
          [DataMember]
        public FlightStatus Status { get; set; }
          [DataMember]
        public SeatClass Class { get; set; }
   ```
10. To save the file, press Ctrl+S.
11. In **Solution Explorer**, right-click the **BlueYonder.BookingService.Contracts** project, point to **Add** , and then click **Class**.
12. In the **Add New Item - BlueYonder.BookingService.Contracts** dialog box, in the **Name** text box, enter **ReservationDto** (replace the existing name), and then click **Add**.
13. In the **ReservationDto.cs** file that opened, add the following **using** directives.

   ```cs
        using System.Runtime.Serialization;
        using BlueYonder.Entities;
   ```
14. To the **ReservationDto** class declaration, add a **public** access modifier.
15. Above the **ReservationDto** class declaration, add a **[DataContract]** attribute.
16. In the **ReservationDto** class, add the following code.

   ```cs
        [DataMember]
        public int TravelerId { get; set; }
          [DataMember]
        public DateTime ReservationDate { get; set; }
          [DataMember]
        public TripDto DepartureFlight { get; set; }
          [DataMember]
        public TripDto ReturnFlight { get; set; }
   ```
17. To save the file, press Ctrl+S.

#### Task 2: Create a service contract for the booking service

1. In **Solution Explorer**, right-click the **BlueYonder.BookingService.Contracts** project, point to **Add** , and then click **New Item**.

2. In the **Add New Item - BlueYonder.BookingService.Contracts** dialog box, from the items list, select **Interface**, in the **Name** text box, enter **IBookingService** (replace the existing name), and then click **Add**.
3. In the **IBookingService.cs** file that opened, add the following **using** directives.

   ```cs
        using System.ServiceModel;
        using BlueYonder.BookingService.Contracts.Faults;
   ```
4. To the **IBookingService** interface declaration, add a **public** access modifier.
5. Above the **IBookingService** interface declaration, add the following attribute.

   ```cs
        [ServiceContract(Namespace = "http://blueyonder.server.interfaces/")]
   ```
6. In the **IBookingService** interface, add the following code.

   ```cs
        [OperationContract]
        [FaultContract(typeof(ReservationCreationFault))]
        string CreateReservation(ReservationDto request);
   ```
7. To save the file, press Ctrl+S.

#### Task 3: Implement the service contract

1. In **Solution Explorer**, expand **BlueYonder.BookingService.Implementation** , and then double-click **BookingService.cs** to open it.
2. To implement the **IBookingService** interface, change the declaration of the class as follows.

   ```cs
        public class BookingService : IBookingService
   ```
3. Above the **BookingService** class declaration, add the following attribute.

   ```cs
        [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
   ```
4. Implement the interface by adding the following method code to the class.

   ```cs
        public string CreateReservation(ReservationDto request)
        {
        }
   ```
  >**Note:** At this point, the class will not compile because no value is returned from the method. Ignore this for now, because you will soon write the missing code.

5. In the **CreateReservation** method, type the following code.

   ```cs
        if (request.DepartureFlight == null)
        {
            throw new FaultException<ReservationCreationFault>(
            new ReservationCreationFault
            {
              Description = "Reservation must include a departure flight",
              ReservationDate = request.ReservationDate
            },  "Invalid flight info");
        }
   ```
6. In the **CreateReservation** method, add the following code below the **if** statement block.

   ```cs
        var reservation = new Reservation
        {
          TravelerId = request.TravelerId,
          ReservationDate = request.ReservationDate,
          DepartureFlight = new Trip
          {
            Class = request.DepartureFlight.Class,
            Status = request.DepartureFlight.Status,
            FlightScheduleID = request.DepartureFlight.FlightScheduleID
          }
        };
   ```
7. At the end of the **CreateReservation** method, add the following code.

   ```cs
        if (request.ReturnFlight != null)
        {
           reservation.ReturnFlight = new Trip
           {
             Class = request.ReturnFlight.Class,
             Status = request.ReturnFlight.Status,
                         FlightScheduleID = request.ReturnFlight.FlightScheduleID
           };
        }
   ```
8. In the **CreateReservation** method, add the following code below the last **if** statement block.

   ```cs
        using (IReservationRepository reservationRepository = new ReservationRepository(ConnectionName))
        {
           reservation.ConfirmationCode = ReservationUtils.GenerateConfirmationCode(reservationRepository);
           reservationRepository.Add(reservation);
          reservationRepository.Save();
          return reservation.ConfirmationCode;
        }
   ```
9. To save the file, press Ctrl+S.
10. In the **CreateReservation** method, right-click the first line of code, point to **Breakpoint** , and then click **Insert Breakpoint**.

>**Results**: You will be able to test your results only at the end of Exercise 2.

### Exercise 2: Configuring and Hosting the WCF Service

#### Task 1: Configure the console project to host the WCF service with the TCP endpoint

1. In **Solution Explorer**, locate the **BlueYonder.BookingService.Host** project, and then expand its **References** folder.

  >**Note:** The starter solution already contains all the project references that are needed for the project. This includes the **BlueYonder.BookingService.Contracts** , **BlueYonder.BookingService.Implementation** , **BlueYonder.DataAccess** ,and **BlueYonder.Entities** projects, and the Entity Framework 5.0 package assembly.

2. Right-click the **References** folder, and then click **Add Reference**.
3. In the **Reference Manager - BlueYonder.BookingService.Host** dialog box, expand the **Assemblies** node, and then click **Framework**.
4. Scroll down the assemblies list, point to the **System.ServiceModel** assembly, and then select the check box next to the assembly name.
5. Click **OK**.
6. In **Solution Explorer**, under the **BlueYonder.BookingService.Host** project, double-click **FlightScheduleDatabaseInitializer.cs**.
7. Review the file to understand how the **Seed** method initializes the database with predefined locations and flights.
8. In **Solution Explorer**, under the **BlueYonder.BookingService.Host** project, double-click **App.config**.
9. Before the **</configuration>** tag (the last tag in the file), add the following configuration.

   ```cs
        <system.serviceModel>
          <services>
             <service name="BlueYonder.BookingService.Implementation.BookingService">
              </service>
          </services>
        </system.serviceModel>
   ```
10. Between the **&lt;service&gt;** and **&lt;/service&gt;** tags of the **App.config** file, add the following configuration.

   ```cs
        <endpoint name="BookingTcp"
                  address="net.tcp://localhost:900/BlueYonder/Booking/"
                  binding="netTcpBinding"
        contract="BlueYonder.BookingService.Contracts.IBookingService" />
   ```
11. In the **App.config** file, before the **</configuration>** tag (the last tag in the file), add the following configuration.

   ```cs
        <connectionStrings>
           <add name="BlueYonderServer" connectionString="Data Source=.\SQLEXPRESS;Database=BlueYonder.Server.Lab5;Integrated Security=SSPI" providerName="System.Data.SqlClient" />
        </connectionStrings>
   ```
12. To save the file, press Ctrl+S.

#### Task 2: Create the service hosting code

1. In **Solution Explorer**, in the **BlueYonder.BookingService.Host** project, double-click **Program.cs**.

2. In the **Program** class, after the **Main** method, enter the following methods.

   ```cs
        private static void OnServiceOpened(object sender, EventArgs e)
        {
          Console.WriteLine("Booking Service Is Running... Press [ENTER] to close.");
        }
        private static void OnServiceOpening(object sender, EventArgs e)
        {
          Console.WriteLine("Booking Service Is Initializing...");
        }
   ```
3. At the beginning of the file, add the following **using** directives.

   ```cs
        using System.ServiceModel;
        using BlueYonder.DataAccess;
   ```
4. In the **Main** method, enter the following code.

   ```cs
        var dbInitializer = new FlightScheduleDatabaseInitializer();
        dbInitializer.InitializeDatabase(new TravelCompanionContext(Implementation.BookingService.ConnectionName));
   ```
5. In the **Main** method, after calling the **InitializeDatabase** method, enter the following code.

   ```cs
        var host = new ServiceHost(typeof(Implementation.BookingService));
        host.Opening += OnServiceOpening;
        host.Opened += OnServiceOpened;
        try
        {
           host.Open();
        }
        catch (Exception e)
        {
           host = null;
           Console.WriteLine("  ***   Error occured while trying to open the booking service host *** \n\n{0}", e.Message);
           Console.WriteLine("\n\n Press [ENTER] to exit.");
        }
        Console.ReadLine();
        if (host == null) return;
        try
        {
           host.Close();
        }
        catch (Exception)
        {
           host.Abort();
        }
   ```
6. To save the file, press Ctrl+S.
7. In **Solution Explorer**, right-click the **BlueYonder.BookingService.Host** project, and then click **Set as StartUp Project**.
8. To start debugging the project, press F5.

   >**Note :** If any **Windows security** dialog appears, click **Allow Access**.
   
9. Wait until the initialization and running messages appear in the service host console window.

  >**Note:** Keep the console window open, because you will need to use it later in the lab.

>**Results**: You will be able to start the console application and open the service host.

### Exercise 3: Consuming the WCF Service from the ASP.NET Web API Booking Service

#### Task 1: Add a reference to the service contract project in the ASP.NET Web API project

1. On the Start menu, click the **Visual Studio 2017** tile.
2. On the **File** menu, point to **Open** , and then click **Project/Solution**.
3. In the **File name** text box, type **[repository root]\AllFiles\20487C\Mod05\LabFiles\begin\BlueYonder.Server\BlueYonder.Companion.sln** , and then click **Open**.
4. On the **File** menu, point to **Add** , and then click **Existing Project**.
5. In the **Add Existing Project** dialog box, go to  **[repository root]\Allfiles\20487C\Mod05\Labfiles\begin\BlueYonder.Server\BlueYonder.BookingService.Contracts** , click  **BlueYonder.BookingService.Contracts.csproj** , and then click **Open**.
6. In **Solution Explorer**, right-click the **BlueYonder.Companion.Controllers** project, click **Add**, and then click **Reference**.
7. In the **Reference Manager - BlueYonder.Companion.Controllers** dialog box, in the pane on the left side, expand **Projects** and click **Solution**. In the pane on the right side, point to **BlueYonder.BookingService.Contracts**, select the check box next to the project name, and then click **OK**.
8. **In Solution Explorer**, right-click the **BlueYonder.Companion.Host** project, click **Add**, and then click **Reference**.
9. In the **Reference Manager - BlueYonder.Companion.Host** dialog box, in the pane on the left side, expand Projects and click **Solution**. In the pane on the right side, point to **BlueYonder.BookingService.Contracts**, select the check box next to the project name, and then click **OK**.

#### Task 2: Add the client configuration to the Web.config file

1. In **Solution Explorer**, expand the **BlueYonder.Companion.Host** project, and then double-click **Web.config**.
2. At the bottom of the file, before the **</configuration>** tag, add the following configuration.

   ```cs
        <system.serviceModel>
           <client>
              <endpoint address="net.tcp://localhost:900/BlueYonder/Booking" binding="netTcpBinding" contract="BlueYonder.BookingService.Contracts.IBookingService" name="BookingTcp"/>
           </client>
        </system.serviceModel>
   ```
3.  To save the file, press Ctrl+S.

#### Task 3: Call the Booking service by using ChannelFactory&lt;T&gt;

1. In Solution Explorer, expand the **BlueYonder.Companion.Controllers** project, and then double-click **ReservationsController.cs**.
2. Add the following **using** directives.

   ```cs
        using BlueYonder.BookingService.Contracts;
        using BlueYonder.BookingService.Contracts.Faults;
   ```
3. In the **ReservationsController** class, locate the **// TODO: Module 5: Exercise 3: Task 3.1 Create an instance of the channel factory** comment and then add the following code after it.

   ```cs
       private ChannelFactory<IBookingService> factory =
             new ChannelFactory<IBookingService>("BookingTcp");
   ```
4. Locate the **CreateReservationOnBackendSystem** method, and then uncomment the code below the **// TODO: Module 5: Exercise 3: Task 3.2 Uncomment the Dto creation objects** comment.
5. To create the channel, above the **try** block, add the following statement.

   ```cs
        IBookingService proxy = factory.CreateChannel();
   ```
6. In the **try** block, add the following code.

   ```cs
        string confirmationCode = proxy.CreateReservation(request);
           (proxy as ICommunicationObject).Close();
        return confirmationCode;
   ```
7. From the end of the method, remove the following code.

   ```cs
        return null;
   ```
8. Locate the **// TODO: Module 5: Exercise 3: Task 3.4: Call the service and return the result** comment, and replace the **catch**  block with the following code.
  
   ```cs
        catch (FaultException<ReservationCreationFault> fault)
        {
            HttpResponseMessage faultedResponse = Request.CreateResponse(HttpStatusCode.BadRequest, fault.Detail.Description);
            throw new HttpResponseException(faultedResponse);
        }
   ```
9. Locate the **// TODO: Module 5: Exercise 3: Task 3.5: abort the communication in case of Exception** comment, and after the comment and before calling the **throw** statement, add the following code.

   ```cs
        (proxy as ICommunicationObject).Abort();
   ```
10. In the **ReservationsController** class, look for the **Post** method, and in it, locate the following comment.

   ```cs
        // TODO: Module 5: Exercise 3: Task 3.6: Call the booking service to create the reservation and get the confirmation code.
   ```
11. After the comment, and before saving the entity, add the following code.

   ```cs
        string confirmationCode = CreateReservationOnBackendSystem(newReservation);
        newReservation.ConfirmationCode = confirmationCode;
   ```
12. To save the file, press Ctrl+S.
13. On the **Build** menu, click **Build Solution**.

#### Task 4: Debug the WCF service with the client app

1. If the **ReservationsController.cs** file is closed, in **Solution Explorer**, expand the **BlueYonder.Companion.Controllers** project, and then double-click **ReservationsController.cs**.
2. In the **Post** method, right-click the line of code that starts with **string confirmationCode**, point to **Breakpoint** , and then click **Insert Breakpoint**.
3. In **Solution Explorer**, right-click the **BlueYonder.Companion.Host** project, and then click **Set as StartUp Project**.
4. To start debugging the web application, press F5.
5. Open a new instance **Visual Studio 2017**.
6. On the **File** menu, point to **Open** , and then click **Project/Solution**.
7. In the **File name** text box, type **[repository root]\AllFiles\20487C\Mod05\LabFiles\begin\BlueYonder.Companion.Client\BlueYonder.Companion.Client.sln**, and then click **Open**.
8. If you are prompted by a **Developers License** dialog box, click **I Agree**. If you are prompted by a **User Account Control**  dialog box, click **Yes**. In the **Windows Security** dialog box, enter your email address and a password, and then click **Sign in**. In the **Developers License** dialog box, click **Close**.

  >**Note:** If you do not have valid email address, click **Sign up** and register for the service.  Write down these credentials and use them whenever an email account is required.
9. In **Solution Explorer**, expand the **BlueYonder.Companion.Shared** project, and then double-click **Addresses.cs**.
10. In the **Addresses** class, locate the **BaseUri** property, and then replace the **\[Web Api web app name\]** string with the address that you have in the browser from the privious execution of the **BlueYonder.Companion.Host** project.
> **Note**:it should be "localhost:{port}"
11. In **Solution Explorer**, right-click the **BlueYonder.Companion.Client** project, and then click **Set as StartUp Project**.
12. To start the client app without debugging, press Ctrl+F5.
13. If you are prompted to allow the app to run in the background, click **Allow**.
14. To display the app bar, right-click or swipe from the bottom of the screen.
15. Click **Search** , and in the **Search** text box, type **Paris**. If you are prompted to allow the app to share your location, click **Yes**.
16. Wait for the app to show a list of flights from **Seattle** to **Paris**.
17. Click **Purchase this trip**.
18. In the **First Name** box, enter your first name.
19. In the **Last Name** box, enter your last name.
20. In the **Passport** box, type **Aa1234567**.
21. In the **Mobile Phone** box, type **555-5555555**.
22. In the **Home Address** box, type **423 Main St.**.
23. In the **Email Address** box, enter your email address.
24. Click **Purchase**.
25. Go back to the **Visual Studio 2017** instance with the **BlueYonder.Companion** solution open. The code execution breaks, and the line at the breakpoint is highlighted in yellow.
26. To step over the line, press F10. Switch to Visual Studio 2017 where the **BlueYonder.Server** solution is open. The code execution breaks, and the line at the breakpoint is highlighted in yellow.
27. To run the service code and return to the previous Visual Studio 2017 window, press F5.
28. Hover over the *confirmationCode* variable and verify that it is now set to a random confirmation code it received from the WCF service.
29. To resume execution and go back to the **Visual Studio 2017** instance with the client app open, press F5.
30. To close the confirmation message, click **Close** and then close the client app.
31. To stop debugging the service, switch to the console window that opened when you started the **BlueYonder.Server** application and close it.
32. To stop debugging the web application, return to Visual Studio 2017 where the **BlueYonder.Companion** solution is open and press Shift+F5.
33. Close all open windows.

>**Results**: After you complete this exercise, you will be able to run the Blue Yonder Companion client application and purchase a trip.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
