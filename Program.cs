/*
 * =================================================================================
 * C# Program.cs for a .NET 8 Minimal API to generate QR codes.
 *
 * To run this code:
 * 1. Create a new minimal API project:
 * dotnet new minimal -n QrCodeApi
 * 2. Navigate into the folder:
 * cd QrCodeApi
 * 3. Add the QRCoder NuGet package:
 * dotnet add package QRCoder
 * 4. Replace the contents of Program.cs with this code.
 * 5. Run the application:
 * dotnet run
 *
 * You can then access the Swagger UI at /swagger (e.g., http://localhost:5123/swagger)
 * and test the endpoint.
 * =================================================================================
 */

// Import all necessary namespaces
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QRCoder; // Required for QRCodeGenerator and PngByteQRCode

// 1. Create the WebApplicationBuilder
var builder = WebApplication.CreateBuilder(args);

// 2. Configure services
// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Build the WebApplication
var app = builder.Build();

// 4. Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enable Swagger and Swagger UI in development
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "QR Code API v1");
        options.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
    });
}

// 5. Define the API endpoints
app.MapGet("/generate", ([FromQuery] string? text) =>
{
    // 6a. Check if the text parameter is null or empty
    if (string.IsNullOrEmpty(text))
    {
        return Results.BadRequest("Text parameter is required.");
    }

    // 6b. Use QRCodeGenerator to create QRCodeData
    // We wrap in 'using' statements to ensure proper disposal
    using (var qrGenerator = new QRCodeGenerator())
    {
        var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

        // 6c. Use PngByteQRCode to generate the graphic
        using (var qrCode = new PngByteQRCode(qrCodeData))
        {
            // 6d. Get the byte array of the PNG image (20 pixels per module)
            byte[] qrCodeImage = qrCode.GetGraphic(20);

            // 6e. Return the image file
            return Results.File(qrCodeImage, "image/png");
        }
    }
})
.WithName("GenerateQrCode")
.WithSummary("Generate a QR Code")
.WithDescription("Generates a PNG image of a QR code for the provided text.")
.WithOpenApi(); // Ensure it's visible in OpenAPI/Swagger

// 6. Run the application
app.Run();