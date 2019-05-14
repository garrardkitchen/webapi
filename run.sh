#!/bin/bash

# tear down
docker rm -f $(docker ps -a | grep my-sql | awk '{print $1}')
docker rm -f $(docker ps -a | grep myapi | awk '{print $1}')

# run mysql
docker run --name my-sql --rm -e MYSQL_ROOT_PASSWORD=password -p 3306:3306 -d garrardkitchen/mysql:5.7.26

# build mysql connection
ip=$(docker inspect -f '{{.NetworkSettings.IPAddress }}' my-sql)
mysql_conn="server=$ip;uid=garrard;pwd=password;database=WebApiDB"

# run api
docker run --name myapi -p 5000:5000 -p 5001:5001 -e MYSQL_CONN=$mysql_conn -d mywebapi:1.0

