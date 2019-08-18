
# Module 4: Extending ASP.NET Core HTTP services

# Lab: Customizing the ASP.NET Core Pipeline

#### Scenario

In this lab, you will customize the ASP.NET Core Pipeline.

#### Objectives

After completing this lab, you will be able to:

- Add inversion of control by using Dependency Injection to the project.
- Create a cache mechanism and action filters.
- Add middleware to inform the client through header response.
  

### Exercise 1: Use Dependency Injection to Get a Repository Object

#### Task 1: Create an interface for the repository 

1.  Open a **Command Prompt** window, and then browse to **[Repository Root]\Allfiles\Mod04\LabFiles\Lab1\Starter\BlueYonder.Hotels**.
2.  Open the project in Microsoft Visual Studio Code.
3.  In **BlueYonder.Hotels.DAL**, under the **Repository** folder, add a new interface, and then name it **IHotelBookingRepository.cs**.
4.  To **IHotelBookingRepository.cs**, add the following method signatures:
    ```cs
    IEnumerable<Room> GetAvaliabileByDate(DateTime date);
    IEnumerable<Reservation> GetAllReservation();
    Task DeleteReservation(int reservationId);
    ```

#### Task 2: Implement the interface on the repository

1. In the **BlueYonder.Hotels.DAL** project, in **HotelBookingRepository** class, implement the **IHotelBookingRepository** interface.

#### Task 3: Register the repository object in the ASP.NET Core Dependency Injection mechanism

1. In the **BlueYonderHotels.Service** project, locate **Startup.cs**.
2. In the **ConfigureServices** method, register **HotelBookingRepository**.

#### Task 4: Change the controller’s constructor to request an injected repository

1. Browse to **HotelBookingController**, change **HotelBookingRepository** to **IHotelBookingRepository**, and then initialize it by using the constructor.

### Exercise 2: Create a Cache Filter

#### Task 1: Create an action filter for cache headers

1. In the **BlueYonderHotels.Service** project, create an **Attributes** folder.
2. Create a new **CacheAttribute** class, and make sure that it is derived from **ActionFilterAttribute**.
3. Add the following fields:
      ```cs
    private string _headerMessage { get; set; }
    private TimeSpan _durationTime;
    private const int _defulatDuration = 60;
    private Dictionary<string,(DateTime, IActionResult)> _cache = new Dictionary<string, (DateTime,IActionResult)>();
    ```
4. To initiate the **_durationTime** field, add a constructor with the optional **int** parameter.
5. To initiate the **_headerMessage** field, add a constructor with the **string** parameter. 
6. To check the cache validation, with the **FilterContext** parameter and return **bool** result, add a new **CacheValid** method.
7. To override the **OnActionExecuting** method, enter the following code:
   ```cs
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (CacheValid(context))
        {
            context.Result = _cache[context.HttpContext.Request.Path].Item2;
            return;
        }
        base.OnActionExecuting(context);
    }
   ```
8.  To override the **OnResultExecuted** method, enter the following method:
    ```cs
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        if(!CacheValid(context))
             _cache[context.HttpContext.Request.Path] = (DateTime.Now,context.Result);
        base.OnResultExecuted(context);
    }
    ```

#### Task 2: Add the cache filter to several actions

1. In **HotelBookingController**, above the **GetAvailability** method, add the **Cache("X-No-Cache")** attribute.
   
#### Task 3: Test cacheable and non-cacheable actions from a browser

1. Run the **BlueYonderHotels.Service** service.
2. Open Windows Powershell.
3. To get the reservations from the server, run the following command:
    ```bash
    $getReservations = Invoke-WebRequest -Url https://localhost:5001/api/HotelBooking/Reservation -UseBasicparsing
    ```
    >**Note:** If you get the **The underlying connection was closed: An unexpected error occurred on a send** error message, run the  **[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12** command, and then redo the step.
4. To display the http result, run the following command:
    ```bash
    $getReservations
    ```
5. To display only the http content, run the following command:
    ```bash
    $getReservations.Content
    ```
6. To get the available rooms on the current date, run the following command:
   ```bash
   Invoke-WebRequest -Url https://localhost:5001/api/HotelBooking/Availability/[year]-[month]-[day] -UseBasicparsing
   ```
   >**Note:** Replace the last section of the URL with the current date.
7. To remove the reservation, run the following command:
    ```bash
    Invoke-WebRequest -Uri https://localhost:5001/api/HotelBooking/Reservation/1 -Method DELETE -UseBasicparsing
    ```
8. Repeat step 6, verify that the room list didn't change, wait for one more minute, repeat step 7 again, and then verify that available rooms are appearing in the room list.

### Exercise 3: Create a Debugging Middleware

#### Task 1: Create a middleware class to calculate the execution time

1. Under **BlueYonderHotels.Service** folder, create a new **Middleware** folder.
2. Create a new static **ExecutionTimeMiddleware** class.
3. Add a new static **AddResponeHeaders** method with the **HttpContext** parameter, the **Func<Task>** parameter, and a return **Task** entity.

#### Task 2: Write server and debug information to response headers
 
1. Inside the method, type **AddResponeHeaders**, and then add:
    - **Server Name**
    - **OS Version**
    - **Request Execution Time**
2. Under **X-OS-Version**, add the **Request Execution Time** header:
    ```cs
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    context.Response.OnStarting(state => 
    {
        var httpContext = (HttpContext)state;
        stopwatch.Stop();
        httpContext.Response.Headers.Add("X-Request-Execution-Time", stopwatch.ElapsedMilliseconds.ToString());
        return Task.CompletedTask;
    }, context);
    ```
3. To continue to the next middleware, under **X-Request-ExecutionTime** enter the following code:
   ```cs
    await next();
   ```
   
#### Task 3: Create the IApplicationBuilder helper class

1. Create a static extension method for **IApplicationBuilder** with the **IApplicationBuilder** parameter and a return **IApplicationBuilder** entity. 

#### Task 4: Register the middleware to the ASP.NET Core pipeline

1. In **Startup.cs**, under **BlueYonderHotels.Service**, locate the **Configure** method.
2. Above **app.UseHttpsRedirection**, enter **UseExecutionTimeMiddleware**.
   
#### Task 5: Test the new middleware from a browser

1. Open Microsoft Edge and browse to **Get HotelBooking Reservation**.
2. Open **Developer Tools**, click **Network**, and then select **Reservation**.
3. Expand **Response Header**.
4. Verify that you received **x-os-version**, **x-server-name**, and **x-request-execution-time** from the server.
5. Close all open windows.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
