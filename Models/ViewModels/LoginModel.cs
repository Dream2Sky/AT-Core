namespace AT_Core.Models.ViewModels
{
    public class LoginInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginOutput
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool LoginState { get; set; }
        public bool IsAdmin { get; set; }
        public int DepartId { get; set; }
    }
}