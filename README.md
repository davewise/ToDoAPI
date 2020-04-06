# ToDoAPI

A RESTful API to support a to-do list application.

For a swagger view of the api go here:
https://wise-to-do-api.herokuapp.com

The ToDoAPI exposes methods for creating and managing to-do lists, adding/editing/deleting items from to-do lists, and marking to-do list items as completed. The ToDoAPI has in-memory persistence layer.

The ToDoAPI exposes two controllers as RESTful resources: One handles ToDoList objects, the other handles ToDoListItem objects.

The ToDoAPI also exposes a controller to allow for account registration and login.

The end user is able to create, edit and delete ToDoList objects. The end user is also able to retrieve all ToDoList objects associated with him/her.
The end user is also able to add or remove ToDoListItem objects to individual ToDoList objects, edit the list items and mark them as completed.

The ToDoAPI was developed in the VisualStudio IDE. Language: C# Frameworks: Microsoft.NETCore.App 3.1.0, Microsoft.ASPNETCore.App 3.1.2

## Docker Build

    docker build -t todoapi .
    
## Docker Run

    docker run -it --rm -p 5000:80 --name todoapi todoapi

## Heroku Push

    heroku container:push web

## Heroku Release

    heroku container:release web


## How to access the ToDoAPI






# ToDoAPI

A RESTful API to support a to-do list application.

## Indices

* [Default](#default)

  * [ToDoItem Delete](#1-todoitem-delete)
  * [ToDoItems Get](#2-todoitems-get)
  * [ToDoItem Update](#3-todoitem-update)
  * [ToDoItem Create](#4-todoitem-create)
  * [ToDoList Delete](#5-todolist-delete)
  * [ToDoLists Get](#6-todolists-get)
  * [ToDoList Update](#7-todolist-update)
  * [ToDoList Create](#8-todolist-create)
  * [Account Login](#9-account-login)
  * [Account Register](#10-account-register)


--------


## Default



### 1. ToDoItem Delete


Deletes a specific ToDoItem based on index for a user given a toDoListId.

/api/ToDoItems/{toDoListId}/{id}


***Endpoint:***

```bash
Method: DELETE
Type: 
URL: https://wise-to-do-api.herokuapp.com/api/ToDoItems/1/2
```



### 2. ToDoItems Get


Return a list of ToDoItems for a user given a toDoListId.

/api/ToDoItems/{toDoListId}


***Endpoint:***

```bash
Method: GET
Type: RAW
URL: https://wise-to-do-api.herokuapp.com/api/ToDoItems/1
```



***Body:***

```js        
{
    "Id":1,
    "name":"walk dog two",
    "isComplete":false,
    "Secret":"the secret"
  }
```



### 3. ToDoItem Update


Updates a ToDoItem based on index for a user given a toDoListId.

/api/ToDoItems/{toDoListId}/{id}


***Endpoint:***

```bash
Method: PUT
Type: RAW
URL: https://wise-to-do-api.herokuapp.com/api/ToDoItems/1/3
```



***Body:***

```js        
{
    "Name": "Walk dog",
    "IsComplete": true
}
```



### 4. ToDoItem Create


Creates a ToDoItem for a user given a toDoListId.

/api/ToDoItems/{toDoListId}


***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://wise-to-do-api.herokuapp.com/api/ToDoItems/1
```



***Body:***

```js        
{
    "Name": "Walk dog",
    "IsComplete": false
}
```



### 5. ToDoList Delete


Deletes a ToDoList for a user given a toDoListId.

/api/ToDoLists/{id}


***Endpoint:***

```bash
Method: DELETE
Type: 
URL: https://wise-to-do-api.herokuapp.com/api/ToDoLists/2
```



### 6. ToDoLists Get


Return a ToDoList for a user given a toDoListId.

/api/ToDoLists/{id}


***Endpoint:***

```bash
Method: GET
Type: 
URL: https://wise-to-do-api.herokuapp.com/api/ToDoLists
```



### 7. ToDoList Update


Updates a ToDoList for a user given a toDoListId.

/api/ToDoLists/{id}


***Endpoint:***

```bash
Method: PUT
Type: RAW
URL: https://wise-to-do-api.herokuapp.com/api/ToDoLists/1
```



***Body:***

```js        
{
    "Name": "Errands Updated"
}
```



### 8. ToDoList Create


Creates a ToDoList for a user.

/api/ToDoLists 



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://wise-to-do-api.herokuapp.com/api/ToDoLists
```



***Body:***

```js        
{
    "Name": "Groceries"
}
```



### 9. Account Login


Login a user.


***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://wise-to-do-api.herokuapp.com/Account/Login
```



***Query params:***

| Key | Value | Description |
| --- | ------|-------------|
| cache-control | no-cache |  |



***Body:***

```js        
{
    "Email": "email@gmail.com",
    "Password": "SomeSecurePassword123!"
}
```



### 10. Account Register


Register a user.


***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://wise-to-do-api.herokuapp.com/Account/Register
```



***Body:***

```js        
{
    "Email": "email@gmail.com",
    "Password": "SomeSecurePassword123!"
}
```



---
[Back to top](#todoapi)
> Made with &#9829; by [thedevsaddam](https://github.com/thedevsaddam) | Generated at: 2020-04-06 12:11:34 by [docgen](https://github.com/thedevsaddam/docgen)
