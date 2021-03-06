﻿using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Loki.Common;

namespace Luna.Data.Local.SQLite
{
    public abstract class SQLiteContext<TContext> : DbContext where TContext : DbContext
    {
        #region Log

        private ILog log = null;

        private string loggerName = null;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILog Log
        {
            get
            {
                if (log == null)
                {
                    Interlocked.CompareExchange(ref log, GetLog(), null);
                }

                return log;
            }
        }

        /// <summary>
        /// Gets the logger name ; must be redefined in derived classes.
        /// </summary>
        protected virtual string LoggerName
        {
            get
            {
                if (loggerName == null)
                {
                    loggerName = this.GetType().FullName;
                }

                return loggerName;
            }
        }

        internal virtual ILog GetLog()
        {
            return Toolkit.Common.Logger.GetLog(LoggerName);
        }

        #endregion Log

        protected SQLiteContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<TContext>(null);
            Database.Log = Log.Debug;

            // Database.Log = Console.WriteLine;
        }

        public new async Task SaveChangesAsync()
        {
            Log.Debug("Saving changes");
            await base.SaveChangesAsync();
        }

        public virtual void SetState(object entity, EntityState state)
        {
            this.Entry(entity).State = state;
        }
    }
}