namespace PlayDrone2SQL
{
    using System;
    using System.Linq;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            string filePath = null;
            if (args.Length == 0)
            {
                throw new ArgumentException("Should be called with a Json PlayDrone file to import.");
            }

            filePath = args[0];

            var log = new Logger();
            var appConverter = new FileReader(log);

            var apps = appConverter.FileToModel(filePath);
            var count = apps.Count();

            // Setup connection
            var connectionString = ConfigurationManager.ConnectionStrings["MarketDbConnectionString"];
            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                // Initialise app store with logger and connection
                var appSqlStore = new AppStore(connection);
                var appStore = new AppLogger(appSqlStore, appSqlStore, log);
                var categorySqlStore = new CategoryStore();
                var categoryCacheStore = new CategoryCache(categorySqlStore, categorySqlStore);
                var categoryStore = new CategoryLogger(categoryCacheStore, categoryCacheStore, log);

                // Assuming apps are added in order to save multiple queries to the database.
                var existingAppCount = appStore.Count();

                // Loop through apps. App store will save every 100,000 apps
                var appsToSave = new List<Models.App>();
                var appCounter = 0;
                for (int i = existingAppCount; i < count; i++)
                {
                    // Get the app from the list.
                    var app = apps[i];

                    // Store the app category if it doesn't exist.
                    if (!categoryStore.Exists(app.category))
                    {
                        var category = new Models.Category { Id = Guid.NewGuid(), Name = app.category };
                        categoryStore.Save(category);
                    }

                    // Lookup app category id.
                    var categoryId = categoryStore.GetId(app.category);

                    app.id = Guid.NewGuid();
                    app.categoryId = categoryId;
                    appsToSave.Add(app);
                    log.LogOperation(string.Format("App {0}/{1} added: {2}", i + 1, count, app.app_id));
                    appCounter++;

                    // Save apps
                    if(appCounter >= 100000)
                    {
                        appStore.SaveMany(appsToSave);
                        appsToSave.Clear();
                        appCounter = 0;
                    }
                }

                // Save the remainder
                appStore.SaveMany(appsToSave);

                log.LogOperation("DONE!!!!");
            }
        }
    }
}
