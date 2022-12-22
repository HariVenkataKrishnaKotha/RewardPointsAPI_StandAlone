# Reward Points API

This is a standalone .NET 6.0 API that calculates reward points for a retailer's customers based on their transactions.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet-core/3.1)

### Installing

1. Clone the repository:

```
git clone https://github.com/HariVenkataKrishnaKotha/RewardPointsAPI_StandAlone.git
```

2. Navigate to the project directory:

```
cd RewardPointsAPI_StandAlone
```

3. Restore the NuGet packages:

```
dotnet restore
```

4. Run the API:

```
dotnet run
```

The API will be running at `https://localhost:7161/swagger/index.html`.

## API Endpoints

The API has the following endpoints:

### `GET /api/transactions`

Returns a list of all transactions.

### `GET /api/transactions/rewardpoints`

Returns a list of reward points earned by customers in a given month.

#### Query Parameters

- `customerName`: (optional) The name of the customer.
- `monthName`: (optional) The name of the month.

### `GET /api/transactions/rewardpoints/total`

Returns a list of total reward points earned by customers.

#### Query Parameters

- `customerName`: (optional) The name of the customer.

## Configuration

The API reads the list of transactions from the `Transactions` section of the `appsettings.json` file. The transactions are in the following format:

```json
"Transactions": [
    {
      "Customer": "Hari Kotha",
      "Month": 1,
      "Amount": 120
    },
    {
      "Customer": "Alex Jones",
      "Month": 2,
      "Amount": 150
    },
    {
      "Customer": "Brian Paul",
      "Month": 1,
      "Amount": 36
    },
]
```
## Logging

The API logs requests and errors using the `Microsoft.Extensions.Logging` package. The logs are written to a text file in the `Logs` folder. The logging configuration can be modified in the `Logging` section of the `appsettings.json` file.

# Invoking the API

## Prerequisites

- [curl](https://curl.haxx.se/)

## Endpoints

### `GET /api/transactions`

Returns a list of all transactions.

```
curl http://localhost:7161/api/transactions
```

### `GET /api/transactions/rewardpoints`

Returns a list of reward points earned by customers in a given month.

#### Query Parameters

- `customerName`: The name of the customer.
- `monthName`: The name of the month.

```
curl "http://localhost:7161/api/transactions/rewardpoints?customerName=Hari Kotha&monthName=January"
```

### `GET /api/transactions/rewardpoints/total`

Returns a list of total reward points earned by customers.

#### Query Parameters

- `customerName`: The name of the customer.

```
curl "http://localhost:7161/api/transactions/rewardpoints/total?customerName=Hari Kotha"
```

## Additional Configuration

The API is configured to read the transactions data from the `Transactions` section of the `appsettings.json` file. You can modify the transactions data in this file to test different scenarios.

## Running the Tests

The API has unit tests for the controllers and the `CalculateMonthlyRewardPoints` helper method. To run the tests, navigate to the project directory and run the following command:

```
dotnet test
```

## Built With

- [.NET 6.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6) - The web framework used
- [NuGet](https://www.nuget.org/) - Dependency Management
- [NUnit](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-nunit) - Unit Tesing Framework

## Transaction Data

The API reads the transaction data from the `Transactions` section of the `appsettings.json` file. Here is the transaction data in a table format:

| Customer       | Month | Amount |
|----------------|-------|--------|
| Hari Kotha     | 1     | 120    |
| Hari Kotha     | 1     | 80     |
| Hari Kotha     | 2     | 200    |
| Alex Jones     | 2     | 150    |
| Alex Jones     | 3     | 90     |
| Alex Jones     | 3     | 60     |
| Brian Paul     | 1     | 36     |
| Brian Paul     | 3     | 135    |
| Brian Paul     | 3     | 98     |
## Example Output

Here are examples of the output for each endpoint in a table format:

### `GET /api/transactions`

| Customer       | Month | Amount |
|----------------|-------|--------|
| Hari Kotha     | 1     | 120    |
| Hari Kotha     | 1     | 80     |
| Hari Kotha     | 2     | 200    |
| Alex Jones     | 2     | 150    |
| Alex Jones     | 3     | 90     |
| Alex Jones     | 3     | 60     |
| Brian Paul     | 1     | 36     |
| Brian Paul     | 3     | 135    |
| Brian Paul     | 3     | 98     |

### `GET /api/transactions/rewardpoints`

| Month | Customer       | Points |
|-------|----------------|--------|
| 1     | Hari Kotha     | 120    |
| 2     | Hari Kotha     | 250    |
| 3     | Hari Kotha     | 0      |
| 1     | Alex Jones     | 0      |
| 2     | Alex Jones     | 150    |
| 3     | Alex Jones     | 50     |
| 1     | Brian Paul     | 0      |
| 2     | Brian Paul     | 0      |
| 3     | Brian Paul     | 168    |

### `GET /api/transactions/rewardpoints/total`

| Customer       | Points |
|----------------|--------|
| Hari Kotha     | 370    |
| Alex Jones     | 200    |
| Brian Paul     | 168    |

# Containerizing the API with Docker

The API can be easily containerized using Docker. Follow these steps to build and run the Docker image:

1. Install Docker on your system.
2. Navigate to the root directory of the project.
3. Build the Docker image using the following command:

```
docker build -t rewardpointsapistandalone .
```

4. Start the container using the following command:

```
docker run -p 80:80 rewardpointsapi
```

This will start the container and expose the API on port 80. You can then access the API using a web browser or a tool like `curl`.

To stop the container, run the following command:

```
docker stop <CONTAINER_ID>
```
# Accessing the Swagger UI

The API documentation can be accessed via the Swagger UI. To view the Swagger UI, follow these steps:

1. Start the API using the instructions provided in the previous sections.
2. Open a web browser and navigate to the base URL of the API.
3. Append `/swagger` to the base URL and press Enter.

For example, if the base URL of the API is `http://localhost:7161`, the Swagger UI can be accessed at `http://localhost:7161/swagger`. This is used in the above API.

The Swagger UI displays the API documentation and allows you to test the API endpoints.

## Output Images

### `GET /api/Transactions`
![image](https://user-images.githubusercontent.com/31946053/209176413-a00ec4d5-0dc5-4935-8ae2-7e19090cb546.png)

### `GET /api/Transactions/rewardpoints`
![image](https://user-images.githubusercontent.com/31946053/209177502-c19e8a7a-935c-4bf5-aef7-dc5dd869d509.png)

### `GET /api/Transactions/rewardpoints/total`
![image](https://user-images.githubusercontent.com/31946053/209177721-eb161610-681b-4618-a771-438fd6697b3c.png)

### `GET /api/Transactions/health`
![image](https://user-images.githubusercontent.com/31946053/209177954-e4e393cb-8deb-4af8-a391-96dd06f89690.png)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
