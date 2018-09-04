namespace AT_Core.Results
{
    public class LoginResult
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool LoginState { get; set; }
        public bool IsAdmin { get; set; }
    }
}