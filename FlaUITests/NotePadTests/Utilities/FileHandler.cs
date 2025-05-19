using NotePadTests.Models;
using System;
using System.IO;
using System.Text.Json;

namespace NotePadTests.Utilities
{
    /// <summary>
    /// Provides utility methods for handling file operations.
    /// </summary>
    public static class FileHandler
    {

        /// <summary>
        /// Reads the specified file, deserializes its JSON content, and returns a <see cref="FolderInfo"/> object.
        /// </summary>
        /// <param name="jsonFilePath">The full path to the configuration file to be read. Must be a valid file path.</param>
        /// <returns>A <see cref="FolderInfo"/> object containing the deserialized data from the given file or null if the file content is not in valid JSON format or an error occurs during deserialization.</returns>
        public static T FetchFileData<T>(string jsonFilePath) where T : class
        {
            string configFileData;
            T configData;

            try
            {
                using (FileStream configFileStream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader configStreamReader = new StreamReader(configFileStream))
                    {
                        configFileData = configStreamReader.ReadToEnd();
                    }
                }

                configData = JsonSerializer.Deserialize<T>(configFileData);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error occured while deserializing data from {jsonFilePath} check weather the data is in JSON format");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while fetching data from {jsonFilePath}");
            }

            return configData;
        }

        /// <summary>
        /// Appends the specified logMessage message to a file.
        /// </summary>
        /// <param name="logMessage">The logMessage message to write to the file. Cannot be null or empty.</param>
        public static void LogData(string logMessage, string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine(logMessage);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Error occured while Logging data in {filePath}");
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception($"Error occured while Logging data in {filePath}, No such directory is not found");
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception($"Error occured while Logging data in {filePath}, The {filePath} is not found\"");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while Logging data in {filePath}\n Error Type: {ex.ToString()}\n Error Message: {ex.Message}");
            }
        }

    }
}
