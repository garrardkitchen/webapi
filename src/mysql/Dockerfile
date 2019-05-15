FROM mysql:5.7.26

MAINTAINER garrardkitchen@gmail.com

ENV MYSQL_DATABASE=WebApiDB \
    MYSQL_ROOT_PASSWORD=password

ADD schema.sql /docker-entrypoint-initdb.d

EXPOSE 3306
