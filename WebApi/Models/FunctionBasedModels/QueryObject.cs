namespace WebApi.Models.FunctionBasedModels;

public class QueryObject
{
    public IList<string> keywords { get; set; } = new List<string>();
    public string? SearchType { get; set; }
}
