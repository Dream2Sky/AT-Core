namespace AT_Core.Models.Entity
{
    public class User:EntityBase
    {
        /// <summary>
        /// department id
        /// </summary>
        /// <value></value>
        public int DepartmentId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}