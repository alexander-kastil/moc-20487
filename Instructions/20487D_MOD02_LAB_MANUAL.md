
# Module 2: Querying and Manipulating Data Using Entity Framework

# Lab: Creating a Data Access Layer using Entity Framework 

#### Scenario

In this lab, you will use the Entity Framework Core to connect to an SQL Database. 

#### Objectives

After completing this lab, you will be able to:

- Create a DAL layer.
- Create an entity data model by using Entity Framework Core.

### Exercise 1: Creating a data model

#### Scenario

In this exercise, you will create the data access layer and connect to the database by using Entity Framework Core
to perform CRUD operations on the SQL Express database.

#### Task 1: Create a class library for the data model

1. Open a command prompt.
2. Browse to the following path **[Repository Root]\Allfiles\Mod02\LabFiles\Lab1\Starter\DAL**, and then create a new **ASP.NET Core Class Library** and name it **DAL**.
3. Use the command prompt to install the  **Entity Framework Core** package.
4. Open the project with Microsoft Visual Studio Code.
5. Add a new folder to the project and name it **Models**.

#### Task 2: Create data model entities using the code-first approach

1. Create a new class and name it **Traveler**, and then add the following properties:
    ```cs
    public int TravelerId { get; set; }
    public string Name { get; set; }
    public string Email {get; set; }
    ```
2. Create a new class and name it **Room**, and then add the following properties:
    ```cs
    public int RoomId { get; set; }
    public int Number { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    ```
3. Create a new class and name it **Hotel**, and then add the following properties:
    ```cs 
    public int HotelId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public ICollection<Room> Rooms { get; set; }
    ```
4. Create a new class and name it **Booking**, and then add the following properties:
    ```cs
    public int BookingId { get; set; }
    public Room Room { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Guests { get; set; }
    public decimal TotalFee { get; set; }
    public bool Paid { get; set; }
    public Traveler Traveler { get; set; }
    ```

#### Task 3: Create a DbContext

1. Create a new folder and name it **Database**.
2. Create a new class that inherits from **DbContext** and name it **MyDbContext**.
3. Add **DbSet** property to each of the models that you created in the previous task.
4. Paste the following code to add two constructors:
    ```cs
    private void InitialDBContext()
    {
        DbInitializer.Initialize(this);
    }
    // Default Constructor
    public MyDbContext()
    {
        InitialDBContext();
    }
    // Constructor with options
    public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
    {
        InitialDBContext();
    }
    ```
5. Override the **OnConfiguring** method to use **SqlExpress**. 

   >**Results** Your application now has a functioning data access layer. 

### Exercise 2: Query your database

#### Scenario

In this exercise, you will use the DAL class library to create a new console application
to display all the data from the database.


#### Task 1: Create a database initializer with dummy data

1. Inside the **Database** folder, create new class and name it **DbInitializer**.
2. To create the **Seed** method, add the following code:
    ```cs
    private static void Seed(MyDbContext context)
    {
        // Create list with dummy travelers 
        List<Travelr> travelerList = new List<Traveler>
        {
            new Traveler(){ Name = "Jon Due", Email = "jond@outlook.com"},
            new Traveler(){ Name = "Jon Due2", Email = "jond2@outlook.com"},
            new Traveler(){ Name = "Jon Due3", Email = "jond3@outlook.com"}
        }   
        // Create list with dummy bookings 
        List<Booking> bookingList = new List<Booking>
        {
            new Booking()
            { 
                DateCreated = DateTime.Now, 
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(2),
                Guests = 2, 
                Paid = true, 
                Traveler = travelerList[0]
            },
            new Booking()
            { 
                DateCreated = DateTime.Now.AddDays(3),
                CheckIn = DateTime.Now.AddDays,
                CheckOut = DateTime.Now.AddDays(8), 
                Guests = 3, Paid = true, 
                Traveler = travelerList[1]
            },
            new Booking()  
            { 
                DateCreated = DateTime.Now.AddDays(-10), 
                CheckIn = DateTime.Now.AddDays( CheckOut = DateTime.Now.AddDays(11),
                Guests = 1, 
                Paid = false, 
                Traveler = travelerList[2]
            }
        };
        // Create list with dummy rooms
        List<Room> roomList = new List<Room>
        {
            new Room(){ Number = 10, Price = 300},
            new Room(){ Number = 20, Price = 200},
            new Room(){ Number = 30, Price = 100}
        }   
        roomList[0].Bookings.Add(bookingList[0]);
        roomList[1].Bookings.Add(bookingList[1]);
        roomList[1].Bookings.Add(bookingList[2])

        Hotel hotel = new Hotel()
        {
            Name = "Azure Hotel",
            Address = "Cloud",
            Rooms = roomList
        };
        // Insert the dummy data to the database
        context.Travelers.AddRange(travelerList);
        context.Bookings.AddRange(bookingList);
        context.Rooms.AddRange(roomList);
        context.Hotels.Add(hotel)   
        context.SaveChanges()   
    }
    ```
3. Add a new **static** method that gets **MyDbContext** as a parameter and does the following logic, and name it **Initialize**:
    - Make sure that the database was created. 
    - If the database was created for the first time, use the **Seed** method.

#### Task 2: Write a LINQ query to query the data

