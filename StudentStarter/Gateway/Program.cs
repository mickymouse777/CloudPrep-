using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("function", client => client.BaseAddress = new Uri(builder.Configuration["FunctionUrl"]!));

// TODO 1: Read Jwt:Key from environment/configuration. Reject missing or short keys.
// TODO 2: Configure AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(...)
// Validate issuer, audience, signing key and lifetime; set ClockSkew to zero.
// TODO 3: AddAuthorization() with an AdminOnly role policy.

var app = builder.Build();
app.UseSwagger(); app.UseSwaggerUI();
// TODO 4: Add UseAuthentication() BEFORE UseAuthorization().

app.MapPost("/api/auth/login", (LoginRequest request, IConfiguration config) =>
{
    // TODO 5: Check demo username "admin" and DEMO_PASSWORD from environment.
    // TODO 6: Create JWT containing Name and Role=Admin, signed with HMAC SHA256.
    return Results.Problem("TODO: implement login", statusCode: 501);
});

app.MapPost("/api/tasks", async (WorkRequest request, IHttpClientFactory factory) =>
{
    // TODO 7: Forward request to POST /api/functions/process using named HttpClient.
    return Results.Problem("TODO: call local function", statusCode: 501);
}); // TODO 8: Require authorization with AdminOnly policy.

app.Run();
record LoginRequest(string Username, string Password);
record WorkRequest(string Task);
