# Web API technical test

In reality I would create a sprint (preferably in Jira) with 1 story and mulitple tasks.  Story will be referenced as api-1 and subsequent tasks will be api-2, api-3 and api-4. These will be referenced in git commits

Master branch will be created and then a feature branch called dev/api-1.

## Solution notes for developer

- TBC

## Domain Specific Language

- TBC

## Tests

- Unit 
- Integration

## Sprint

### Story (api-1)

- To develop a ASP.NET WebAPI to persist and return user related information.
- Create sprint tasks

### Sub-tasks 

#### (api-2)

- Create RESTful API for create, update & retrieval
- Model
  - Firstname (PII) - VARCHAR 50
  - Surname (PII) - VARCHAR 50
  - EMail (PII) - VARCHAR 320 [rfc3696](http://tools.ietf.org/html/rfc3696)
  - Password (Hashed) - VARCHAR 128
- Create Unit tests
- Persist to DB (MySQL)

#### (api-3)

- Create script to build and package in container
- Create docker compose file to spin up container with api + Db
- Document how API can be run locally using docker and include sample web requests (integration tests or manual)
- Ensure easy to follow instructions so container is spun up/down and endpoints are hit

#### (api-4)

_Include considerations in README (this) file._

- Consider how API can be run in a production environment
- Consider how a large volume of requests, including concurrent creation and update operations
- Consider continuous operation in the event of problems around reading and writing from the DB
- Consider how to ensure security of the user information



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

Going to use MySQL. In reality, based on spec and the type of data (what I consider to be a list and non relational), I would opt for NoSQL engine and initially partition on first letter of Surname.  I would always opt for a managed cloud service. Other considerations will be HA and configurating solution over multiple cloud providers if you need to go to that extreme.  I would definitely configure over multiple availability zones for minimum HA, but would push for cross region HA and well practices DR procedures & drills.