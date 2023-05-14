namespace Event_manager_API.DTOs.Auth
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationDate {  get; set; }
    }
}
