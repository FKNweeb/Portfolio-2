using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace WebApi.DTO.TitleDtos;


[Keyless]
public class SearchResult
{
    
    public string tconst { get; set; }

    
    public string primary_title { get; set; }

    public override string? ToString()
    {
        return $"tconst: {tconst} || Title: {primary_title}";
    }
}
