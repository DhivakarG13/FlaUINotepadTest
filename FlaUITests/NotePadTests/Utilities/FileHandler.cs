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
                configData = null;
                //LogTestData("JsonException thrown while Deserializing the config file, Check whether the file's data is in JSON format");
                //Change the exception message
            }
            catch (Exception ex)
            {
                configData = null;
                //LogTestData($"{ex.Message} thrown while Deserializing");
                //Change the exception message
            }

            return configData;
        }

        /// <summary>
        /// Appends the specified logMessage message to a file.
        /// </summary>
        /// <param name="logMessage">The logMessage message to write to the file. Cannot be null or empty.</param>
        public static void LogTestData(string logMessage, string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(logMessage);
                }
            }
        }

    }
}
