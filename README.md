# Web API technical test

In reality I would create a sprint (preferably in Jira) with 1 story and mulitple tasks.  Story will be referenced as api-1 and subsequent tasks will be api-2, api-3 and api-4. These will be referenced in git commits

Master branch will be created and then a feature branch called dev/api-1.

## Solution notes for developer

All developed using macOS and not windows.

### Prerequisites

- .NET Core 2.2
- xUnit
- Moq
- Dapper

## Getting Started

This section covers the options available to getting up and running with the solution.  These options include using docker-compose and an IDE.

### Option 1 - docker-compose

To run the entire solution `cd <into repo root folder>` and follow these instructions:

To build all images for the containers enter:
```
docker-compose build
```

The above builds, tests and creates the images required for the solution.

There are 2 images that are built; `myapi` and `garrardkitchen/mysql:5.7.26`. The mysql image loads a default schema which can be found in the `/src/mysql/schema.sql` file.

Enter this command to run the entire solution from the command line:
```
cd <into repo root folder>
docker-compose up
```

Enter this command to stop/tear down the entire solution from the command line:

```
docker-compose down
```

### Option 2 - IDE

To buld and run an instance of MySQL in docker enter the following commands:

```
cd src/mysql
./build.sh
./run.sh
```

Open Users solution (`/Users.sln`) in Visual Studio 2019 or Rider. Select Users.Web as start up project then either run or debug project.

To test the RESTful API, please use the 3 endpoints as shown in the `POSTMAN` section below.

There are 2 test projects located in the `/tests` folder.  These test the API (done) and the other the HTTP API endpoints (TODO).

If you want to run from the command line, outside of an IDE, please run the following:

```
cd <into repo root folder>
./build.sh
./run.sh
```

## POSTMAN

This sections includes the HTTP API endpoints to call to test the 3 use cases (insert, update and read).

To add user details:

```json
GET http://localhost:5000/api/user/add
{
  "firstname": "Garrard",
  "surname": "Kitchen",
  "email": "garrardpkitchen@yahoo.co.uk",
  "password" : "ABCD"
}
```

To update user details:

```json
GET http://localhost:5000/api/user/update
{
  "firstname": "Garrard",
  "surname": "Kitchen",
  "email": "garrardkitchen@gmail.com",
  "password" : "EFGH"
}
```

To obtain user details for email **garrardkitchen@gmail.com**:

```http
GET http://localhost:5000/api/user/garrardkitchen@gmail.com
```

## Domain Specific Language

- TBC

## Sprint

### Story (api-1)

- To develop a ASP.NET WebAPI to persist and return user related information.
- Create sprint tasks

### Sub-tasks 

#### (api-2)

- Create RESTful API for create, update & retrieval using different routes - done
  - get = GET /api/user
  - insert = POST /api/add
  - update = PUT /api/update
- Model - done
  - Firstname (PII) - VARCHAR 50
  - Surname (PII) - VARCHAR 50
  - EMail (PII) - VARCHAR 320 [rfc3696](http://tools.ietf.org/html/rfc3696)
  - Password (Hashed) - VARCHAR 128
- Create Unit tests - done
- Persist to DB (MySQL) - done

#### (api-3)

- Create script to build and package in container - done
- Create docker compose file to spin up container with api + Db - done
- Document how API can be run locally using docker and include sample web requests (integration tests or manual) - done
- Ensure easy to follow instructions so container is spun up/down and endpoints are hit - done

#### (api-4)

_Include considerations in README (this) file._

- Consider how API can be run in a production environment
- Consider how a large volume of requests, including concurrent creation and update operations
- Consider continuous operation in the event of problems around reading and writing from the DB
- Consider how to ensure security of the user information

_Please see `Considerations` section below_


### Notes

- commands used to create solution coded structure

```bash
dotnet new sln -n users
dotnet new webapi -n Users.Web -o src//Users.Web
dotnet new classlib -n Users.Api -o src//Users.Api
dotnet new classlib -n Users.Shared -o src//Users.Shared
dotnet new xunit -n Users.Web.Tests -o tests//Users.Web.Tests
dotnet new xunit -n Users.Api.Tests -o tests//Users.Api.Tests
dotnet sln users.sln add **/*.csproj
```

- Db Choice for test

Going to use MySQL. In reality, based on spec and the type of data (what I consider to be a list and non relational), I would opt for NoSQL engine and initially partition on first letter of Surname.  I would always opt for a managed cloud service. Other considerations will be HA and configuring solution over multiple cloud providers if you need to go to that extreme.  I would definitely configure over multiple availability zones for minimum HA, but would push for cross region HA and well practices DR procedures & drills.

- Run MySQL instance using docker

```docker
docker run --name my-sql --rm -e MYSQL_ROOT_PASSWORD=password -p 3306:3306 -d mysql:5.7.26
```

## Considerations

### Consider how API can be run in a production environment

- CI/CD 
- Include observability (logs, metrics and tracing)
- Hook up to remote API solution to test availability and response times
- Use different permissions (thinking of AWS IAM) to those used for dev/staging environments
- Use different environment variable values in dockerfile
- Load balance RESTful API over multiple instances
- Scale based on demand
- Canary releases
- Ensure redirect to Https endpoints

### Consider how a large volume of requests, including concurrent creation and update operations

- Autoscale
- Change code to be async from being a sync
- Possibly break RESTful API up into approapriate silos (microservices) based on demand and or responsibility (CQRS)
- Ensure host instances are configured for high IOPs for DB access and ensure instances are close (thinking of AWS placement groups)
- Use Redis and use write through pattern to write changes onto DB 

### Consider continuous operation in the event of problems around reading and writing from the DB

- Configure DB for HA, use read replicas
- Recycle DB password on regular basis
- Failover to different region, minimum, configure for different Availbility Zone
- Rearchitect for loosely coupling using messaging service (thinking of SQS or RabbitMQ)
- Mechanism to update customers of system degredation (thinking of statuspage.io)
- Monitor servers & services (thinking of datadog)
- Runbooks

### Consider how to ensure security of the user information

- Appropriate Permissions to invoke and permissions to operate (Least Privilege - POLP)
- Use hashed password
- Mask PII fields
- Regularly recycle DB password
- Use an appropriate Key Management Service for database encryption
- Encrypt data as rest (database storage)
- Use Database Encryption option
- Use TLS for HTTP data in transit
- Don't use live data in dev/test environment
- Ensure infrastructure is securely configured (e.g. DB not accessible from public internet)

### Other

- Regular database backups
- Well practiced DR plan

## Release Notes

0.1.0 RESTful API that accepts requests to insert, update & read user information.