using System;
using System.Security;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.DTO.NameDtos;

public class GetAllNameDTO
{
    public string nconst { get; set; }
    public string Name { get; set; }
    public string BirthYear { get; set; }
    public string DeathYear { get; set; }

    public List<string> KnownForTitles { get; set; } = new List<string>();

    public List<string> Professions { get; set; } = new List<string>();

    
}
