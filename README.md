# ShopsRUs.Service
ShopsRUs is an existing retail outlet. They would like to provide discount to their customers on all their web/mobile platforms.

| app | url |
| --- | --- |
| ShopsRUs API Health  | [http://localhost:5000/health](http://localhost:5000/health) |


#### API Documentation
API documentation is [here](https://localhost:5000/swagger/index.html)


#### Required Features
Customer
  - Get a list of all customers
  - Create a customer
  - Get a specific customer by ID
  - Get a specific customer by name
    
Discounts
  - Get a list of all discounts
  - Get a specific discount percentage by type
  - Add a new discount type

Invoice
  - Get total invoice amount given a bill


#### Technologies Used
- dotnet-core 3.1
- POSTGRESQL

##### Prerequisites
- dotnet-core 3.1 SDK needs to be installed on your machine. See [dotnet-core Docs](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- Although not required visual studio 2019 needs to be installed
- PG4Admin needs to be installed on you local machine

##### Setting up locally
- Clone with SSH => `git@github.com:chiomajoshua/ShopsRUs.Service.git`
- Clone with HTTPS => `https://github.com/chiomajoshua/ShopsRUs.Service.git`

##### Running the app on an attached device
- Open project with visual studio, run the project.
- Run `dotnet run` in command prompt


## Accessing PostGres DB

http://127.0.0.1:53192/browser

You should see the pgAdmin login page. Login with your username and password you set for pgadmin
Once you login, you should see the pgAdmin dashboard.
Now, you can work with your PostgreSQL database as much as you want.

