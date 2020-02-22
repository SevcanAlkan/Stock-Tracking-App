using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Core.MongoDB
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string PictureCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseSettings
    {
        string PictureCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
