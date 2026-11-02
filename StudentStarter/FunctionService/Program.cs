var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
// Pre-built local HTTP function: only accessible to the gateway on the Compose network.
app.MapPost("/api/functions/process", (WorkRequest request) => Results.Ok(new { message = $"Processed: {request.Task}", processedAt = DateTimeOffset.UtcNow }));
app.Run();
record WorkRequest(string Task);
