.NET 8 QR Code Minimal API

A simple, fast, and lightweight .NET 8 Minimal API for generating QR code images on the fly. This project demonstrates the power of Minimal APIs and the usefulness of the QRCoder library.

Features

Fast Generation: Creates PNG QR code images from any text string.

Minimal & Modern: Built with .NET 8 Minimal APIs for high performance and low boilerplate.

Swagger/OpenAPI: Includes a built-in Swagger UI for easy API testing and documentation.

Lightweight: Small and easy to deploy to any service that hosts .NET applications.

Technologies Used

.NET 8

ASP.NET Core Minimal API

QRCoder: The core NuGet package used for generating the QR codes.

Swashbuckle.AspNetCore: The library that provides the Swagger documentation page.

Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing.

Prerequisites

You must have the .NET 8 SDK (or newer) installed on your machine.

Download .NET 8 SDK

Installation & Running

Clone the repository (or just place the Program.cs file in a new project folder).

# Create a new minimal API project (if starting from scratch)
dotnet new minimal -n QrCodeApi
cd QrCodeApi


Add the QRCoder Package:

dotnet add package QRCoder


(The Swashbuckle.AspNetCore package is included by default in new .NET 8 API templates).

Run the application:

dotnet run


Access the API:
Once running, the terminal will show the URLs it's listening on, typically:

http://localhost:5123

https://localhost:7123

API Usage

You can test the API using the built-in Swagger UI or by making a direct GET request.

1. Using the Swagger UI (Recommended)

Navigate to the /swagger route on your running application:
http://localhost:5123/swagger

You will see the interactive documentation for the /generate endpoint. You can enter a text string, click "Execute," and see the resulting QR code image directly on the page.

2. Using a Direct Request

Make a GET request to the /generate endpoint with your text in a query parameter.

Endpoint

GET /generate

Query Parameters

text (string, required): The text you want to encode into the QR code.

Example Request

You can test this right from your browser's address bar:
http://localhost:5123/generate?text=Hello+world

Or using a tool like curl:

curl "http://localhost:5123/generate?text=Hello+world" -o my-qrcode.png


Success Response

Code: 200 OK

Content-Type: image/png

Body: The binary data for the generated PNG image.

Error Response

If the text parameter is missing or empty, the API will return:

Code: 400 Bad Request

Body: (string) "Text parameter is required."
