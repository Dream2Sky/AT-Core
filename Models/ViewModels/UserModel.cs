namespace AT_Core.Models.ViewModels
{
    public class AddUserInput
    {
        public string UserName { get; set; }
    }

    public class GetUserOutput
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int DepartId { get; set; }
        public string DepartName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDisabled { get; set; }
    }

    public class ResetPwdInput
    {
        public int UserId { get; set; }
        public string OldPwd { get; set; }
        public string NewPwd { get; set; }
        public string ConfirmPwd { get; set; }
    }
}