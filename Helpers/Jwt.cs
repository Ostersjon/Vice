namespace Venna.Helpers;

public class Jwt
{
    public string Audiance { get; set; }=string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public int Expires { get; set; }
}