
# Event Managment WebAPI

## Main Functions

* Allow users to create events, specifying the name, description, date and time, location, and maximum attendee capacity.
* Display a list of all created events, with basic information like name, date, and location, so users can browse them and decide which one to attend.
* Allow users to register for a particular event, keeping track of the attendees and the number of places available.
* Display a form for users to submit questions or comments to the event organizer.
* Allow the event organizer to edit event details at any time.
* Send automatic reminders to registered users for an event, a few days before the scheduled date.
* Allow users to search for events by name, date, or location.
* Allow users to follow a particular organizer to receive updates on their upcoming events.
* Show a list of popular or featured events so users can discover new events of interest to them.
* Allow users to bookmark events so they can be easily accessed later.
* Keep track of events a user has attended in the past, along with their registration and attendance history.
* Allow organizers to create discounts or promotions for a particular event, and send promotional codes to registered users.

# API Reference
## Account
#### Register a User

```http
  POST /Account/Register
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `username`| `string` | **Required**. User Name |
| `email`| `string` | **Required**. User email |
| `password`| `string` | **Required**. Password |
| `role`| `string` | **Required**. user/ admin |


#### Sign In

```http
  POST Account/Login
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `email`| `string` | **Required**. User email |
| `password`| `string` | **Required**. Password |

#### Renew Token

```http
  POST Account/RenewToken
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `email`| `string` | **Required**. User email |


## Coupon

#### Add Coupon

```http
  POST /Coupon
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `Description`      | `string` | **Required**. Coupon Description|
| `Code`      | `string` | **Required**. Coupon Code |
| `DiscountPercentage`      | `int` | **Required**. Discount Percentage 0%-100% |
| `EventId`      | `int` | **Required**. Relationship to Event |


#### Update Coupon

```http
  PUT /Coupon/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `Description`      | `string` | **Required**. Coupon Description|
| `Code`      | `string` | **Required**. Coupon Code |
| `DiscountPercentage`      | `int` | **Required**. Discount Percentage 0%-100% |
| `EventId`      | `int` | **Required**. Relationship to Event |

#### Get all Coupons

```http
  GET /Coupon/GetAll
```

#### Get Coupon

```http
  GET /Coupon/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |



#### Get list of tickets using a Coupon

```http
  GET /Coupon/Tickets/${CouponId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `CouponId`      | `int` | **Required**. Id of item to fetch |

#### Get coupon by Code

```http
  GET /Coupon/Code/${code}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `code`      | `string` | **Required**. Code of coupon to fetch |


#### Delete Coupon

```http
  DELETE /Coupon/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |


## Event

#### Add Event

```http
  POST /Event
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `Name`      | `string` | **Required**. Event Name|
| `Description`      | `string` | **Required**. Event Description|
| `TicketPrice`      | `decimal` | **Required**. Ticket Price|
| `EventCapacity`      | `int` | **Required**. Event Capacity|
| `date`      | `DateTime` | **Required**. Event Date |
| `AdminId`      | `int` | **Required**. Event Administrator |
| `LocationId`      | `int` | **Required**. Event Location |

#### Update Event

```http
  PUT /Event/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `Name`      | `string` | **Required**. Event Name|
| `Description`      | `string` | **Required**. Event Description|
| `TicketPrice`      | `decimal` | **Required**. Ticket Price|
| `EventCapacity`      | `int` | **Required**. Event Capacity|
| `date`      | `DateTime` | **Required**. Event Date |
| `AdminId`      | `int` | **Required**. Event Administrator |
| `LocationId`      | `int` | **Required**. Event Location |

#### Get all Event

```http
  GET /Event/GetAll
```

#### Get Event

```http
  GET /Event/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |


#### Get Event by Name

```http
  GET /Event/Name/${name}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | **Required**. Name of Event to fetch |

#### Get list of Events by Date

```http
  GET /Event/Date/${date}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `date`      | `string` | **Required**. Date of Event to fetch |

#### Get list of Events by Location

