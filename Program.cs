using Microsoft.EntityFrameworkCore;
using RestaurantSaaS.Models;
using RestaurantSaaS.Data;
var builder = WebApplication.CreateBuilder(args);

//Use of SQLite to ensure that anyone who clones the repository can get started immediately (no external configuration required)
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=prenotazioni.db"));

//Integration of Swagger to provide an interactive interface for testing and self-documenting APIs.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Implementation of endpoints using Minimal APIs, with injection into the DbContext
//Validation of business logic and use of HTTP status codes to ensure a clean RESTful architecture.
app.MapGet("/api/tables", async (AppDbContext db) =>
{
    var tables = await db.Tables.ToListAsync();
    return Results.Ok(tables); 
});

app.MapPost("/api/tables", async (Table newTable, AppDbContext db) =>
{
    db.Tables.Add(newTable);
    await db.SaveChangesAsync();
    return Results.Created($"/api/tables/{newTable.Id}", newTable);
});

app.MapPut("/api/tables/{id}",async (int id, Table updatedTable, AppDbContext db) =>
{
    var table = await db.Tables.FindAsync(id);

    if (table is null) return Results.NotFound();

    table.Seat = updatedTable.Seat;
    table.IsActive = updatedTable.IsActive;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/api/tables/{id}/toggle-status",async (int id, AppDbContext db) =>
{
   var table = await db.Tables.FindAsync(id);

   if (table is null) return Results.NotFound();

   table.IsActive = !table.IsActive;

   await db.SaveChangesAsync();

   return Results.Ok(table);
});

app.MapDelete("/api/tables/{id}", async (int id, AppDbContext db) =>
{
   var table = await db.Tables.FindAsync(id);

   if (table is null) return Results.NotFound(); 

   db.Tables.Remove(table);

   await db.SaveChangesAsync();

   return Results.Ok();
});

app.MapGet("/api/reservations", async (AppDbContext db) =>
{
   var reservations = await db.Reservations.ToListAsync();
   return Results.Ok(reservations); 
});

app.MapPost("/api/reservations", async (Reservation newReservation, AppDbContext db) =>
{
var table = await db.Tables.FindAsync(newReservation.TableId);

if (table is null)
    {
        return Results.BadRequest("Errore: Il tavolo richiesto non esiste nel ristorante.");
    }

if (table.IsActive == false)
    {
        return Results.BadRequest("Errore: Il tavolo selezionato è attualmente fuori servizio o già occupato.");
    }

if (table.Seat < newReservation.People)
    {
        return Results.BadRequest($"Errore di capienza: Il tavolo ha solo {table.Seat} posti,ma la prenotazione è per {newReservation.People} persone.");
    }

db.Reservations.Add(newReservation);
await db.SaveChangesAsync();

return Results.Created($"/api/reservations/{newReservation.Id}", newReservation);
});

app.MapPut("/api/reservations/{id}", async (int id, Reservation updatedReservation, AppDbContext db) =>
{
    var reservation = await db.Reservations.FindAsync(id);
    if (reservation is null) return Results.NotFound();

    var tableExists = await db.Tables.AnyAsync(t => t.Id == updatedReservation.TableId);

    if (!tableExists)
        {
            return Results.BadRequest("Impossibile modifiare: il nuovo tavolo indicato non esiste");
        }  

    reservation.CustomerName = updatedReservation.CustomerName;
    reservation.CustomerPhone = updatedReservation.CustomerPhone;
    reservation.ReservationTime = updatedReservation.ReservationTime;
    reservation.People = updatedReservation.People;
    reservation.TableId = updatedReservation.TableId;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/api/reservations/{id}", async (int id, AppDbContext db) =>
{
    var reservation = await db.Reservations.FindAsync(id);

    if (reservation is null) return Results.NotFound();

    db.Reservations.Remove(reservation);

    await db.SaveChangesAsync();

    return Results.Ok();

});

app.Run();

