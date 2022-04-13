
<< [[Projetos]]
Tags: #WebAPI, #CQRS, #MediaTR, #Dapper, #MySQL, #MongoDB, #EventSourcing, #Caching, #Decorator, #Docker, #RabbitMQ, #MassTransit

# API User
- [Open](file:///C:\Users\jpaul\source\repos\Projects\SocialNetwork_Angular_dotNet\Back-End\UserCQRS)


|Project Content|
|---|
|CQRS|
|MediaTR|
|Dapper|
|MongoDb|
|MySQL|
|Event Sourcing __IMPLEMENTAR__|
|Decorator Design Pattern|
|Caching|
|Docker|
|MassTransit|
|RabbitMQ|

---

### References

- [Support Article I - Balta.io](https://balta.io/blog/aspnet-core-cqrs-mediator)
- [Support Article II - Macoratti](https://www.macoratti.net/20/10/aspc_cqrsmedr1.htm)
- [Suport Article III - Eduardo Pires](https://www.eduardopires.net.br/2016/07/cqrs-o-que-e-onde-aplicar/)
- [Support Article IV - TreinaWeb](https://www.treinaweb.com.br/blog/mediator-pattern-com-mediatr-no-asp-net-core)
- [Event Sourcing (EasyNetQ/RabbitMQ) - brunobritto](https://www.brunobrito.net.br/asp-net-core-event-sourcing-microsservicos-background-services-rabbit/)

---

### Project Domain Map
- __Commands__: Actions DTOs;

- __EventHandlers__: Objects that receive event notifications;

- __Handlers__: Objects that receive Commands actions;

- __Models__: Program Entities;

- __Notifications__: Notification DTOs.


---


### Useful Operational Information

Clear Docker Volume and Run:

- ~~~~ properties
docker-compose down -v
docker-compose up -d


Create table for Dapper:

- ~~~~ SQL
USE  mysqldb;
CREATE TABLE User(
	Id varchar(36),
    Name varchar(255),
    Age int,
    Gender varchar(10),
    isEnabled bool,
    Created datetime
);


---

###  Architecture

![](../../resources/SN_User.jpg)