```http
  GET /Event/Location/${LocationId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `LocationId`      | `int` | **Required**. LocationId of Event to fetch |


#### Get list of Event Coupons

```http
  GET /Event/Tickets/${EventId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `EventId`      | `int` | **Required**. Id of item to fetch |


#### Get list of Form Responses of Event

```http
  GET /Event/Forms/${EventId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `EventId`      | `int` | **Required**. Id of item to fetch |


#### Get list of Sold Tickets of Event

```http
  GET /Event/Tickets/${EventId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `EventId`      | `int` | **Required**. Id of item to fetch |

#### Get list popular Events

```http
  GET /Event/Popular
```

#### Delete Event

```http
  DELETE /Event/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |



## Favourite

#### Add Favourite

```http
  POST /Favourite
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `userId`      | `int` | **Required**. User's Id|
| `eventId`      | `int` | **Required**. Event's Id |

#### Update Favourite

```http
  PUT /Favourite/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `userId`      | `int` | **Required**. User's Id|
| `eventId`      | `int` | **Required**. Event's Id |


#### Get all Users Favourites

```http
  GET /Favourites/GetAll
```

#### Get Favourite

```http
  GET /Favourite/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |


#### Delete Favourite

```http
  DELETE /Favourite/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |



## Follow


#### Add Follow

```http
  POST /Favourite
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `userId`      | `int` | **Required**. User's Id|
| `adminId`      | `int` | **Required**. Administrator's Id |



#### Update Follow

```http
  PUT /Follow/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `userId`      | `int` | **Required**. User's Id|
| `adminId`      | `int` | **Required**. Administrator's Id |


#### Get all Follows

```http
  GET /Follow/GetAll
```

#### Get Follow

```http
  GET /Follow/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |



#### Delete Follow

```http
  DELETE /Follow/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |




## Form
#### Add Form

```http
  POST /Form
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `comment`      | `string` | **Required**. Form Response|
| `userId`      | `int` | **Required**. User Id |
| `eventId`      | `int` | **Required**. Event Id |


#### Update Form

```http
  PUT /Form/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `comment`      | `string` | **Required**. Form Response|
| `userId`      | `int` | **Required**. User Id |
| `eventId`      | `int` | **Required**. Event Id |



#### Get all Form Responses

```http
  GET /Form/GetAll
```

#### Get Form Response

```http
  GET /Form/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |




#### Delete Form

```http
  DELETE /Form/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |



## Location

#### Add Location

```http
  POST /Location
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | **Required**. Location Name|
| `address`      | `int` | **Required**. Location address|
| `capacity`      | `int` | **Required**. Location capacity |


#### Update Location

```http
  PUT /Location/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | **Required**. Location Name|
| `address`      | `int` | **Required**. Location address|
| `capacity`      | `int` | **Required**. Location capacity |



#### Get all Locations

```http
  GET /Location/GetAll
```

#### Get Location

```http
  GET /Location/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |



#### Delete Location

```http
  DELETE /Location/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |




## Ticket

#### Add Ticket

```http
  POST /Ticket
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `userId`      | `int` | **Required**. Relationship to User|
| `eventId`      | `int` | **Required**. Relationship to Event|
| `couponCode`      | `int` | CouponCode |

#### Update Ticket

```http
  PUT /Ticket/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `userId`      | `int` | **Required**. Relationship to User|
| `eventId`      | `int` | **Required**. Relationship to Event|
| `couponCode`      | `int` | CouponCode |


#### Get all Tickets

```http
  GET /Ticket/GetAll
```

#### Get Ticket

```http
  GET /Ticket/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |





#### Delete Ticket

```http
  DELETE /Ticket/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |




## Upcoming Events
#### Get User´s upcoming Events (30 days)

```http
  GET /UpcomingEvents/${UserId}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `UserId`      | `string` | **Required**. Id of user to fetch |



## User
#### Add User

A user is created when an account is registered

#### Get all Users


```http
  GET /User/GetAll
```

#### Get Users

```http
  GET /User/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |



#### Get User Favourites

```http
  GET /User/Favourites/${Userid}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |

#### Get User Followers

```http
  GET /User/Followers/${Userid}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |


#### Get User Following

```http
  GET /User/Following/${Userid}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |


#### Get User Form Responses

```http
  GET /User/Forms/${Userid}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |


#### Get User Tickets

```http
  GET /User/Tickets/${Userid}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |


#### Change User Name

```http
  PATCH /User/ChangeUserName/${Userid}/${UserName}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |
| `UserName`      | `string` | **Required**. New UserName |


