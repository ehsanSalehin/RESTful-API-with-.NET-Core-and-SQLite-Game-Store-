namespace GameStore.Dtos;

public record class GameDto(int Id, string Name, string Gener, decimal Price, DateOnly ReleaseDate);
