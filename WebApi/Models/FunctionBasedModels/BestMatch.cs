using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.FunctionBasedModels;

[Keyless]
public class BestMatch
{
    public string tconst { get; set; }
    public int? rank { get; set; } 
    public string title { get; set; }
    
}
