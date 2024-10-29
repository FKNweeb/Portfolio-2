using WebApi.DTO.NameDtos;
using WebApi.Models.NameRelatedModels;

namespace WebApi.Mappers;

public static class NameMapper
{
    public static GetAllNameDTO ToGetAllNameDTO(this Name nameObject)
    {
        return new GetAllNameDTO
        {
            Name = nameObject.NameId,
            BirthYear = nameObject.BirthYear,
            DeathYear = nameObject.DeathYear
        };
    }
}
