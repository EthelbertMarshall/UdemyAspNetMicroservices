# UdemyAspNetMicroservices

Micorservices Architecture and Implementation on .NET 5  Udemy Course implemented using .NET 6.0

Micorservices Architecture and Implementation on .NET 5 ( Github -- https://github.com/aspnetrun/run-aspnetcore-microservices)

--Catalog.Api
--Basket API
--Discount API
--Ordering API

Docker Commands to Execute is Provided against each DB / Service
====================================================================

--Mongodb 
	---( docker start shopping-mongo, docker ps -a ,docker run -d -p 27017:27017 --name shopping-mongo mongo ,  docker logs -f shopping-mongo,  docker exec -it shopping-mongo /bin/bash, mongosh ,use CatalogDB ,  db.createCollection('Products'), docker stop 43b1 , docker rmi 43b1,   docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d,   docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down,  docker run -d -p 3000:3000 mongoclient/mongoclient)
  
--Redis
	---( docker start aspnetrun-redis,  docker run -d -p 6379:6379 --name aspnetrun-redis redis,  docker logs -f aspnetrun-redis,  docker exec -it aspnetrun-redis /bin/bash , redis-cli , get test,  docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d,)
  
--portainer
	---( docker pull portainer/portainer-ce /  , http://localhost:9000/ )
  
--Postgresql
	---( "DiscountServer/DiscountDB" in PGAdmin ,docker pull dpage/pgadmin, http://localhost:5050/  url for PGAdmin , docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d)

--Docker
--Aggregator Pattern
--Domain Driven Microservices and CQRS

--Grpc
	-- Same PGAdmin and Postgresql  ,  Create Proto Buff, Create Service Class ,

--RabbitMQ
	---(
--ElasticSearch
	---(
--Kibana
	---(


--Repository Pattern
--Sync and Async Microservices Communication
--Portainer Tools (portainer.io)
--Health Checks Status
--Ocelot Gateways



--------- Mongo DB Commands--------------------------------

Documents and Collections in MongoDB-- Document is a Record/Row, Collection is a  Table

db.Products.insertMany( [
      { "Name": "Asus Laptop", "Category": "Computers" , "Summary": "Summary","Description": "Description", "ImageFile": "ImageFile", "Price": 54.93},
      { "Name": "HP Laptop", "Category": "Computers" , "Summary": "Summary","Description": "Description", "ImageFile": "ImageFile", "Price": 85.21},
      { "Name": "Dell Laptop", "Category": "Computers" , "Summary": "Summary","Description": "Description", "ImageFile": "ImageFile", "Price": 71.45},
   ] );

db.Products.find({})

db.Products.remove({})

show dbs

show collections


--------- Redis Commands--------------------------------

get key  , set key 

--------- Postgresql Commands--------------------------------


