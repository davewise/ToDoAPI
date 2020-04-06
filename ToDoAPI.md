
# ToDoAPI

A RESTful API to support a to-do list application.

## Indices

* [Account](#account)

  * [Account Register](#1-account-register)
  * [Account Login](#2-account-login)

* [ToDoLists](#todolists)

  * [ToDoList Create](#1-todolist-create)
  * [ToDoList Update](#2-todolist-update)
  * [ToDoLists Get](#3-todolists-get)
  * [ToDoList Delete](#4-todolist-delete)

* [ToDoItems](#todoitems)

  * [ToDoItem Create](#1-todoitem-create)
  * [ToDoItem Update](#2-todoitem-update)
  * [ToDoItems Get](#3-todoitems-get)
  * [ToDoItem Delete](#4-todoitem-delete)


--------


## Account



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



## ToDoLists



### 1. ToDoList Create


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



### 2. ToDoList Update


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



### 3. ToDoLists Get


Return a ToDoList for a user given a toDoListId.

/api/ToDoLists/{id}


***Endpoint:***

```bash
Method: GET
Type: 
URL: https://wise-to-do-api.herokuapp.com/api/ToDoLists
```



### 4. ToDoList Delete


Deletes a ToDoList for a user given a toDoListId.

/api/ToDoLists/{id}


***Endpoint:***

```bash
Method: DELETE
Type: 
URL: https://wise-to-do-api.herokuapp.com/api/ToDoLists/2
```



## ToDoItems



### 1. ToDoItem Create


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



### 2. ToDoItem Update


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



### 3. ToDoItems Get


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



### 4. ToDoItem Delete


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
> Made with &#9829; by [thedevsaddam](https://github.com/thedevsaddam) | Generated at: 2020-04-06 13:58:18 by [docgen](https://github.com/thedevsaddam/docgen)
