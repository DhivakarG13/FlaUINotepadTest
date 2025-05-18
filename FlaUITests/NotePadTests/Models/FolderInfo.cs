namespace NotePadTests.Models
{
    /// <summary>
    /// Represents information about a folder, including its source and destination file paths.
    /// </summary>
    /// <remarks>This class is typically used to store and transfer information about a folder's source and 
    /// destination paths, such as when performing file operations like copying or moving folders.</remarks>
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
