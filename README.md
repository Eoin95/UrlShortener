# URL Shortener

A URL shortening webapp along the lines of TinyUrl or bit.ly

[TOC]



## Architecture

### Application Design

UrlShortener is composed of a React web app, a .Net 7 Application and a SQL database.

- UrlShortener WebApp - Written in React with Typescript
  - Uses Axios to send web requests; this is wrapped in a HttpClient class so it could be swapped out while only changing one class
  - Uses Tailwinds CSS for styling
- UrlShortener API - Written in .Net 7 
  - Two endpoints 
    - FindExistingUrl - A GET endpoint with a route parameter. This is used to find and return the long form URL that corresponds to the short URL passed in the route parameter, if it exists. If it does not find a URL, it returns a 404.
    - ShortenUrl - A POST endpoint that accepts a ShortenUrlRequest as a JSON body which contains a 'LongUrl' string property and validates that it has been passed a valid URL, returning a 401 otherwise. This first queries the UrlPair table to check for an existing pair, if one is found it will return it's short URL, otherwise it will generate a new unique id and insert this as part of a new UrlPair with the Long URL.
  - Entity Framework Core is used as the data access technology. This allows for rapid development as it removes the need to write SQL stored procedures or queries. The data operations in this application are not complex so there is no need for complex stored procedures.
  - [Nanoid](https://zelark.github.io/nano-id-cc/) is used to generate the short URL ID. It generates unique IDs based on a list of characters and a length. These are stored in AppSettings. Highly unlikely to cause a collision, with the settings configured in AppSettings it would take 120,000 years for a 1% chance.
- UrlShortener Database - A SQL database containing one table 'UrlPair', used to store Long URL and Short URL pairs.



## Dev Setup

### Creating the UrlShortener Database

In the UrlShortener\Database folder there are two SQL scripts

1. CreateUrlShortenerDb.Sql - Creates the UrlShortener database and UrlPair table
2. CreateUrlShortenerUser - Creates a sql user and login
   - This user is used by the UrlShortner API to access the database

### Starting the client

In the terminal run the following commands:

``` bash
cd UrlShortener\Client\urlshortener
npm install
...
npm start
```

The client should be open at  [http://localhost:3000](http://localhost:3000)

### Starting the Server

The application will be available at https://localhost:5001

#### Via Terminal

```bash
cd UrlShortener\Server\UrlShortener.API
dotnet build && dotnet run
```

#### Via Visual Studio

1. Open the UrlShortener.sln
2. Set UrlShortener.API as the Startup project
3. Click to run

