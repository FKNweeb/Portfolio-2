using WebApi.DTO.NameDtos;
using WebApi.Models.NameRelatedModels;

namespace WebApi.Mappers;

public static class NameMapper
{
    public static GetAllNameDTO ToGetAllNameDTO(this Name nameObject)
    {
        return new GetAllNameDTO
        {
            Name = nameObject.PrimaryName,
            BirthYear = nameObject.BirthYear,
            DeathYear = nameObject.DeathYear,
            KnownForTitles = nameObject.KnownForTitles?.Select(t=>t.Title.PrimaryTitle).ToList(),
            Professions = nameObject.ProfessionNames?.Select(p=>p.ProfessionTitle).ToList(),
        };
    }
}
