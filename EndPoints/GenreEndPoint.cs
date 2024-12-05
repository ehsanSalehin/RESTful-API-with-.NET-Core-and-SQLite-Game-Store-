using System;
using GameStore.Data;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.EndPoints;

public static class GenreEndPoint
{
    public static WebApplication MapGenresEndPoints(this WebApplication app)
    {
        app.MapGet("genre", async(GameStoreContext dbContext)=>await dbContext.Genres.Select(genre=>genre.ToDto()).AsNoTracking().ToListAsync());
        return app;
    }

}
