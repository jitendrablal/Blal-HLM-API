
namespace BlalApi.Models
{
    public class AuthModel
    {
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        //  public int[] MultiUserIds { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }

    }
}