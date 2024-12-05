using System;
using GameStore.Data;
using GameStore.Dtos;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.EndPoints;

public static class GameEndpoints
{

    const string GetGameEndPoint = "GetGame";

    //we gonna return WebApplication type and call this method name MapGameEndPoints
    //this => make this an extention metho
    public static WebApplication MapGameEndPoints(this WebApplication app)
    {
        //GET / games
        app.MapGet("games", async(GameStoreContext dbContext) => await dbContext.Games.Include(game => game.Genre).Select(Game =>Game.ToDto()).AsNoTracking().ToListAsync());


        //GET / games / with (id mumber)
        app.MapGet("games/{id}", async(int id, GameStoreContext dbContext) =>
        {
            //we could either recive a game or null => GameDto?
            Game? game = await dbContext.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(game.ToDetailsDto());
        }).WithName(GetGameEndPoint);



        //POST => CRAETE A NEW GAME
        app.MapPost("games/", async(CreateDto newGame, GameStoreContext dbContext) =>
        {
            // we cn use this way to make sure the user has enter the name for game or chagne Dto=>[Required][StringLength(50)]
            /*if(string.IsNullOrEmpty(newGame.Name))
            {
                return Results.BadRequest("Name is Required");
            }*/
            // now we gonna inject our GameStoreContext dbContext 
            //now after injections we add GameMapping file

            Game game = newGame.ToEntity();
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
            //game is our payload (actual data sent in an API request)
            return Results.CreatedAtRoute(GetGameEndPoint, new { id = game.Id }, game.ToDetailsDto());
            //WithParameterValidation => for cheking Dto [required],....
        }).WithParameterValidation();


        //PUT => update the game 
        app.MapPut("games/{id}", async(int id, UpdateDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            //see if the game exist or not
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }).WithParameterValidation();;


        //DELETE
        app.MapDelete("games/{id}", async(int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game=>game.Id==id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return app;
    }
}
