using System;
using GameStore.Dtos;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class GenerMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
