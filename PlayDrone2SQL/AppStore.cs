
namespace PlayDrone2SQL
{
    using Repository;
    using SqlBulkCopyExample;

    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Models;

    /// <summary>
    /// Store the app using SQL.
    /// </summary>
    public class AppStore: IWriter<Models.App>, IReader
    {
        private SqlBulkCopy bulkCopy;

        private SqlConnection connection;

        /// <summary>
        /// App Store constructor
        /// </summary>
        /// <param name="log">
        /// The logger.
        /// </param>
        /// <param name="connection">
        /// A SQL connection.
        /// </param>
        public AppStore(SqlConnection connection)
        {
            this.connection = connection;
            bulkCopy = new SqlBulkCopy(connection);
            bulkCopy.BulkCopyTimeout = 300;

            // Open the connection if it isn't open yet.
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            MapModelToDb();
        }

        /// <summary>
        /// Persist a list of apps to the database.
        /// </summary>
        /// <param name="apps">
        /// A list of apps to save.
        /// </param>
        public void SaveMany(List<Models.App> apps)
        {
            using (var dataReader = new ObjectDataReader<Models.App>(apps))
            {
                bulkCopy.WriteToServer(dataReader);
            }
        }

        /// <summary>
        /// Persist an app to the database.
        /// </summary>
        /// <param name="app">
        /// The app to save.
        /// </param>
        public void Save(Models.App app)
        {
            using (var db = new MarketDbContainer())
            {
                db.Apps.Add(Map(app));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Count the apps in the database.
        /// </summary>
        /// <returns>
        /// The number of apps in the database.
        /// </returns>
        public int Count()
        {
            using (var db = new MarketDbContainer())
            {
                return db.Apps.Count();
            }
        }

        /// <summary>
        /// Get the app id.
        /// </summary>
        /// <param name="name">
        /// The unique name of the app.
        /// </param>
        /// <returns>
        /// The unque identifier for the app.
        /// </returns>
        public Guid GetId(string name)
        {
            using (var db = new MarketDbContainer())
            {
                return db.Apps.Single(c => c.AppId == name).Id;
            }
        }

        /// <summary>
        /// Check to see if the app exists.
        /// </summary>
        /// <param name="name">
        /// The unique name of the app.
        /// </param>
        /// <returns>
        /// True if the app exists.
        /// </returns>
        public bool Exists(string name)
        {
            using (var db = new MarketDbContainer())
            {
                return db.Apps.Any(c => c.AppId == name);
            }
        }

        /// <summary>
        /// Map the Model to the database columns.
        /// </summary>
        private void MapModelToDb()
        {
            // TODO: Improve by using reflection
            bulkCopy.DestinationTableName = "Apps";
            bulkCopy.ColumnMappings.Add("id", "Id");
            bulkCopy.ColumnMappings.Add("app_id", "AppId");
            bulkCopy.ColumnMappings.Add("apk_url", "ApkUrl");
            bulkCopy.ColumnMappings.Add("categoryId", "Category");
            bulkCopy.ColumnMappings.Add("developer_name", "DeveloperName");
            bulkCopy.ColumnMappings.Add("downloads", "Downloads");
            bulkCopy.ColumnMappings.Add("free", "Free");
            bulkCopy.ColumnMappings.Add("installation_size", "InstallationSize");
            bulkCopy.ColumnMappings.Add("metadata_url", "MetadataUrl");
            bulkCopy.ColumnMappings.Add("snapshot_date", "SnapshotDate");
            bulkCopy.ColumnMappings.Add("star_rating", "StarRating");
            bulkCopy.ColumnMappings.Add("title", "Title");
            bulkCopy.ColumnMappings.Add("version_code", "VersionCode");
            bulkCopy.ColumnMappings.Add("version_string", "VersionString");
        }

        /// <summary>
        /// Map app model to entity model.
        /// </summary>
        /// <param name="app">
        /// The app model.
        /// </param>
        /// <returns>
        /// The app entity model.
        /// </returns>
        private Repository.App Map(Models.App app)
        {
            return new Repository.App
            {
                Id = (Guid)app.id,
                AppId = app.app_id,
                ApkUrl = app.apk_url,
                Category = (Guid)app.categoryId,
                DeveloperName = app.developer_name,
                Downloads = app.downloads,
                Free = app.free,
                InstallationSize = app.installation_size,
                MetadataUrl = app.metadata_url,
                SnapshotDate = app.snapshot_date,
                StarRating = app.star_rating,
                Title = app.title,
                VersionCode = app.version_code,
                VersionString = app.version_string
            };
        }
    }
}
