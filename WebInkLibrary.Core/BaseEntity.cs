using System;

namespace WebInkLibrary.Core
{
    /// <summary>
    /// Node Instance table will inherit this class to make their schema
    /// </summary>
    public class BaseEntity
    {
        public int Id { get; set; }
        public int SrNo { get; set; }
        public bool Published { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public bool Deleted { get; set; }
    }
}
