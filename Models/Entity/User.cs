using System.ComponentModel.DataAnnotations;

namespace AT_Core.Models.Entity
{
    public class User:EntityBase
    {
        /// <summary>
        /// department id
        /// </summary>
        /// <value></value>
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}