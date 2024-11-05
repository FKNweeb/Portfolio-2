using WebApi.DTO.TitleDtos;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Interfaces;

public interface IGenreRepository
{
    int NumberOfGenre();

    Task<List<GenreWithTitles>> GetAllTitlesByGenre(int page, int pageSize);

    Task<List<GenreWithTitles>> GetTitlesBySpecificGenre(string genre, int page, int pageSize);


}
