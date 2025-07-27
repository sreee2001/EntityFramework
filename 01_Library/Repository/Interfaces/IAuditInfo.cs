using System;

namespace Repository.Interfaces
{
    /// <summary>
    /// Interface for audit information. Used to track who created or modified an entity and when.
    /// </summary>
    public interface IAuditInfo
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