1. Close the Visual Studio Code window.
2. From the command prompt, create a new **ASP.NET Core Console Application** project.
3. Switch to the command prompt.
4. In the **[Repository Root]\Allfiles\Mod02\LabFiles\Lab1\Starter\DatabaseTester** path, create a new **ASP.NET Core Console Application** and name it **DatabaseTester**.
5. Create a new **Solution** and name it **Mod2Lab1**.
6. Add the **DAL** and **DatabaseTester** projects to the **Mod2Lab1** solution.
7. Open the folder in Visual Studio Code.
8. In **DatabaseTester.csproj**, add a reference to the **DAL** project.
9. In the **DatabaseTester** folder, navigate to **Program.cs**, and locate the **main** method.
10. Create a new **MyDbContext** instance.
11. In the **DbInitializer** class, use the **Initialize** method with the new **MyDbContext** instance.
12. Display the following info on the screen:
    - The name of the hotel.
    - The number and price of the rooms.
    - The name and email of all the users.
13. From the command prompt, run the **DatabaseTester** application.

   >**Results** Your application can now display all of the data from the database by using LINQ queries.

# Lab: Manipulating Data

#### Scenario

In this lab, you will create a repository with CRUD methods and inject two kinds of database configurations to work with SQL Express and SQLite.

#### Objectives

After completing this lab, you will be able to:

- Create a hotel booking repository and populate it with CRUD methods.
- Test the queries with SQL Express and SQLite databases.

### Exercise 1: Create repository methods

#### Scenario

In this exercise, you will create a hotel booking repository.

#### Task 1: Create a method to add entities

1. Open a command prompt.
2. To change directory to the startup project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod02\LabFiles\Lab2\Starter
    ```
3. Open the folder in Visual Studio Code.
4. In Visual Studio Code, expand the **DAL** folder, expand the **Repository** folder, and then double-click **HotelBookingRepository.cs**.
5. Add a **DbContextOptions** field and name it **_options**.
6. To initialize **_options** field, add a constructor with **DbContextOptions** as an optional parameter.
7. Create and implement a new method that gets those parameters and name it **Add**:
   ```cs
   int travelerId, int roomId, DateTime checkIn, int guest = 1
   ```
#### Task 2: Create a method to update entities

1. Add a new method that gets the **Booking** class as a parameter and updates the database, and name it **Update**.

#### Task 3: Create a method to delete entities

1. Add a new method that gets the **BookingId** int as a parameter and deletes the database, and name it **Delete**.

    >**Results** You created a repository with the **Add**, **Update**, **Delete** methods.

#### Exercise 2: Test the model using SQL Server and SQLite

#### Scenario

In this exercise, you will inject SQL Lite DB to the repository,
and you will create, test and run them on SQL Lite and SQL Server. 

#### Task 1: Create test code with transactions

1. Open a command prompt.
2. To change the directory to the **Starter** folder, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod02\LabFiles\Lab2\Starter
   ```
3. Create a new **Unit Test Project** and name it **DAL.Test**.
4. Add the new testing project to the **solution**.
5. From the command prompt, open Visual Studio Code.
6. From the **DAL** project, add a reference to **DAL.Test**.
7. Locate the **UnitTest1.cs** file and rename it **BookingRepositoryTests**.
8. Change **public void TestMethod1()** to **public async Task AddTwoBookingsTest()**.
9. To test the **Transaction Scope** with two bookings, paste the following code in the **AddTwoBookingsTest** method:
    ```cs
    Booking fristBooking;
    Booking secondBooking;
    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
    {
        HotelBookingRepository repository = new HotelBookingRepository();
        fristBooking = await repository.Add(1, 1, DateTime.Now.AddDays(6), 4);
        secondBooking = await repository.Add(1, 2, DateTime.Now.AddDays(8), 3)
        scope.Complete();
    }
    
    using (MyDbContext context = new MyDbContext())
    {
        int bookingsCounter = context.Bookings.Where(booking => booking.BookingId == fristBooking.BookingId ||
                                                                booking.BookingId == secondBooking.BookingId).ToList().Count
        Assert.AreEqual(2, bookingsCounter);
    }
    ```

#### Task 2: Test code against a local Microsoft SQL Server database

1. Switch to the command prompt.
2. Change the directory to the **DAL.Test** folder. 
3. To test the **AddTwoBookingsTest** method, run the following command:
   ```bash
   dotnet test
   ```
4. Open **SQL Operations Studio**:
    - Check whether the **Mod2Lab2DB** database was created.
    - Check whether there are two bookings saved in the database.

#### Task 3: Replace the SQL Server provider with SQLite

1. From the command prompt, navigate to **DAL** folder.
2. Install the **Entity Framework Core Sqlite** package version 2.1.1.
3. Open the solution in Visual Studio Code.
4. Expand the **DAL.Test** folder, then double-click **BookingRepositoryTests.cs**.
5. Paste the following field:
    ```cs
    private DbContextOptions<MyDbContext> _options =
               new DbContextOptionsBuilder<MyDbContext>()
                   .UseSqlite(@"Data Source = [Repository Root]\Allfiles\Mod02\LabFiles\Lab2\Database\SqliteHotel.db")
                   .Options;
    ```
6. Add a testing method that tests insertion of two books in to **SQL Lite** and name it **AddTwoBookingsSQLiteTest**.

#### Task 4: Test code against SQLite

1. At the command prompt, run the testing project.
2. Check whether the **SqliteHotel.db** file was created in the **[Repository Root]\Allfiles\Mod02\LabFiles\Lab2\Database** folder.
3. Open **DB Browser for SQLite**. 
4. Verify that the two bookings exist in the database. 
5. Close all open windows.
    >**Results** You finished testing your database with SQL Lite and SQL Server.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
