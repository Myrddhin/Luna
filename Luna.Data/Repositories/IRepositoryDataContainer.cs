using System;
using Luna.Data.Local.CRM;
using Luna.Data.Storage;

namespace Luna.Data.Local.Storage
{
    /// <summary>
    /// Interface for client data container.
    /// </summary>
    public interface IRepositoryDataContainer : IDataContainer
    {
        /// <summary>
        /// Gets the internal repository id.
        /// </summary>
        Guid InternalRepositoryId { get; }
    }
}