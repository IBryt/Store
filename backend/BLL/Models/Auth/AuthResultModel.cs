namespace IgorBryt.Store.BLL.Models.Auth;

public class AuthResultModel
{
    public string Token { get; set; }
    public bool Result { get; set; }
    public IEnumerable<String> Errors { get; set; }
}
