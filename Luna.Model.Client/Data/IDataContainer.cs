using System;

namespace Luna.Data.Storage
{
    /// <summary>
    /// Interface for data containers.
    /// </summary>
    public interface IDataContainer
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

        /// <summary>
        /// Container schema version.
        /// </summary>
        decimal SchemaVersion { get; }

        /// <summary>
        /// Container connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Upgrade container schema to the specified version.
        /// </summary>
        /// <param name="newVersion">The new version.</param>
        void UpgradeToVersion(decimal newVersion);
    }
}