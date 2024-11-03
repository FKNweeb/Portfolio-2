using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.FunctionBasedModels;

[Keyless]
public class FindCoPlayersResults
{
    public string nconst { get; set; }
    public string p_name { get; set; }
    public int frequency { get; set; }
}
