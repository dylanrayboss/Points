# Points

Points is a RESTful API for accessing/tracking points per payer.

## Table of Contents

- [Routes](#routes)
- [What is Needed](#what-is-needed)
- [How to run Application](#how-to-run-application)

## Routes

__Get All Payer Points Balances:__ Return all payer point balances.
```bash
curl --location --request GET '{{BASE_URL}}/api'
```
__Add Transaction:__ Adds a transaction for a specific payer and date.
```bash
curl --location --request POST '{{BASE_URL}}/api' \
--header 'Content-Type: application/json' \
--data-raw '{
    "payer": "DANNON",
    "points": 1000,
    "timestamp": "2020-11-02T14:00:00Z"
}'
```
__Spend Points:__ Spends points from the user's current balances.
```bash
curl --location --request PUT '{{BASE_URL}}/api' \
--header 'Content-Type: application/json' \
--data-raw '{
    "points": 5000
}'
```

## What is Needed 

1. [.NET Core 3.1](https://dotnet.microsoft.com/download)
   * Used to run the application.
2. [dotnet-ef](https://www.nuget.org/packages/dotnet-ef/)
   * Used for creating the database (if needed, a blank database file is provided).
3. [DB Browser for SQLite](https://sqlitebrowser.org/)
   * Used for viewing/editing database (if needed).
4. [Postman](https://www.postman.com/downloads/)
   * Used for submitting the requests.

## How to run Application

1. Install the frameworks/tools provided in the [What is Needed](#what-is-needed) section.
