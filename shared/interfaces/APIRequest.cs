public class APIRequest
{
    public required string BaseUrl { get; set; }
    public required string Url { get; set; }
    public string? Body { get; set; }
    public string? Params { get; set; }
    public string? Token { get; set; }
}