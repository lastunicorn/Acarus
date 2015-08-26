using System;

namespace DustInTheWind.Versioning.Download
{
    public class FileDownloadResult
    {
        public string SourceUri { get; set; }
        public string DestinationFilePath { get; set; }
        public Exception Error { get; set; }
        public bool Cancelled { get; private set; }
    }
}