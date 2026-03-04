
# Blog Backend API - Project Overview

## Project Goal

Create a backend for Blog Application:

- Support full CRUD operations
- All users to create account and login
- Deploy to Azure
- Uses SCRUM workflow concepts???
- Introduce Azure DevOps practices

## Stack (meaning what particular framework)
// What stack is your software built in!
    - Back end will be .Net 9, ASP.net core, EF Core, SQL Server
    (Ef core to talk to our database with sql) (ASP.net core): Create a web based API endpoints for front-end to talk to endpoint (works on cloud and web)
    ASP.net core is the latest version!
    - Front End will be done in Next JS with Typescript (To be done by jacob.) Flowbite(Tailwind!) TBA (Vercel or Azure)!

## Application Features

### User Capabilities

 - Create an account
 - Login
 - Delete account

 ### Blog Features

 - View all published blog post
 - Filter Blog Post
 - Create new Posts
 - Edit Existing Posts
 - Delete Blog Posts
 - Publish/ UnPublish Posts

 ### Pages (FrontEnd Connected to API)

 - Create Account Page
 - Blog view post Page of published items
 - Dashboard page ( This is the profile page will edit, delete, publish and unpublish our blog posts )

 - **Blog Page**
    - Display all published blog items
 - **Dashboard**
    - User profile page
    - Create blog posts
    - Edit blog posts
    - Delete blog posts

## Project Folder Structure

### Controllers

#### UserController

Handles All our users interactions,
Endpoint:

- Login
- Add user
- update user
- delete user

#### BlogController

Handles All our Blog operations;

Endpoints:

 - Create Blog Items (C)
 - Get All Blog Items (R)
 - Get Blog Items By Category (R)
 - Get Blog Items By Tags (R)
 - Get Blog Items By Date (R)
 - Get Published Blog Items (R)
 - Update Blog Items (U)
 - Delete Blog Item (D)
 - Get Blog Items By UserId (R)

 - Delete will be used for Soft delete / publish logic:

 --- 

 ## Models 

### UserModel

 ```csharp 

 int Id;
 string Username;
 string Salt
 string Hash
```
### BlogItemModel
 ```csharp 

int Id
int UserId
string PublisherName
string Title
string Image
string Description
string Date
string Category
string Tags
bool IsPublished
bool IsDeleted
```
## Items Saved to our DB
### We need a way to sign in with our username and password

```csharp
(Data transfer Object)
### LoginModelDTO

    string Username
    string Password

### CreateAccountModelDTO

    int Id = 0;
    string Username
    string Password

### PasswordModelDTO

    string Salt;
    string Hash;

```
Services Folder with DBContext and Services
### Services
    Context/Folder
    - DataContext
    - UserService/file

        - GetUserByUsername
        - Login
        - AddUser
        - UpdateUser
        - DeleteUser

### BlogItemService

    - AddBlogItems
    - GetAllBlogItems
    - GetBlogItemsByCategory
    - GetBlogItemsByTags
    - GetBlogItemsByDate
    - GetPublishedBlogItems
    - UpdateBlogItems
    - DeleteBlogItems
    - GetUserById
### PasswordServices

    - Hash Password
    - Verify Hash Password
    - 