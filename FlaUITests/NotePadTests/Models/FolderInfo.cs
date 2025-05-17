namespace NotePadTests.Models
{
    public class FolderInfo
    {
        public string SourceFilePath { get; set; }
        public string DestinationFilePath { get; set; }

        public FolderInfo(string sourceFilePath, string destinationFilePath)
        {
            SourceFilePath = sourceFilePath;
            DestinationFilePath = destinationFilePath;
        }
    }
}
