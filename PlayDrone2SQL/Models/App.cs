
namespace PlayDrone2SQL.Models
{
    using System;

    /// <summary>
    /// A model of the app in the PlayDrone Json file.
    /// </summary>
    public class App
    {
        /// <summary>
        /// A unique identifier for the app.
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// The Google ID for the app.
        /// </summary>
        public string app_id { get; set; }

        /// <summary>
        /// The title of the app.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// The name of the developer.
        /// </summary>
        public string developer_name { get; set; }

        /// <summary>
        /// The category name. 
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// The category id.
        /// </summary>
        public Guid? categoryId { get; set; }

        /// <summary>
        /// A flag indicating if the app is free.
        /// </summary>
        public bool free { get; set; }

        /// <summary>
        /// The version number.
        /// </summary>
        public long version_code { get; set; }

        /// <summary>
        /// The version string.
        /// </summary>
        public string version_string { get; set; }

        /// <summary>
        /// The size of the installed app.
        /// </summary>
        public long installation_size { get; set; }

        /// <summary>
        /// The number of downloads.
        /// </summary>
        public long downloads { get; set; }

        /// <summary>
        /// The average star rating.
        /// </summary>
        public float star_rating { get; set; }

        /// <summary>
        /// The date the snapshot was taken.
        /// </summary>
        public DateTime snapshot_date { get; set; }

        /// <summary>
        /// The URL of the additional Json data file.
        /// </summary>
        public string metadata_url { get; set; }

        /// <summary>
        /// The URL of the apk for the app.
        /// </summary>
        public string apk_url { get; set; }
    }
}
