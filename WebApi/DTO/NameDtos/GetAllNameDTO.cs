using System;
using System.Security;
using WebApi.Models.TitleRelatedModels;
using WebApi.Models.UserRelatedModels;

namespace WebApi.DTO.NameDtos;

public class GetAllNameDTO
{
    public string NameId { get; set; }
    public string Name { get; set; }
    public string BirthYear { get; set; }
    public string DeathYear { get; set; }

    public List<string> KnownForTitles { get; set; } = new List<string>();

    public List<string> Professions { get; set; } = new List<string>();

    public LocalNameRating? LocalNameRatings { get; set; } 
    
}
