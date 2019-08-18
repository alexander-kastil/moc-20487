
# Module 3: Creating and Consuming ASP.NET Core Web APIs 

# Lab: Creating an ASP.NET Core Web API 

#### Scenario

In this lab, you will create and use ASP.NET Core Web APIs.

#### Objectives

After completing this lab, you will be able to:

- Create a Web API controller to expose APIs.
- Invoke the API through a browser.
- Use **httpClient**, create **ConsoleApplication**, and then connect to the server by using **httpClient**.


### Exercise 1: Create a Controller Class
 
#### Task 1: Add a controller class

1. Open Command Prompt, and then browse to **[Repository Root]\Allfiles\Mod03\LabFiles\Lab1\Starter**.
2. Open the project in Microsoft Visual Studio Code.
3. In the **BlueYonder.Hotels.Service** project, add a new controller called **HotelBookingController**.
4. In **HotelBookingController**, add the **HotelBookingRepository** field, and then initialize the field by using the constructor.

#### Task 2: Add action methods to the controller for GET, POST, and PUT

1. Add a new **GET** action with the *id* parameter, and then return the **Booking** entity.
2. Add a new **POST** action with the *Booking* parameter, and then return the **Booking** entity.
3. Add a new **PUT** action with the *id* and *Booking* parameters, and then return the **Booking** entity.

### Exercise 2: Use the API from a Browser

#### Task 1: Use a browser to access the GET action

1. Run the **BlueYonder.Hotels.Service** project.
2. Open a browser,  and navigate to the following URL:
   ```url
    http://localhost:5000/api/HotelBooking/1
   ```
   >**Note**: If there is an error in the Console after running the application, run the following command: **dotnet dev-certs https --trust**.
3. Close all open windows.
   >**Result:** You should see the **Booking** reservation with **Booking Id: 1**.

### Exercise 3: Create a Client

#### Task 1: Create a console project and add a reference to System.Net.Http

1. To change the directory to the Starter project, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod03\LabFiles\Lab1\Starter
   ```
2. Create a new console application called **BlueYonder.Hotels.Client**.
3. Add the **BlueYonder.Hotels.Client** project to the solution.
4. In the **BlueYonder.Hotels.Client** project, install the **Microsoft AspNet WebApi Client** package.

#### Task 2: Use HttpClient to Call the GET and PUT Actions of the Controller

1. Open the **BlueYonder.Hotels.Client** project in Visual Studio Code.
2. In **BlueYonder.Hotels.Client.csproj**, add a reference DAL class library.
3. In **Program.cs**, change the **Main** method return type declaration to **async Task**.
4. In the **Main** method, create a new **HttpClient** instance.
5. Inside the **HttpClient** bracket, create a **HotelBooking GET** request, and pass **id=1** to it.
6. To read the content of the response as a string, under **HttpResponseMessage**, use **ReadAsStringAsync**, and print the result to the console.
7. Read the content of the response as a **Booking** entity.
8. Change the **Paid** property of the response to **false**.
9. Create a **PUT** request, and then pass the **id=1** to it, and the response a **Booking** entity.
10. Read the content of the **PUT** response as a string, and print the result to the console.
11. Run the **BlueYonder.Hotels.Service** project.
12. Run the **BlueYonder.Hotels.Client** project.
13. At the command prompt, verify that the bookings from the **GET** and the **PUT** requests appear. 
14. Close all open windows.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
