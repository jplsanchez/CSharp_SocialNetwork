
<< [[Projetos]]
Tags: #WebAPI, #Authentication, #Identity, #EntityFramework, #MassTransit , #RabbitMQ , #PostgreSQL

# API User Authentication


- [Open](file:///C:/Users/jpaul/source/repos/Projects/SocialNetwork_Angular_dotNet/Back-End/UserAuthentication)

|Project Content|
|---|
|Identity|
|Token JWT|
|Authentication|
|Entity Framework|
|PostgreSQL|
|MassTransit|
|RabbitMQ|


### Instruções

Para rodar é necessário:




- docker-compose do RabbitMQ e PostgreSQL
	- Diretório [[API User]]: 
~~~~ properties
	docker-compose up -d
~~~~



- Migrations no Nuget:
~~~~ properties
	update-database
~~~~
