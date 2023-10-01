## Table of Contents
1. [Installation](#installation)
2. [APIs](#apis)

## Installation
**Clone the repository**: Clone the NetPcContactApi repository to your local machine using the following command in your terminal:

```bash
git clone https://github.com/UnFriend-PL/NetPcContactApi.git
```
Set up the database: Set up your database and update the connection string in the appsettings.json file.

Run the application: Navigate to the root directory of the project in your terminal and run the following command:
dotnet ef database update
dotnet run

## APIs
This section should provide a detailed description of the available APIs. It should describe the endpoints, request methods, request parameters, and expected responses. Here is a brief overview based on the provided code:

### Register
Endpoint: POST /api/Users/Register
Description: Registers a new user.

### Login
Endpoint: POST /api/Users/Login
Description: Authenticates a user and returns a token.

### UpdateUser
Endpoint: PUT /api/Users/UpdateUser
Description: Updates a user's details.

### Users
Endpoint: PUT /api/Users/Users
Description: Returns a list of application users.

### DeleteUser
Endpoint: PUT /api/Users/DeleteUser
Description: Delete selected user.

### ContactCategories
Endpoint: PUT /api/ContactCategories/ContactCategories
Description: Returns a list of contact categories.

### ContactSubCategories
Endpoint: PUT /api/ContactCategories/ContactSubCategories
Description: Returns a list of contact subcategories.

### CreateContactSubCategory
Endpoint: PUT /api/ContactCategories/CreateContactSubCategory
Description: Create and return new contact subcategory.
