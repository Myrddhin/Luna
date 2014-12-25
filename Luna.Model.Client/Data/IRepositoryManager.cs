using System;

namespace Luna.Data.Storage
{
    /// <summary>
    /// Interface for repositories managers.
    /// </summary>
    public interface IRepositoryManager
    {
        /// <summary>
        /// Creates a new database with the specified name.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        void Create(string databaseName);

        /// <summary>
        /// Opens the specified database name.
        /// </summary>
        /// <param name="databaseName">Name of the  database.</param>
        void Open(string databaseName);

        /// <summary>
        /// Saves the current database with the specified name.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        void SaveAs(string databaseName);

        /// <summary>
        /// Disconnect the current database.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Occurs when the repository connection changed (used to force client to recycle DbContexts).
        /// </summary>
        event EventHandler ConnectionChanged;

        decimal SchemaVersion { get; }

        string ConnectionString { get; }

        void UpgradeToVersion(decimal newVersion);
    }
}