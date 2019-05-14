docker rm -f $(docker ps -a | grep my-sql | awk '{print $1}')
docker run --name my-sql -e MYSQL_ROOT_PASSWORD=password -p 3306:3306 -d garrardkitchen/mysql:5.7.26