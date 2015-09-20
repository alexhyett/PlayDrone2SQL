
namespace PlayDrone2SQL
{
    using Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The logger.
    /// </summary>
    public class AppLogger : IWriter<App>, IReader
    {
        /// <summary>
        /// The writer.
        /// </summary>
        private readonly IWriter<App> writer;

        /// <summary>
        /// The reader.
        /// </summary>
        private IReader reader;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger log;

        public AppLogger(IWriter<App> writer, IReader reader, ILogger log)
        {
            this.writer = writer;
            this.reader = reader;
            this.log = log;
        }

        /// <summary>
        /// Logs at the start and end of an app save.
        /// </summary>
        /// <param name="app">
        /// The app to save.
        /// </param>
        public void Save(App app)
        {
            log.LogOperation(string.Format("Starting save of app: {0}.", app.app_id));
            writer.Save(app);
            log.LogOperation(string.Format("Finished save of app: {0}.", app.app_id));
        }

        /// <summary>
        /// Logs at the start and end of saving many apps.
        /// </summary>
        /// <param name="apps">
        /// A list of apps to save.
        /// </param>
        public void SaveMany(List<App> apps)
        {
            apps.ForEach(i => log.LogOperation(string.Format("Starting save of app: {0}.", i.app_id)));
            writer.SaveMany(apps);
            apps.ForEach(i => log.LogOperation(string.Format("Finished save of app: {0}.", i.app_id)));
        }

        /// <summary>
        /// Count the apps using the reader.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return reader.Count();
        }

        /// <summary>
        /// Get the id of the app from it's unique name.
        /// </summary>
        /// <param name="name">
        /// The name of the app.
        /// </param>
        /// <returns>
        /// The unique identifier.
        /// </returns>
        public Guid GetId(string name)
        {
            return reader.GetId(name);
        }

        /// <summary>
        /// Check to see if an app with the given unique name exists.
        /// </summary>
        /// <param name="name">
        /// The name of the app.
        /// </param>
        /// <returns>
        /// True if it exists.
        /// </returns>
        public bool Exists(string name)
        {
            return reader.Exists(name);
        }
    }
}
