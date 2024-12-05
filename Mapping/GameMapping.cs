using System;
using GameStore.Dtos;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class GameMapping
{
    //send to data base
    public static Game ToEntity(this CreateDto game)
    {
        return new Game()
        {
            Name = game.Name,
            GenerId = game.GenerId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

        public static Game ToEntity(this UpdateDto game, int id)
    {
        return new Game()
        {
            Id = id,
            Name = game.Name,
            GenerId = game.GenerId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }
    //send to Dto
    public static GameDto ToDto(this Game game)
    {
        return new(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            );
    }


        public static GameDetailsDto ToDetailsDto(this Game game)
    {
        return new(
                game.Id,
                game.Name,
                game.GenerId,
                game.Price,
                game.ReleaseDate
            );
    }
}
