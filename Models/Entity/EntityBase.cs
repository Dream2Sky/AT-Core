using System;

namespace AT_Core.Models.Entity
{
    public class EntityBase
    {
        /// <summary>
        /// Entity Id
        /// </summary>
        /// <value></value>
        public long Id { get; set; }

        /// <summary>
        /// Is Disable
        /// </summary>
        /// <value></value>
        public bool IsDisable { get; set; }

        /// <summary>
        /// entity created time
        /// </summary>
        /// <value></value>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// entity disabled time
        /// </summary>
        /// <value></value>
        public DateTime DisableTime { get; set; }
    }
}