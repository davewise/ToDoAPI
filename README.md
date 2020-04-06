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
1. Register an account and grab the JSON Web Token. (See ToDoAPI docs below)
2. Use the token in the Authorization: Bearer header
3. Don't forget to append the list id and item id where apropriate to the endpoint url (See ToDoAPI docs below)




# ToDoAPI

A RESTful API to support a to-do list application.

## Indices

* [Default](#default)

  * [Account Register](#1-account-register)
  * [Account Login](#2-account-login)
  * [ToDoList Create](#3-todolist-create)
  * [ToDoList Update](#4-todolist-update)
  * [ToDoLists Get](#5-todolists-get)
  * [ToDoList Delete](#6-todolist-delete)
  * [ToDoItem Create](#7-todoitem-create)
  * [ToDoItem Update](#8-todoitem-update)
  * [ToDoItems Get](#9-todoitems-get)
  * [ToDoItem Delete](#10-todoitem-delete)


--------


## Default



### 1. Account Register


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



### 2. Account Login


Login a user.


***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://wise-to-do-api.herokuapp.com/Account/Login
```



***Body:***

```js        
{
    "Email": "email@gmail.com",
    "Password": "SomeSecurePassword123!"
}
```



### 3. ToDoList Create


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



### 4. ToDoList Update


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



### 5. ToDoLists Get


Return a ToDoList for a user given a toDoListId.

/api/ToDoLists/{id}


***Endpoint:***

```bash
Method: GET
Type: 
URL: https://wise-to-do-api.herokuapp.com/api/ToDoLists
```



### 6. ToDoList Delete


Deletes a ToDoList for a user given a toDoListId.

/api/ToDoLists/{id}


***Endpoint:***

```bash
Method: DELETE
Type: 
URL: https://wise-to-do-api.herokuapp.com/api/ToDoLists/2
```



### 7. ToDoItem Create


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



### 8. ToDoItem Update


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



### 9. ToDoItems Get


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



### 10. ToDoItem Delete


Deletes a specific ToDoItem based on index for a user given a toDoListId.

/api/ToDoItems/{toDoListId}/{id}


***Endpoint:***

```bash
Method: DELETE
Type: 
URL: https://wise-to-do-api.herokuapp.com/api/ToDoItems/1/2
```



---
[Back to top](#todoapi)
> Made with &#9829; by [thedevsaddam](https://github.com/thedevsaddam) | Generated at: 2020-04-06 13:05:34 by [docgen](https://github.com/thedevsaddam/docgen)
