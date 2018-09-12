namespace AT_Core.Common
{
    public class ATConst
    {
        public const string AuthUserSessionKey = "CurrentAuthUser";
        private static string defaultPwd;
        public static string DefaultPwd
        {
            get {
                if(string.IsNullOrWhiteSpace(defaultPwd))
                {
                    defaultPwd = EncryptUtils.GetMD5("888888");
                }
                return defaultPwd;
            }
        }
        
    }
}