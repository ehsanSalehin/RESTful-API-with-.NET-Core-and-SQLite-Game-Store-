using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos;

public record class UpdateDto(
    [Required][StringLength(50)] string Name,
    int GenerId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate);
