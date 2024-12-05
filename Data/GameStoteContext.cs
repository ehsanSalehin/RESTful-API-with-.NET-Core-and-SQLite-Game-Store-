using System;
using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext>options) : DbContext(options)
{
    //Set<Game> => initial value
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre>  Genres => Set<Genre>();

    //as soon as migration execute, this code gonna execute too
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new{Id=1, Name = "Action"},
            new{Id=2, Name = "Adventure"},
            new{Id=3, Name = "Sports"},
            new{Id=4, Name = "Kids"}
        );
    }
}
