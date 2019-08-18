# Module 1: Overview of Service and Cloud Technologies

# Lab: Exploring the Work Environment

#### Scenario

In this lab, you will explore several frameworks and platforms, such as Entity Framework Core, Microsoft ASP.NET Core Web API, and Microsoft Azure, which are used for creating distributed applications.

#### Objectives

After completing this lab, you will be able to:

- Create an entity data model by using Entity Framework Core.
- Create an ASP.NET Core Web API service.
- Create an Azure SQL database.
- Deploy a web application to an Azure website.

### Exercise 1: Creating an ASP.NET Core Project

#### Scenario

In this exercise, you will create a Web API through ASP.NET Core by using the command prompt.

#### Task 1: Create a new ASP.NET Core project

1. To create a ASP.NET Core Web API core project, at the command prompt, run the **dotnet** tool.

   >**Results**: After completing this exercise, you should have a basic Web API ASP.NET Core project.

### Exercise 2: Creating a Simple Entity Framework Model

#### Scenario

In this exercise, you will create data model classes to represent flights, implement a **DbContext** -derived class, and create a new repository class for the **Flight** entity.

The main tasks for this exercise are:

- Prepare the data model classes for Entity Framework Core.
- Add the newly created entity to the context.

#### Task 1: Create a new POCO entity

1. Open the project in **VSCode**.
2. Install the **Microsoft.EntityFrameworkCore.SqlServer** with the **dotnet** tool.
3. Create a new **Flight** class, and then add the following properties:
    ```cs
    namespace BlueYonder.Flights.Models
    {
        public class Flight
        {
            public int Id { get ;set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string FlightNumber { get; set; }
            public DateTime DepartureTime { get; set; }
        }
    }
    ```

#### Task 2: Create a new DbContext class

1. Create a new **FlightsContext** class, which will inherit from **DbContext**.
2. Add a constructor to the **FlightsContext** class, with the **DbContextOptions<FlightsContext>** parameter type.
3. Call the base constructor, and then pass the option parameters.
4. To the **Flight** entity, add a new **DbSet\<T\>** property and name it **Flights**.
5. In **Startup.cs**, locate **ConfigureServices**, register **FlightsContext** as service, and then configure it to use SQL Server with **defaultConnection**.
    >**Note**: In Exercise 4, we will create a web app and a SQL database in Azure, which will generate a connection string. To access the database, you need to use **defaultConnection** key.

   >**Results**: After completing this exercise, you should have created entity framework wrappers for the **BlueYonder** database.

### Exercise 3: Creating a Web API class

#### Scenario

Implement the flight service by using ASP.NET Core Web API. Start by creating a new ASP.NET Web API controller, and implement CRUD functionality by using the **POST** and **GET HTTP** methods.

#### Task 1: Create a new class for the web API

1. Expand the **Controllers** folder Change the name of the **ValuesController.cs** file to **FlightsController.cs**.
2. In **FlightsController.cs**, change the class name to **FlightsController**.
3. Add a new field and and name it **FlightContext**.
4. Add a constructor to the **FlightsController** class, with the **FlightContext** parameter type, and the **_context** parameter.
5. In the constructor, assign the parameter to the field.

#### Task 2: Create an action and use the Entity Framework context

1. To modify the **GET** method without using parameters, preform the the following steps:
  - Change the method signature **string** to **Flight**.
  - Inside the method, modify the **return** value to return all the flights from the database.
2. To modify the **POST** method, perform the following steps:
  - Replace the **string** type parameter with **Flight**.
  - Change the method signature, **void** to **IActionResult**.
  - Add the **Flight** parameter to the context and save the changes.
  - Return **CreatedAtAction** with the **flight** id.
  >**Note**: **CreatedAtResult** returns 201 status code along with a URI to the created resource.

  >**Results**: After completing this exercise, you should have created a web app that exposes the Web API.

### Exercise 4: Deploying the Web Application to Azure

#### Scenario

In this exercise, you will create an Azure web app and a SQL database to host the ASP.NET Core Web API application.

The main tasks for this exercise are:

- Create a new Azure web app and SQL database.
- Deploy the web application to the Azure web app.
- Test the Web API using Windows PowerShell.
- View the results in the database.

#### Task 1: Create an Azure web app and an SQL database

1. Go to the Azure portal at **https://portal.azure.com**.
2. In the left pane, click **App Services**, click **Add**, and then select **Web App + SQL**.
3. Name the web app **flightsmod1lab***YourInitials*. (Replace *YourInitials* with your initials).
4. Create a new resource group.
5. Configure the SQL database and server.
6. Click **Create** and wait until the new web app is ready.
7. To create a new table in the SQL database, browse to it, and then in **Query editor**, run the following script:
    ```sql
    CREATE TABLE [dbo].[Flights](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Origin] [varchar](50) NOT NULL,
    [Destination] [varchar](50) NOT NULL,
    [FlightNumber] [varchar](50) NOT NULL,
    [DepartureTime] [date] NOT NULL,

    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    ))
    ```

#### Task 2: Deploy the web application to the Azure web app

1. Browse to the **App Services**, and then select the web app that you created.
2. In **Deployment Credentials**, fill in the form and save it.
3. Switch to **VSCode**, create a new folder, and then name it **Properties**. In this folder, create another folder, and then name it **PublishProfiles**.
4. In the **PublishProfiles** folder, create a new file, name it **Azure.pubxml**, and then add the following code to the file:
    ```xml
    <Project>
      <PropertyGroup>
        <PublishProtocol>Kudu</PublishProtocol>
        <PublishSiteName>[App Service Url]</PublishSiteName>
        <UserName>[FTP/Deployment UserName]</UserName>
        <Password>[Password]</Password>
      </PropertyGroup>
    </Project>
    ```
  >**Note**: Replace the the information inside the brakets with the information that you filled in **Deployment Credentials** on Azure Portal.
5. Switch to the command prompt, and then run the following command:
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
  >**Note**: If the there was an error in the publish process, restart the flightsmod1lab*YourInitials* app services. 
 
#### Task 3: Test the web API

1. Open Windows PowerShell.
2. To add a flight to the database, run the following command:
    ```bash
    $postParams = "{'origin': 'Germany',
        'destination': 'France',
        'flightNumber': 'GF7625',
        'departureTime': '0001-01-01T00:00:00'}"
    Invoke-WebRequest -Uri http://flightsmod1lab{YourInitials}.azurewebsites.net/api/flights -ContentType "application/json" -Method POST -Body $postParams
    ```
3. Open Microsoft Edge.
4. Go to the following URL:
    
    https://flightsmod1lab{YourInitials}.azurewebsites.net/api/flights
    
5. Verify that you got the expected result.

#### Task 4: View result in the database

1. Switch to **Azure Protal**.
2. Browse to your database, and then in **Query editor**, enter the following script to verify that the flight was added to the database:
    ```sql
    select * from flights
    ```

   >**Results**: After completing this exercise, you should have ensured that all your products are hosted on the Azure cloud by using Azure SQL Database and Azure Web App.


©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
