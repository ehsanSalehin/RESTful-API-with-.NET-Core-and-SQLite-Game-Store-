using GameStore.Data;
using GameStore.EndPoints;

var builder = WebApplication.CreateBuilder(args);

//connection to sqlite
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);


var app = builder.Build();

app.MapGameEndPoints();
app.MapGenresEndPoints();

await app.MigrateDbAsync();

app.Run();
