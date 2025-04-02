using AimReactionAPI.Data;
using AimReactionAPI.Services;
using Microsoft.EntityFrameworkCore;
using Fleck;
using WebSocketBoilerplate;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader()
    ));

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register services
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<GameUserService>();
builder.Services.AddScoped<TargetService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<MultiplayerService>();
builder.Services.AddScoped(typeof(GameSessionHandler<>));

var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());
var app = builder.Build();

var multiplayerService = app.Services.GetRequiredService<MultiplayerService>();
var wsServer = new WebSocketServer("ws://0.0.0.0:8080");
wsServer.Start(ws =>
{
    int? userId = null;
    ws.OnOpen = async () =>
    {
        if (int.TryParse(ws.ConnectionInfo.Path.Trim('/'), out int id))
        {
            userId = id ;
            await multiplayerService.Connect(id, ws);
        }
    };
    ws.OnMessage = async message =>
    {
        try
        {
            await app.InvokeClientEventHandler(clientEventHandlers, ws, message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    };
    ws.OnClose = () =>
    {
        if (userId.HasValue)
        {
            multiplayerService.Disconnect(userId.Value);
        }
    };
});

// Ensure CORS is applied before routing and authentication
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();